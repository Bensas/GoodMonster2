using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

    private const double GRABBING_RANGE = 2;
    private Camera myCam;
    public LayerMask collisionLayer;
    public Spawner spawner;

	// Use this for initialization
	void Start () {
        myCam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        var mouseRay = myCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastInfo;
        if (Physics.Raycast(mouseRay, out raycastInfo, float.MaxValue, collisionLayer))
            transform.position = raycastInfo.point;
        if (Input.GetMouseButtonDown(1))
        {
            foreach(Monster m in spawner.Monsters)
            {
                if (IsWithinGrabbingRange(m.transform.position))
                {
                    m.State = Monster.STATE.GRABBED;
                    break;
                }
            }
        }
	}

    private bool IsWithinGrabbingRange(Vector3 position)
    {
        if (Mathf.Abs(transform.position.x - position.x) < GRABBING_RANGE
            && Mathf.Abs(transform.position.y - position.y) < GRABBING_RANGE)
            return true;
        return false;
    }
}
