using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour {
    private Camera myCam;
    private LineRenderer lineRenderer;
    public GameObject source, destination;

	// Use this for initialization
	void Start () {
        myCam = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        lineRenderer.SetPosition(0, source.transform.position);
        lineRenderer.SetPosition(1, destination.transform.position);
        //Debug.Log(-Vector2.SignedAngle((Vector2)pivot.transform.position, myCam.ScreenToWorldPoint(Input.mousePosition)));
        //float rotation = Vector2.SignedAngle((Vector2)pivot.transform.position, myCam.ScreenToWorldPoint(Input.mousePosition));
        //float rotation = 

        //var mouseRay = myCam.ScreenPointToRay(Input.mousePosition);
        //RaycastHit raycastInfo;
        //if (Physics.Raycast(mouseRay, out raycastInfo, float.MaxValue, collisionLayer)){
        //    pivot.transform.LookAt(raycastInfo.point);
        //    pivot.transform.eulerAngles = new Vector3(0, 0, pivot.transform.eulerAngles.z);
        //}
        //transform.localScale = new Vector3(1, Vector2.Distance((Vector2)pivot.transform.position, myCam.ScreenToWorldPoint(Input.mousePosition)), 1);

    }
}
