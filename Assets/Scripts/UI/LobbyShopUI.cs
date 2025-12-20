using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LobbyShopUI : MonoBehaviour
{
    public TMP_Text goldText;

    public int ammoPackAmount = 10;
    public int ammoPackCost = 5;

    public int healCost = 8;

    public string nextLevelScene = "Level_1"; // change to Level2 later

    private PlayerHealth playerHealth;
    private PlayerCombat playerCombat;

    void Start()
    {
        // In lobby you may show a “dummy player” for preview, or spawn a player.
        // Simplest: have a player object in lobby.
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        playerCombat = FindFirstObjectByType<PlayerCombat>();
    }

    void Update()
    {
        if (goldText) goldText.text = $"GOLD: {GameSession.Instance.gold}";
    }

    public void ClaimStarterPistol()
    {
        GameSession.Instance.ownedWeapons.Add(WeaponId.Pistol);
        if (playerCombat != null) playerCombat.SetOwned(WeaponId.Pistol, true);
    }

    public void BuyAmmoForCurrentWeapon()
    {
        if (playerCombat == null) return;

        if (GameSession.Instance.gold < ammoPackCost) return;
        var current = playerCombat.weapons[playerCombat.currentWeaponIndex];

        if (current.data.infiniteAmmo) return;

        GameSession.Instance.gold -= ammoPackCost;
        playerCombat.AddAmmo(current.data.id, ammoPackAmount);
    }

    public void HealOne()
    {
        if (playerHealth == null) return;
        if (playerHealth.hp >= playerHealth.maxHP) return;
        if (GameSession.Instance.gold < healCost) return;

        GameSession.Instance.gold -= healCost;
        playerHealth.Heal(1);
    }

    public void GoToNextLevel()
    {
        SceneManager.LoadScene(nextLevelScene);
    }
}
