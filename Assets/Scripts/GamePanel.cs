using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour {

    private Camera myCam;
    public LayerMask collisionLayer;
    public Spawner spawner;
    public GameObject bed;

    public const int KILL_SCORE = 30;
    public Text scoreText;

    private int score = 0;
    private int health = 1;

	// Use this for initialization
	void Start () {
        scoreText.text = "Score: 0";
        myCam = Camera.main;
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            var mouseRay = myCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastInfo;
            if (Physics.Raycast(mouseRay, out raycastInfo, float.MaxValue, collisionLayer))
                foreach (Monster monster in spawner.Monsters)
                    if (Vector3.Distance(monster.transform.position, raycastInfo.point) < 1f)
                    {
                        AddScore(GamePanel.KILL_SCORE);
                        monster.Hit(Hand.DEFAULT_HAND_DAMAGE);
                    }
        }

        foreach (Monster monster in spawner.Monsters)
            if (Vector3.Distance(monster.transform.position, bed.transform.position) < 1f){
                health--;
            }
        if (health <= 0){
            Lose();
        }


    }

    void Lose(){

    }

    void AddScore(int num){
        score += num;
        scoreText.text = "Score: " + score;
    }
}
