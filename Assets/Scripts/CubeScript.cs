using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class CubeScript : MonoBehaviour {
    public GameObject cube;
    public Text modeText;
    public float speed = 3f;
    public KinectWrapper.NuiSkeletonPositionIndex TrackedJoint = KinectWrapper.NuiSkeletonPositionIndex.HandRight;
    public GameObject OverlayObject;
    public float smoothFactor = 5f;

    private float distanceToCamera = 10f;
    private KinectManager manager;
    private bool swipeLeft;
    private bool swipeRight;
    private CubeGestureListener gestureListener;
    private int selectedMode;
    private const int MODE_IDLE = -1;
    private const int MODE_ROTATE = 0;
    private const int MODE_MOVE = 1;
    private const int MODE_SCALE = 2;
    private Vector3 screenPoint;
    
    public void SwitchMode(int mode) {
        switch(mode) {
            case MODE_IDLE: selectedMode = MODE_IDLE;
                modeText.text = "Idle...";
                break;
            case MODE_ROTATE: selectedMode = MODE_ROTATE;
                modeText.text = "Rotate";
                break;
            case MODE_MOVE: selectedMode = MODE_MOVE;
                modeText.text = "Move";
                break;
            case MODE_SCALE: selectedMode = MODE_SCALE;
                modeText.text = "Scale";
                break;
            default: break;
        }
    }

    public void ResetAttributes()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        transform.localScale.Set(0, 0, 0);
    }

    void Start()
    {
        manager = KinectManager.Instance;
        SwitchMode(MODE_IDLE);
        gestureListener = Camera.main.GetComponent<CubeGestureListener>();
    }

    void RotateCube()
    {
        int iJointIndex = (int)TrackedJoint;
        uint userId = manager.GetPlayer1ID();
        if (manager.IsJointTracked(userId, iJointIndex))
        {
            Vector3 posJoint = manager.GetRawSkeletonJointPos(userId, iJointIndex);
            if (posJoint != Vector3.zero)
            {
                Vector2 posDepth = manager.GetDepthMapPosForJointPos(posJoint);
                Vector2 posColor = manager.GetColorMapPosForDepthPos(posDepth);

                float scaleX = (float)posColor.x / KinectWrapper.Constants.ColorImageWidth;
                float scaleY = 1.0f - (float)posColor.y / KinectWrapper.Constants.ColorImageHeight;

                if (OverlayObject)
                {
                    Vector3 vPosOverlay = Camera.main.ViewportToWorldPoint(new Vector3(scaleX, scaleY, distanceToCamera));
                    OverlayObject.transform.Rotate(vPosOverlay);
                }
            }
        }
    }

    void MoveCube()
    {
        int iJointIndex = (int)TrackedJoint;
        uint userId = manager.GetPlayer1ID();
        if (manager.IsJointTracked(userId, iJointIndex))
        {
            Vector3 posJoint = manager.GetRawSkeletonJointPos(userId, iJointIndex);
            if (posJoint != Vector3.zero)
            {
                Vector2 posDepth = manager.GetDepthMapPosForJointPos(posJoint);
                Vector2 posColor = manager.GetColorMapPosForDepthPos(posDepth);

                float scaleX = (float)posColor.x / KinectWrapper.Constants.ColorImageWidth;
                float scaleY = 1.0f - (float)posColor.y / KinectWrapper.Constants.ColorImageHeight;

                if (OverlayObject)
                {
                    Vector3 vPosOverlay = Camera.main.ViewportToWorldPoint(new Vector3(scaleX, scaleY, distanceToCamera));
                    OverlayObject.transform.position = Vector3.Lerp(OverlayObject.transform.position, vPosOverlay, smoothFactor * Time.deltaTime);
                }
            }
        }
    }

    void Update()
    {
        if (manager && manager.IsInitialized())
        {
            if (gestureListener.IsSwipeLeft() && selectedMode == MODE_IDLE)
            {
                Debug.Log("Resetting..");
                ResetAttributes();
            }

            if (gestureListener.isJump() && selectedMode == MODE_SCALE)
            {
                OverlayObject.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
                Debug.Log("Scale up");
            }

            if (gestureListener.isSquat() && selectedMode == MODE_SCALE)
            {
                OverlayObject.transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
                Debug.Log("Scale down");
            }

            if(gestureListener.isRaiseLeftHand())
            {
                if (selectedMode != MODE_ROTATE && selectedMode == MODE_IDLE)
                    SwitchMode(MODE_ROTATE);
                else
                    SwitchMode(MODE_IDLE);

                Debug.Log("GESTURE: Raise Left Hand detected");
            }

            if(gestureListener.isWave())
            {
                if (selectedMode != MODE_SCALE && selectedMode == MODE_IDLE)
                    SwitchMode(MODE_SCALE);
                else
                    SwitchMode(MODE_IDLE);

                Debug.Log("GESTURE: Wave detected!");
            }

            if(gestureListener.isPull())
            {
                if (selectedMode != MODE_MOVE && selectedMode == MODE_IDLE)
                    SwitchMode(MODE_MOVE);
                else
                    SwitchMode(MODE_IDLE);

                Debug.Log("GESTURE: Pull detected!");
            }

            if (manager.IsUserDetected())
            {
                switch (selectedMode)
                {
                    case MODE_ROTATE: RotateCube(); break;
                    case MODE_MOVE: MoveCube(); break;
                    default: break;
                }
            }
        }
    }
}
