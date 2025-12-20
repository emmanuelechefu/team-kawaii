using UnityEngine;

public enum WeaponId { Slash, Pistol /* add more later */ }

[CreateAssetMenu(menuName = "Game/WeaponData")]
public class WeaponData : ScriptableObject
{
    public WeaponId id;
    public string displayName;

    [Header("Combat")]
    public Projectile projectilePrefab;
    public float fireCooldown = 0.25f;
    public int damage = 1;
    public float projectileSpeed = 14f;

    [Header("Ammo")]
    public bool infiniteAmmo = false;
    public int startAmmo = 30;
    public int maxAmmo = 30;

    [Tooltip("For Slash: regen 1 ammo every X seconds.")]
    public bool regenAmmoOverTime = false;
    public float regenInterval = 2.5f;

    [Header("Collision")]
    public bool projectilePassesThroughWalls = false;
}
