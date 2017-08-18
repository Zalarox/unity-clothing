using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    private int score = 0;
       
	void Start () {
    	
	}

    public void IncreaseScore()
    {
        score += 500;
    }

    public void DecreaseScore()
    {
        score -= 250;
    }

    public void SaveAndExit()
    {

    }
}
