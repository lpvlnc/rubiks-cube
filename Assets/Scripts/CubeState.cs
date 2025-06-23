using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeState : MonoBehaviour
{
    // Sides
    public List<GameObject> Up = new();
    public List<GameObject> Left = new();
    public List<GameObject> Down = new();
    public List<GameObject> Front = new();
    public List<GameObject> Right = new();
    public List<GameObject> Back = new();

    public GameObject CubeLabel;
    public static bool AutoRotating = false;
    public static bool Shuffling = false;
    public static bool Solving = false;
    public static bool Started = false;
    public static bool StartAnimationFinished = false;

    private Text _label;
    private Color _color;
    public float BlinkFadeInTime = 0.3f;
    public float BlinkStayTime = 0.4f;
    public float BlinkFaceOutTime = 0.5f;
    private float _timer = 0f;

    public void Start()
    {
        _label = CubeLabel.GetComponent<Text>();
        _color = _label.color;
    }
    public void Update()
    {
        if (Shuffling)
            Blink("Shuffling...");
        else if (Solving)
            Blink("Solving...");
        else
        {
            _timer = 0f;
            CubeLabel.GetComponent<Text>().text = "";
        }
    }

    public void Blink(string text)
    {
        _timer += Time.deltaTime;
        if (_timer < BlinkFadeInTime)
            _label.color = new Color(_color.r, _color.g, _color.b, _timer / BlinkFadeInTime);
        else if (_timer < BlinkFadeInTime + BlinkStayTime)
            _label.color = new Color(_color.r, _color.g, _color.b, 1);
        else if (_timer < BlinkFadeInTime + BlinkStayTime + BlinkFaceOutTime)
            _label.color = new Color(_color.r, _color.g, _color.b, 1 - (_timer - (BlinkFadeInTime + BlinkStayTime)) / BlinkFaceOutTime);
        else
            _timer = 0f;
        CubeLabel.GetComponent<Text>().text = text;
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
