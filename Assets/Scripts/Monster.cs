using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
    private Camera myCam;
    public LayerMask collisionLayer;
    private Vector3 mousePos;

    private const int STARTING_HEALTH_NORMAL = 1;
    private const int STARTING_HEALTH_HEALTHY = 2;
    private const float SPEED_FACTOR_NORMAL = 1f/25;
    private const float SPEED_FACTOR_FAST = 1f/12;
    private const int DROP_DAMAGE = 2;
    private const float DROP_RANGE = 10;
    private Vector3 speed;
    public static int MonstersOnScreen = 0;
    public Spawner spawner;
    public GameObject bed;

    public enum STATE { ACTIVE, INACTIVE, GRABBED };
    public enum TYPE { FAST, HEALTHY, FASTHEALTHY, NORMAL};

    void Start()
    {
        Health = GetStartingHealth();
        State = STATE.INACTIVE;
        myCam = Camera.main;
    }

    void Update()
    {
        switch (State)
        {
            case STATE.ACTIVE:
                Vector3 finalSpeed = new Vector3(speed.x * GetSpeedFactor(), speed.y * GetSpeedFactor(), speed.z * GetSpeedFactor());
                transform.position += finalSpeed;
                break;
            case STATE.GRABBED:
                var mouseRay = myCam.ScreenPointToRay(Input.mousePosition);
                RaycastHit raycastInfo;
                if(Physics.Raycast(mouseRay, out raycastInfo, float.MaxValue, collisionLayer))
                    transform.position = raycastInfo.point;
                break;  
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

    private int GetStartingHealth()
    {
        switch (Type)
        {
            case TYPE.HEALTHY:
                return STARTING_HEALTH_HEALTHY;
            case TYPE.FASTHEALTHY:
                return STARTING_HEALTH_HEALTHY;
            default:
                return STARTING_HEALTH_NORMAL;
        }
    }

    private float GetSpeedFactor()
    {
        switch (Type)
        {
            case TYPE.FAST:
                return SPEED_FACTOR_FAST;
            case TYPE.FASTHEALTHY:
                return SPEED_FACTOR_FAST;
            default:
                return SPEED_FACTOR_NORMAL;
        }
    }

    public STATE State { get; set; }
    public TYPE Type { get; set; }
    public int Health { get; private set; }
}
