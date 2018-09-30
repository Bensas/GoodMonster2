using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour {
    private Camera myCam;
    private LineRenderer lineRenderer;
    public GameObject source, destination;

	void Start () {
        myCam = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
	}
	
	void Update () {
        lineRenderer.SetPosition(0, source.transform.position);
        lineRenderer.SetPosition(1, destination.transform.position);
    }
}
