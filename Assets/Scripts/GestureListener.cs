using UnityEngine;
using System.Collections;
using System;

public class GestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface {

    KinectManager manager;

    void Start()
    {
        manager = KinectManager.Instance;
    }

    void Update()
    {

    }

    public bool GestureCancelled(uint userId, int userIndex, KinectGestures.Gestures gesture, KinectWrapper.NuiSkeletonPositionIndex joint)
    {
        return false;
    }

    public bool GestureCompleted(uint userId, int userIndex, KinectGestures.Gestures gesture, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
    {
        if (gesture == KinectGestures.Gestures.Click)
        {
            //kinectOverlayer.CheckClick();
        }
        return true;
    }

    public void GestureInProgress(uint userId, int userIndex, KinectGestures.Gestures gesture, float progress, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
    {
    }

    public void UserDetected(uint userId, int userIndex)
    {
    }

    public void UserLost(uint userId, int userIndex)
    {
    }

}