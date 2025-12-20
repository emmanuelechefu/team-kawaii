using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Tooltip("All weapon datas that exist in the game (slash, pistol, etc).")]
    public List<WeaponData> allWeapons;

    [HideInInspector] public List<WeaponInstance> weapons = new();
    [HideInInspector] public int currentIndex = 0;

    public WeaponInstance Current => weapons[currentIndex];

    void Awake()
    {
        BuildFromGameSession();
    }

    public void BuildFromGameSession()
    {
        weapons.Clear();

        foreach (var wd in allWeapons)
        {
            var wi = new WeaponInstance { data = wd };
            wi.Init();

            // Owned comes from persistent session
            wi.owned = GameSession.Instance != null && GameSession.Instance.ownedWeapons.Contains(wd.id);

            // Always force Slash owned
            if (wd.id == WeaponId.Slash) wi.owned = true;

            weapons.Add(wi);
        }

        currentIndex = FindIndex(WeaponId.Slash);
        if (currentIndex < 0) currentIndex = 0;
    }

    public int FindIndex(WeaponId id)
    {
        for (int i = 0; i < weapons.Count; i++)
            if (weapons[i].data.id == id) return i;
        return -1;
    }

    public bool TrySetCurrentByScroll(int direction)
    {
        // direction: +1 or -1
        int start = currentIndex;

        for (int i = 0; i < weapons.Count; i++)
        {
            currentIndex = (currentIndex + direction + weapons.Count) % weapons.Count;
            if (weapons[currentIndex].owned) return true;
        }

        currentIndex = start;
        return false;
    }

    public void GrantWeapon(WeaponId id)
    {
        int idx = FindIndex(id);
        if (idx < 0) return;

        weapons[idx].owned = true;
        GameSession.Instance.ownedWeapons.Add(id);
    }

    public void AddAmmo(WeaponId id, int amount)
    {
        int idx = FindIndex(id);
        if (idx < 0) return;

        var w = weapons[idx];
        if (w.data.infiniteAmmo) return;

        w.ammo = Mathf.Clamp(w.ammo + amount, 0, w.data.maxAmmo);
    }

    public void FallbackToSlashIfEmpty()
    {
        var w = Current;
        if (w.data.id == WeaponId.Slash) return;
        if (w.data.infiniteAmmo) return;

        if (w.ammo <= 0)
        {
            int slash = FindIndex(WeaponId.Slash);
            if (slash >= 0) currentIndex = slash;
        }
    }
}
