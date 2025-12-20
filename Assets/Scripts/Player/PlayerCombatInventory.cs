using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class PlayerCombatInventory : MonoBehaviour
{
    public Transform firePoint;
    public Camera mainCam;
    
    Animator anim;

    private Inventory inv;
    private float nextFireTime;
    private float regenTimer;

    void Awake()
    {
        inv = GetComponent<Inventory>();
        anim = GetComponent<Animator>();
        if (mainCam == null) mainCam = Camera.main;
    }

    void Update()
    {
        if (firePoint == null || mainCam == null || inv.weapons.Count == 0) return;

        HandleScroll();
        HandleSlashRegen();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("attack");
            TryFire();
        }

        inv.FallbackToSlashIfEmpty();
    }

    void HandleScroll()
    {
        float scroll = Input.mouseScrollDelta.y;
        if (Mathf.Abs(scroll) < 0.01f) return;
        inv.TrySetCurrentByScroll(scroll > 0 ? 1 : -1);
    }

    void TryFire()
    {
        var w = inv.Current;
        if (Time.time < nextFireTime) return;

        if (!w.data.infiniteAmmo && w.ammo <= 0) return;

        Vector3 mouseWorld = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mouseWorld - firePoint.position);
        dir.Normalize();

        var p = Instantiate(w.data.projectilePrefab, firePoint.position, Quaternion.identity);
        p.Init(dir, w.data.projectileSpeed, w.data.damage, w.data.projectilePassesThroughWalls, ownerTag: "Player");

        nextFireTime = Time.time + w.data.fireCooldown;

        if (!w.data.infiniteAmmo)
            w.ammo = Mathf.Max(0, w.ammo - 1);
    }

    void HandleSlashRegen()
    {
        int slashIndex = inv.FindIndex(WeaponId.Slash);
        if (slashIndex < 0) return;

        var slash = inv.weapons[slashIndex];
        if (!slash.data.regenAmmoOverTime || slash.data.infiniteAmmo) return;
        if (slash.ammo >= slash.data.maxAmmo) { regenTimer = 0f; return; }

        regenTimer += Time.deltaTime;
        if (regenTimer >= slash.data.regenInterval)
        {
            regenTimer -= slash.data.regenInterval;
            slash.ammo = Mathf.Min(slash.data.maxAmmo, slash.ammo + 1);
        }
    }
}
