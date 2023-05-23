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
    public static bool AutoRotating = false;
    public static bool Shuffling = false;
    public static bool Solving = false;
    public static bool Started = false;
    public static bool StartAnimationFinished = false;

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
    }

    public void PutDown(List<GameObject> littleCubes, Transform pivot)
    {
        foreach (GameObject littleCube in littleCubes)
        {
            if (littleCube != littleCubes[4])
                littleCube.transform.parent.transform.parent = pivot;
        }
    }

    string GetSideString(List<GameObject> side)
    {
        string sideString = "";
        foreach (GameObject face in side)
        {
            sideString += face.name[0].ToString();
        }
        return sideString;
    }

    public string GetStateString()
    {
        // the stateString needs to be build at this exact order
        // Up -> Right -> Front -> Down -> Left -> Back 
        // that's the order the solver algorithm expect it
        string stateString = "";
        stateString += GetSideString(Up);
        stateString += GetSideString(Right);
        stateString += GetSideString(Front);
        stateString += GetSideString(Down);
        stateString += GetSideString(Left);
        stateString += GetSideString(Back);
        return stateString;
    }
}
