using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour {
    public const int KILL_SCORE = 30;
    public Text scoreText;

    private int score;
    private int health;

	// Use this for initialization
	void Start () {
        scoreText.text = "Score: 0";
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddScore(int num){
        score += num;
        scoreText.text = "Score: " + score;
    }
}
