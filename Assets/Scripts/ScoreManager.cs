using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public Text scoreText;
    int score;
    bool trackingScore;
    public ParticleSystem particles;

    IEnumerator ScoreCoroutine()
    {
        while (true)
        {
            IncreaseScore();
            yield return new WaitForSeconds(2);
        }
    }

    IEnumerator ScoreDecCoroutine()
    {
        while (true)
        {
            DecreaseScore();
            yield return new WaitForSeconds(2);
        }
    }

    public void EnterScoreArea()
    {
        if(!trackingScore)
        {
            trackingScore = true;
            particles.Play();
            StopAllCoroutines();
            StartCoroutine("ScoreCoroutine");
        }
    }

    public void ExitScoreArea()
    {
        if(trackingScore)
        {
            trackingScore = false;
            particles.Stop();
            StopAllCoroutines();
            StartCoroutine("ScoreDecCoroutine");
        }
    }

    void Start () {
        trackingScore = false;
        score = 200;
        scoreText.text = score.ToString();
	}
	
	public void IncreaseScore()
    {
        score += 25;
        scoreText.text = score.ToString();
    }
    
    public void DecreaseScore()
    {
        if(score > 10)
            score -= 10;

        if (score < 0)
            score = 0;

        scoreText.text = score.ToString();
    }
}
