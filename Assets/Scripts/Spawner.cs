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
    private int _difficulty = 1;
    private const int DIFFICULTY_INCREASE = 5;
    private const int MAX_DIFFICULTY = 5;
    public static readonly Vector3 OUT_OF_MAP_POSITION = new Vector3(20, -25);
    private static double _spawnTime = STARTING_SPAWN_TIME;
    private int _timeSinceLastSpawn = 0;
    private int _monstersSpawned = 0;
    private static readonly Vector2[] spawnPoints = { new Vector2(-10.86f, -9.94f), new Vector2(26.79f, -4.1f), new Vector2(-19.5f, 9.6f) };
    public Monster original;

    void Start()
    {
        GenerateMonsters();
    }

    void Update()
    {
        if (Monster.MonstersOnScreen < MAX_MONSTERS_ON_SCREEN && _timeSinceLastSpawn > _spawnTime)
        {
            Monster newSpawn = GetInactiveMonster();
            if (newSpawn != null)
            {
                newSpawn.Spawn(GetRandomSpawnPoint());
                newSpawn.Type = GetEnemyType();
                _timeSinceLastSpawn = 0;
                _monstersSpawned++;
            }
        }
        else
            _timeSinceLastSpawn++;
        if (_monstersSpawned % MONSTERS_TILL_LEVEL_UP == 0 && _monstersSpawned != 0)
        {
            if(_spawnTime < MINIMUM_SPAWN_TIME)
                _spawnTime *= DECREASE_IN_SPAWN_TIME;
            if(_difficulty < MAX_DIFFICULTY)
                _difficulty += DIFFICULTY_INCREASE;
        }
    }

    private Monster GetInactiveMonster()
    {
        foreach (Monster m in Monsters)
        {
            if (m.State == Monster.STATE.INACTIVE)
                return m;
        }
        return null;
    }

    private void GenerateMonsters()
    {
        Monsters = new List<Monster>();
        for (int i = 0; i < MONSTERS; i++)
            Monsters.Add(Instantiate(original, OUT_OF_MAP_POSITION, Quaternion.identity));
    }

    public Vector2 GetRandomSpawnPoint()
    {
        System.Random random = new System.Random();
        return spawnPoints[random.Next(3)];
    }

    private Monster.TYPE GetEnemyType()
    {
        int chance = new System.Random().Next(15);
        int fiftyFifty = new System.Random().Next(1);
        if(chance > _difficulty && chance < _difficulty*2)
        {
            if (fiftyFifty == 1)
                return Monster.TYPE.FAST;
            else
                return Monster.TYPE.HEALTHY;
        }
        if (chance < _difficulty)
            return Monster.TYPE.FASTHEALTHY;
        else
            return Monster.TYPE.NORMAL;
    }

    public List<Monster> Monsters { get; private set; }
}
