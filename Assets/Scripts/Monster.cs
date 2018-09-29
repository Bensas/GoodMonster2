using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    private Vector3 speed;
    private float speedFactor;
    public static int MonstersOnScreen = 0;
    public GameObject bed;

    public enum STATE { ACTIVE, INACTIVE };

    void Start()
    {
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

    public STATE State { get; private set; }
}
