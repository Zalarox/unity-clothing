using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FootballScoreScript : MonoBehaviour {

    public Text scoreText;
    int score = 0;

	public void UpdateScore () {
        score += 100;
        scoreText.text = score.ToString();
	}
}
