using UnityEngine;
using System.Collections;
using System;

public class GestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
    KinectManager manager;

    public void UserDetected(uint userId, int userIndex)
    {
        manager = KinectManager.Instance;
        manager.DetectGesture(userId, KinectGestures.Gestures.Jump);
        manager.DetectGesture(userId, KinectGestures.Gestures.Squat);
    }

    public void UserLost(uint userId, int userIndex)
    {
        
    }

    public bool GestureCancelled(uint userId, int userIndex, KinectGestures.Gestures gesture, KinectWrapper.NuiSkeletonPositionIndex joint)
    {
        return true;
    }

    public bool GestureCompleted(uint userId, int userIndex, KinectGestures.Gestures gesture, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
    {
        switch (gesture)
        {
            case KinectGestures.Gestures.Jump:
                //swapper.avatars[swapper.index].transform.position = Vector3.Lerp(transform.position,  new Vector3(0, 1, 0), 0.5f);
                break;
            default: break;
        }
        return true;
    }

    public void GestureInProgress(uint userId, int userIndex, KinectGestures.Gestures gesture, float progress, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
    {
    }

    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
