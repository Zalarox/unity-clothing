using UnityEngine;
using System.Collections;

public class CubeGestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface {

    bool swipeLeft;
    bool swipeRight;
    bool stop;
    bool raiseLeftHand;
    bool wheel;
    bool wave;
    bool psi;
    bool tpose;
    bool zoomOut;
    bool zoomIn;
    bool push;
    bool pull;
    bool jump;
    bool squat;

    public bool isPull()
    {
        if (pull)
        {
            pull = false;
            return true;
        }
        return false;
    }

    public bool isPush()
    {
        if (push)
        {
            push = false;
            return true;
        }
        return false;
    }

    public bool isZoomIn()
    {
        if (zoomIn)
        {
            zoomIn = false;
            return true;
        }
        return false;
    }

    public bool isZoomOut()
    {
        if (zoomOut)
        {
            zoomOut = false;
            return true;
        }
        return false;
    }

    public bool isTPose()
    {
        if (tpose)
        {
            tpose = false;
            return true;
        }
        return false;
    }

    public bool isPsi()
    {
        if(psi)
        {
            psi = false;
            return true;
        }
        return false;
    }

    public bool isWave()
    {
        if (wave)
        {
            wave = false;
            return true;
        }

        return false;
    }

    public bool isJump()
    {
        if (jump)
        {
            jump = false;
            return true;
        }

        return false;
    }


    public bool isSquat()
    {
        if (squat)
        {
            squat = false;
            return true;
        }

        return false;
    }


    public bool isWheel()
    {
        if (wheel)
        {
            wheel = false;
            return true;
        }

        return false;
    }

    public bool isRaiseLeftHand()
    {
        if (raiseLeftHand)
        {
            raiseLeftHand = false;
            return true;
        }

        return false;
    }

    public bool isStop()
    {
        if (stop)
        {
            stop = false;
            return true;
        }

        return false;
    }

    public bool IsSwipeLeft()
    {
        if (swipeLeft)
        {
            swipeLeft = false;
            return true;
        }

        return false;
    }

    public bool IsSwipeRight()
    {
        if (swipeRight)
        {
            swipeRight = false;
            return true;
        }

        return false;
    }


    public void UserDetected(uint userId, int userIndex)
    {
        // detect these user specific gestures
        KinectManager manager = KinectManager.Instance;
        manager.DetectGesture(userId, KinectGestures.Gestures.SwipeLeft);
        manager.DetectGesture(userId, KinectGestures.Gestures.SwipeRight);
        manager.DetectGesture(userId, KinectGestures.Gestures.RaiseLeftHand);
        manager.DetectGesture(userId, KinectGestures.Gestures.Wave);
        manager.DetectGesture(userId, KinectGestures.Gestures.Push);
        manager.DetectGesture(userId, KinectGestures.Gestures.Jump);
        manager.DetectGesture(userId, KinectGestures.Gestures.Squat);
        manager.DetectGesture(userId, KinectGestures.Gestures.Pull);
        manager.DetectGesture(userId, KinectGestures.Gestures.ZoomOut);
        manager.DetectGesture(userId, KinectGestures.Gestures.ZoomIn);
    }

    public void UserLost(uint userId, int userIndex)
    {

    }

    public void GestureInProgress(uint userId, int userIndex, KinectGestures.Gestures gesture,
                                  float progress, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
    {
        // don't do anything here
    }

    public bool GestureCompleted(uint userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
    {

        switch (gesture)
        {
            case KinectGestures.Gestures.ZoomIn: zoomIn = true; break;
            case KinectGestures.Gestures.ZoomOut: zoomOut = true; break;
            case KinectGestures.Gestures.SwipeLeft: swipeLeft = true; break;
            case KinectGestures.Gestures.SwipeRight: swipeRight = true; break;
            case KinectGestures.Gestures.RaiseLeftHand: raiseLeftHand = true; break;
            case KinectGestures.Gestures.Wave: wave = true; break;
            case KinectGestures.Gestures.Pull: pull = true; break;
            case KinectGestures.Gestures.Push: push = true; Debug.Log("PUSH"); break;
            case KinectGestures.Gestures.Squat: squat = true; Debug.Log("SQUAT"); break;
            case KinectGestures.Gestures.Jump: jump = true; Debug.Log("JUMP"); break;
            default: break;
        }
        return true;
    }

    public bool GestureCancelled(uint userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectWrapper.NuiSkeletonPositionIndex joint)
    {
        // don't do anything here, just reset the gesture state
        return true;
    }
}
