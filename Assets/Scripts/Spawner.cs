using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    private const int FRAMES_PER_SECOND = 60;
    private const int MAX_MONSTERS_ON_SCREEN = 10;
    private const int MONSTERS = 20;
    private const int MINIMUM_SPAWN_TIME = 1 * FRAMES_PER_SECOND;
    private const int STARTING_SPAWN_TIME = 5 * FRAMES_PER_SECOND;
    private const int MONSTERS_TILL_LEVEL_UP = 10;
    private const double DECREASE_IN_SPAWN_TIME = 0.95;
    private const int SPAWN_POINTS = 3;
    private double spawnTime = STARTING_SPAWN_TIME;
    private int monstersOnScreen = 0;
    private int timeSinceLastSpawn = 0;
    private int monstersSpawned = 0;
    private LinkedList<Monster> monsters = new LinkedList<Monster>();
    private static Vector2[] spawnPoints = { new Vector2(-10.86f, -9.94f), new Vector2(26.79f, -4.1f), new Vector2(0, 0) };
    public Monster original;

    void Start()
    {
        generateMonsters();
    }

    void Update()
    {
        if (monstersOnScreen < MAX_MONSTERS_ON_SCREEN && timeSinceLastSpawn > STARTING_SPAWN_TIME)
        {
            Monster newSpawn = getInactiveMonster();
            if (newSpawn != null)
            {
                newSpawn.spawn();
                timeSinceLastSpawn = 0;
                monstersSpawned++;
            }
        }
        else
            timeSinceLastSpawn++;
        if (monstersSpawned % MONSTERS_TILL_LEVEL_UP == 0)
            spawnTime *= DECREASE_IN_SPAWN_TIME;
    }

    private Monster getInactiveMonster()
    {
        foreach (Monster m in monsters)
        {
            if (m.State == Monster.STATE.INACTIVE)
                return m;
        }
        return null;
    }

    private void generateMonsters()
    {
        for (int i = 0; i < MONSTERS; i++)
            monsters.AddFirst(Instantiate(original, new Vector3(20,-25), Quaternion.identity));
    }

    public static Vector2 getRandomSpawnPoint()
    {
        System.Random random = new System.Random();
        return spawnPoints[random.Next(3)];
    }
}
