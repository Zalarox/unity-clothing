using UnityEngine;
using System.Collections;

public class ShapeChanger : MonoBehaviour {

    public GameObject[] shapes;

    private int currentIndex;
    private GameObject currentShape;

    void Start()
    {
        currentIndex = 0;
        currentShape = Instantiate(shapes[currentIndex]) as GameObject;
    }

	public void NextShape()
    {
        if (currentIndex < shapes.Length-1)
        {
            Destroy(currentShape);
            currentShape = Instantiate(shapes[++currentIndex]) as GameObject;
        }
        else
        {
            Destroy(currentShape);
            currentIndex = 0;
            currentShape = Instantiate(shapes[currentIndex]) as GameObject;
        }

    }
}
