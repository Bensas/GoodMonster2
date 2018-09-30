using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour {
    private Camera myCam;
    public LayerMask collisionLayer;
    public Spawner spawner;
    public GamePanel gamePanel;

	// Use this for initialization
	void Start () {
        myCam = Camera.main;
	}

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            var mouseRay = myCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastInfo;
            if (Physics.Raycast(mouseRay, out raycastInfo, float.MaxValue, collisionLayer)){
                foreach (Monster monster in spawner.Monsters){
                    if (Vector3.Distance(monster.transform.position, raycastInfo.point) < 1f){
                        gamePanel.AddScore(GamePanel.KILL_SCORE);
                        monster.Hit(Hand.BASE_HAND_DAMAGE);
                    }
                }
                    
            }

        }

    }
}
