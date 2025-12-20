using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] public TMP_Text goldText;
    public TMP_Text hpText;
    public TMP_Text weaponText;
    public TMP_Text ammoText;

    private PlayerHealth ph;
    private Inventory inv;

    void Start()
    {
        // find from GameManager if present
        if (GameManager.Instance != null)
        {
            ph = GameManager.Instance.PlayerHealth;
            inv = GameManager.Instance.PlayerInventory;
        }

        // fallback find
        if (ph == null) ph = FindFirstObjectByType<PlayerHealth>();
        if (inv == null) inv = FindFirstObjectByType<Inventory>();
    }

    void Update()
    {
        if (goldText && GameSession.Instance != null)
            goldText.text = $"{GameSession.Instance.gold}";

        if (ph != null && hpText)
            hpText.text = $"HP: {ph.hp}/{ph.maxHP}";

        if (inv != null)
        {
            var w = inv.Current;

            if (weaponText)
                weaponText.text = w.data.displayName;

            if (ammoText)
            {
                if (w.data.infiniteAmmo) ammoText.text = "Ammo: ∞";
                else ammoText.text = $"Ammo: {w.ammo}/{w.data.maxAmmo}";
            }
        }
    }
}
