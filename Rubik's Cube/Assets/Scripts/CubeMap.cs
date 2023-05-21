using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set()
    {
        _cubeState = FindObjectOfType<CubeState>();
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
                map.GetComponent<Image>().color = Color.yellow; // Yellow - #FFFFBA

            if (face[i].name.Equals("Left"))
                map.GetComponent<Image>().color = Color.green; // Green - #BAFFC9

            if (face[i].name.Equals("Down"))
                map.GetComponent<Image>().color = Color.white; // White - #FFFFFF

            if (face[i].name.Equals("Front"))
                map.GetComponent<Image>().color = new(1, 0.5f, 0, 1); // Orange - #F8DBBA

            if (face[i].name.Equals("Right"))
                map.GetComponent<Image>().color = Color.blue; // Blue - #BAE1FF

            if (face[i].name.Equals("Back"))
                map.GetComponent<Image>().color = Color.red; // Red - #FFB3BA
            i++;
        }
    }
}
