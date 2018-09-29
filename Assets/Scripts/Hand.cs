using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {
    private Camera myCam;
    public LayerMask collisionLayer;

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
	}
}
