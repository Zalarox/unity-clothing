using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OverlayTracker : MonoBehaviour {

    public KinectWrapper.NuiSkeletonPositionIndex TrackedJoint = KinectWrapper.NuiSkeletonPositionIndex.HandRight;
    public GameObject OverlayObject;
    public Text debugText;
    public float smoothFactor = 5f;

    private float distanceToCamera = 15f;
    private Rigidbody2D rb;

    void Start()
    {
        //if (OverlayObject)
        //{
        //    distanceToCamera = (OverlayObject.transform.position - Camera.main.transform.position).magnitude;
        //}
        rb = OverlayObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        KinectManager manager = KinectManager.Instance;

        if (manager && manager.IsInitialized())
        {
            int iJointIndex = (int)TrackedJoint;

            if (manager.IsUserDetected())
            {
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
                        float zPos = distanceToCamera + (-1*posJoint.z)*5f;
                        //float zPos = Mathf.Clamp((distanceToCamera + (posJoint.z * -6f)), (distanceToCamera-10), (distanceToCamera+10));
                        //debugText.text = "Right Hand POS: " + posJoint.ToString();
                        //debugText.text += " zPOS: " + zPos.ToString();
                        
                        if (OverlayObject)
                        {
                            Vector3 vPosOverlay = Camera.main.ViewportToWorldPoint(new Vector3(scaleX, scaleY, zPos));
                            OverlayObject.transform.position = Vector3.Lerp(OverlayObject.transform.position, vPosOverlay, smoothFactor * Time.deltaTime);

                            //Mathf.Clamp(vPosOverlay.y, 0.5f, 20f);
                            //rb.MovePosition(vPosOverlay);
                        }
                    }
                }
            }
        }
    }
}
