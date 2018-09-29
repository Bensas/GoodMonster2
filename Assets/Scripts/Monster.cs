using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    private Vector3 speed;
    public GameObject bed;

    public enum STATE { ACTIVE, INACTIVE };
    STATE state = STATE.INACTIVE;

	void Start ()
    {
		
	}
	
	void Update ()
    {
		if(State == STATE.ACTIVE)
        {
            transform.position += speed;
        }
	}

    public void spawn()
    {
        State = STATE.ACTIVE;
        transform.position = Spawner.getRandomSpawnPoint();
        speed = new Vector3(bed.transform.position.x - transform.position.x, bed.transform.position.y - transform.position.y, 0)
                .normalized;
    }

    public STATE State { get; private set; }
}
