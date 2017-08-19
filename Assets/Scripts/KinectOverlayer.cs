using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KinectOverlayer : MonoBehaviour 
{
	public KinectWrapper.NuiSkeletonPositionIndex TrackedJoint = KinectWrapper.NuiSkeletonPositionIndex.HandRight;
    public Text debugText;

    Vector2 newPos = Vector2.zero;

    public void CheckClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
    }

    void Update()
    {
        
    }
}
