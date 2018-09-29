using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    private Vector3 speed;
    private float speedFactor;
    public GameObject bed;

    public enum STATE { ACTIVE, INACTIVE };

	void Start ()
    {
        State = STATE.INACTIVE;
        speedFactor = 0.5f;
    }
	
	void Update ()
    {
		if(State == STATE.ACTIVE)
        {
            Vector3 finalSpeed = new Vector3(speed.x * speedFactor, speed.y * speedFactor, speed.z * speedFactor);
            transform.position += finalSpeed;
        }
	}

    public void Spawn()
    {
        State = STATE.ACTIVE;
        transform.position = Spawner.GetRandomSpawnPoint();
        speed = Vector3.Normalize(new Vector3(bed.transform.position.x - transform.position.x, bed.transform.position.y - transform.position.y, 0));
    }

    public void Kill()
    {
        State = STATE.INACTIVE;
        transform.position = Spawner.OUT_OF_MAP_POSITION;
    }

    public STATE State { get; private set; }
}
