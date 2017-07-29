using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ManagerScript : MonoBehaviour {

    int clicks;
    public Text clickCounter;

	void Start () {
        clicks = 0;
	}

    IEnumerator decrementCoroutine()
    {
        while (clicks > 0)
        {
            clicks--;
            yield return new WaitForSeconds(5);
        }
        yield return null;
    }

    public void SetClicks(int clicks)
    {
        this.clicks = clicks;
        clickCounter.text = clicks.ToString();
    }

    public void IncrementClick()
    {
        StopAllCoroutines();
        clicks++;
        clickCounter.text = clicks.ToString();
        StartCoroutine("decrementCoroutine");
    }
	
	void Update () {
	}
}
