using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounce : MonoBehaviour {

    public int appliedForce = 100;
    public FootballScoreScript scoreManager;

	void Start () {
		
	}
	
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            scoreManager.UpdateScore();
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            foreach (ContactPoint contact in collision.contacts)
            {
                rb.AddForce(contact.point * appliedForce * Time.deltaTime);
            }
        }
    }
}
