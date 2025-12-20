using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance { get; private set; }

    public int gold = 0;

    // Persist weapon ownership
    public HashSet<WeaponId> ownedWeapons = new HashSet<WeaponId> { WeaponId.Slash };

    // Optional: persist HP between Lobby and Levels (enable if you want)
    public int persistedHP = 3;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
