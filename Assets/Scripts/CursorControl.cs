using UnityEngine;
using System.Collections;

public class CursorControl : MonoBehaviour {

    private static GUITexture texture;
    public static bool visible = false;

	void Start () {
        Cursor.visible = false;
        texture = gameObject.GetComponent<GUITexture>();
    }

    public static void CursorToggled(bool show)
    {
        visible = show;
        texture.enabled = visible;
    }
	
	void Update () {
    }
}
