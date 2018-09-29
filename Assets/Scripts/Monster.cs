using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    private const int STARTING_HEALTH = 1;
    private const int DROP_DAMAGE = 2;
    private const float DROP_RANGE = 10;
    private Vector3 speed;
    private float speedFactor;
    public static int MonstersOnScreen = 0;
    public Spawner spawner;
    public GameObject bed;
    public GameObject arm;

    public enum STATE { ACTIVE, INACTIVE, GRABBED };

    void Start()
    {
        Health = STARTING_HEALTH;
        State = STATE.INACTIVE;
        speedFactor = 1f/(new System.Random().Next(12,25));
    }

    void Update()
    {
        if (State == STATE.ACTIVE)
        {
            Vector3 finalSpeed = new Vector3(speed.x * speedFactor, speed.y * speedFactor, speed.z * speedFactor);
            transform.position += finalSpeed;
        }
        else if(State == STATE.GRABBED)
        {
            transform.position = arm.transform.position;
        }
    }

    public void Spawn(Vector2 spawnPosition)
    {
        State = STATE.ACTIVE;
        transform.position = spawnPosition;
        speed = Vector3.Normalize(new Vector3(bed.transform.position.x - transform.position.x, bed.transform.position.y - transform.position.y, 0));
        MonstersOnScreen++;
    }

    public void Kill()
    {
        State = STATE.INACTIVE;
        transform.position = Spawner.OUT_OF_MAP_POSITION;
        MonstersOnScreen--;
    }

    public void Drop(Vector3 position)
    {   
        if(State == STATE.GRABBED)
        {
            foreach (Monster m in spawner.Monsters)
            {
                if (IsWithinDropRange(m.transform.position))
                    m.Hit(DROP_DAMAGE);
            }
            Kill();
        }
    }

    public void Hit(int damage)
    {
        if(State == STATE.ACTIVE)
        {
            Health -= 2;
            if (Health <= 0)
                Kill();
        }
    }

    public bool IsWithinDropRange(Vector3 position)
    {
        if (Mathf.Abs(transform.position.x - position.x) < DROP_RANGE
            && Mathf.Abs(transform.position.y - position.y) < DROP_RANGE)
            return true;
        return false;
    }

    public STATE State { get; set; }
    public int Health { get; private set; }
}
