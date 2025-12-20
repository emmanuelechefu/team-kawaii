using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Entry
    {
        public EnemyBase prefab;
        public float weight = 1f;
    }

    [Header("Spawn Points (place these off-screen)")]
    public Transform leftSpawn;
    public Transform rightSpawn;

    [Header("Player near-wall spawn block")]
    public Transform leftWallPoint;
    public Transform rightWallPoint;
    public float blockDistance = 3f;

    [Header("Enemies")]
    public List<Entry> enemies;

    [Header("Difficulty Ramp")]
    public float baseInterval = 4f;
    public float minInterval = 0.6f;
    public float intervalRampPerSecond = 0.02f; // reduces interval over time
    public int baseMaxAlive = 4;
    public int maxAliveRampPer30s = 1;

    private Transform player;
    private float timer;
    private float timeAlive;
    private readonly List<EnemyBase> alive = new();

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null || enemies.Count == 0) return;

        alive.RemoveAll(e => e == null);

        timeAlive += Time.deltaTime;

        int maxAlive = baseMaxAlive + Mathf.FloorToInt(timeAlive / 30f) * maxAliveRampPer30s;
        if (alive.Count >= maxAlive) return;

        float interval = Mathf.Max(minInterval, baseInterval - timeAlive * intervalRampPerSecond);

        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0f;
            SpawnOne();
        }
    }

    void SpawnOne()
    {
        bool canLeft = Vector2.Distance(player.position, leftWallPoint.position) > blockDistance;
        bool canRight = Vector2.Distance(player.position, rightWallPoint.position) > blockDistance;

        Transform spawn = null;
        if (canLeft && canRight) spawn = Random.value < 0.5f ? leftSpawn : rightSpawn;
        else if (canLeft) spawn = leftSpawn;
        else if (canRight) spawn = rightSpawn;
        else return;

        EnemyBase prefab = PickWeighted();
        if (prefab == null) return;

        var e = Instantiate(prefab, spawn.position, Quaternion.identity);
        alive.Add(e);
    }

    EnemyBase PickWeighted()
    {
        float total = 0f;
        foreach (var e in enemies) total += Mathf.Max(0f, e.weight);

        float r = Random.value * total;
        foreach (var e in enemies)
        {
            r -= Mathf.Max(0f, e.weight);
            if (r <= 0f) return e.prefab;
        }
        return enemies[0].prefab;
    }
}
