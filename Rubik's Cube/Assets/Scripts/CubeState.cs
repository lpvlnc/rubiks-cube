using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeState : MonoBehaviour
{
    // Sides
    public List<GameObject> Up = new();
    public List<GameObject> Left = new();
    public List<GameObject> Down = new();
    public List<GameObject> Front = new();
    public List<GameObject> Right = new();
    public List<GameObject> Back = new();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUp(List<GameObject> cubeSide)
    {
        foreach (GameObject face in cubeSide)
        {
            // Attach the parent of each face (the little cube)
            // to the parent of the 4th index (the little cube in the middle)
            // unless it is already the 4th index
            if (face != cubeSide[4])
            {
                face.transform.parent.transform.parent = cubeSide[4].transform.parent;
            }
        }
        //start the side rotation logic
        cubeSide[4].transform.parent.GetComponent<PivotRotation>().Rotate(cubeSide);
    }

    public void PutDown(List<GameObject> littleCubes, Transform pivot)
    {
        foreach (GameObject littleCube in littleCubes)
        {
            if (littleCube != littleCubes[4])
                littleCube.transform.parent.transform.parent = pivot;
        }
    }
}
