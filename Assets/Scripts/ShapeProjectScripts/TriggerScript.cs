using UnityEngine;
using System.Collections;

public class TriggerScript : MonoBehaviour {
    
    ScoreManager scoreManager;

    void Start()
    {
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (this.CompareTag("Inside"))
            scoreManager.EnterScoreArea();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (this.CompareTag("Inside"))
            scoreManager.ExitScoreArea();
    }

    void OnTriggerEnter(Collider col)
    {
        if (this.CompareTag("Inside"))
            scoreManager.EnterScoreArea();
    }

    void OnTriggerExit(Collider col)
    {
        if (this.CompareTag("Inside"))
            scoreManager.ExitScoreArea();
    }
}
