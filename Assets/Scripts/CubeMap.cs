using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeMap : MonoBehaviour
{
    CubeState _cubeState;

    public Transform Up;
    public Transform Left;
    public Transform Down;
    public Transform Front;
    public Transform Right;
    public Transform Back;

    public void Set()
    {
        _cubeState = FindFirstObjectByType<CubeState>();
        UpdateMap(_cubeState.Up, Up);
        UpdateMap(_cubeState.Left, Left);
        UpdateMap(_cubeState.Down, Down);
        UpdateMap(_cubeState.Front, Front);
        UpdateMap(_cubeState.Right, Right);
        UpdateMap(_cubeState.Back, Back);
    }

    void UpdateMap(List<GameObject> face, Transform side) 
    {
        int i = 0;
        foreach (Transform map in side) 
        {
            if (face[i].name.Equals("Up"))
                map.GetComponent<Image>().color = Colors.Up;

            if (face[i].name.Equals("Left"))
                map.GetComponent<Image>().color = Colors.Left;

            if (face[i].name.Equals("Down"))
                map.GetComponent<Image>().color = Colors.Down;

            if (face[i].name.Equals("Front"))
                map.GetComponent<Image>().color = Colors.Front;

            if (face[i].name.Equals("Right"))
                map.GetComponent<Image>().color = Colors.Right;

            if (face[i].name.Equals("Back"))
                map.GetComponent<Image>().color = Colors.Back;
            i++;
        }
    }
}
