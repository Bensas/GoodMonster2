using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour {
    private Camera myCam;
    public LayerMask collisionLayer;

    public Hand rightHand, leftHand;

    // Use this for initialization
    void Start()
    {
        myCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        var mouseRay = myCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastInfo;
        if (Physics.Raycast(mouseRay, out raycastInfo, float.MaxValue, collisionLayer)){
            if (raycastInfo.point.x > -2f && raycastInfo.point.x < 1.5f && rightHand.state != Hand.STATE.REACHING_MIDDLE && rightHand.state != Hand.STATE.MIDDLE)
            {
                rightHand.state = Hand.STATE.REACHING_MIDDLE;
                rightHand.SetSpeed();
                leftHand.state = Hand.STATE.REACHING_MIDDLE;
                leftHand.SetSpeed();
            }
            else if (raycastInfo.point.x < -2f && leftHand.state != Hand.STATE.REACHING_SIDE && leftHand.state != Hand.STATE.SIDE)
            {
                rightHand.state = Hand.STATE.RETREATING;
                rightHand.SetSpeed();
                leftHand.state = Hand.STATE.REACHING_SIDE;
                leftHand.SetSpeed();

            }
            else if (raycastInfo.point.x > 1.5f && rightHand.state != Hand.STATE.REACHING_SIDE && rightHand.state != Hand.STATE.SIDE)
            {
                rightHand.state = Hand.STATE.REACHING_SIDE;
                rightHand.SetSpeed();
                leftHand.state = Hand.STATE.RETREATING;
                leftHand.SetSpeed();
            }
        }
    }
}
