using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

    public static readonly int DEFAULT_HAND_DAMAGE = 1;
    private Camera myCam;
    public LayerMask collisionLayer;

    public enum STATE{INACTIVE, RETREATING, REACHING_MIDDLE, MIDDLE, REACHING_SIDE, SIDE}
    public STATE state;

    private const float GRABBING_RANGE = 1f;

    public GameObject bed;
    private Vector3 mousePosition;
    public Spawner spawner;

    public Vector3 speed = new Vector3(0, 0, 0);
    public float speedFactor = 0.5f;

    private void Start()
    {
        myCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        var mouseRay = myCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastInfo;
        if (Physics.Raycast(mouseRay, out raycastInfo, float.MaxValue, collisionLayer))
        {
            mousePosition = raycastInfo.point;

            //transform.Rotate(Vector3.Angle())

            switch (state)
            {
                case STATE.INACTIVE:
                    break;

                case STATE.RETREATING:
                    transform.position += speed;
                    if (Vector3.Distance(bed.transform.position, transform.position) < 1f)
                    {
                        state = STATE.INACTIVE;
                    }

                    break;

                case STATE.REACHING_MIDDLE:
                    speed = speedFactor * Vector3.Normalize(new Vector3(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y, 0));
                    transform.position += speed;
                    Debug.Log(transform.position);

                    if (Vector3.Distance(mousePosition, transform.position) < 1f)
                    {
                        state = STATE.MIDDLE;
                    }

                    break;

                case STATE.MIDDLE:
                    transform.position = mousePosition;
                    transform.Translate(0, 0.5f, 0);
                    break;

                case STATE.REACHING_SIDE:
                    speed = speedFactor * Vector3.Normalize(new Vector3(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y, 0));
                    transform.position += speed;
                    Debug.Log(transform.position);
                    if (Vector3.Distance(mousePosition, transform.position) < 1f)
                    {
                        state = STATE.SIDE;
                    }
                    break;

                case STATE.SIDE:
                    transform.position = mousePosition;
                    break;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            foreach (Monster m in spawner.Monsters)
            {
                if (Vector3.Distance(m.transform.position, transform.position) < GRABBING_RANGE)
                {
                    m.State = Monster.STATE.GRABBED;
                    break;
                }
            }
        }
    }



    public void SetSpeed(){
        switch (state)
        {
            case STATE.INACTIVE:
                break;

            case STATE.RETREATING:
                speed = speedFactor * Vector3.Normalize(new Vector3(bed.transform.position.x - transform.position.x, bed.transform.position.y - transform.position.y, 0));
                break;

            case STATE.REACHING_MIDDLE:
                speed = speedFactor * Vector3.Normalize(new Vector3(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y, 0));
                break;

            case STATE.REACHING_SIDE:
                speed = speedFactor * Vector3.Normalize(new Vector3(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y, 0));
                break;
        }
    }
}
