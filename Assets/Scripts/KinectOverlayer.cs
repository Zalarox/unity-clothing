using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KinectOverlayer : MonoBehaviour 
{
	public KinectWrapper.NuiSkeletonPositionIndex TrackedJoint = KinectWrapper.NuiSkeletonPositionIndex.HandRight;
    public Text debugText;

    GameObject selectedObject;
    SpriteRenderer soSpriteRenderer;
    Rigidbody2D soRB;
    Vector2 newPos = Vector2.zero;

    public void SelectObject(GameObject obj)
    {
        transform.position = new Vector3(0, 4, 0);
        selectedObject = Instantiate(obj, transform.position, Quaternion.identity) as GameObject;
        soSpriteRenderer = selectedObject.GetComponent<SpriteRenderer>();
        soSpriteRenderer.color = Color.red;
        soRB = selectedObject.GetComponent<Rigidbody2D>();
    }

    void PickObject(GameObject obj)
    {
        selectedObject = obj;
        soSpriteRenderer = obj.GetComponent<SpriteRenderer>();
        soSpriteRenderer.color = Color.red;
        soRB = obj.GetComponent<Rigidbody2D>();
        soRB.gravityScale = 0;
    }

    void PlaceObject()
    {
        selectedObject = null;
        soSpriteRenderer.color = Color.white;
        soSpriteRenderer = null;
        newPos = Vector2.zero;
        soRB.gravityScale = 1;
        soRB = null;
    }

    void StartPhysics()
    {
        Debug.Log("Enabling physics engine.");
    }

    public void CheckClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null && selectedObject != null)
        {
            Debug.Log("Placing object.");
            PlaceObject();
        }
        else if(hit.collider != null && selectedObject == null && hit.collider.CompareTag("Shape"))
        {
            PickObject(hit.collider.gameObject);
        }
    }

    void Update()
    {
        if(selectedObject)
        {
            debugText.text = transform.position.ToString();
            //selectedObject.transform.position = Vector3.Lerp(selectedObject.transform.position, transform.position, Time.deltaTime * 5f);
            newPos = (transform.position - selectedObject.transform.position);
            soRB.velocity = newPos * 20f;
        }
    }
}
