using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFace : MonoBehaviour
{
    CubeState _cubeState;
    ReadCube _readCube;
    int _layerMask = 1 << 6;
    public GameObject soundObject;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _cubeState = FindFirstObjectByType<CubeState>();
        _readCube = FindFirstObjectByType<ReadCube>();
        if (soundObject != null)
        {
            _audioSource = soundObject.GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !CubeState.AutoRotating && CubeState.StartAnimationFinished)
        {
            // Read the current state of the cube
            _readCube.ReadState();

            // Raycast from the mouse towards the cube to see if a face is hit
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100.0f, _layerMask))
            {
                GameObject face = hit.collider.gameObject;

                // Make a lista of all the sides (lists of face GameObjects)
                List<List<GameObject>> cubeSides = new()
                {
                    _cubeState.Up,
                    _cubeState.Left,
                    _cubeState.Down,
                    _cubeState.Front,
                    _cubeState.Right,
                    _cubeState.Back,
                };

                // If the face hit exists within a side
                foreach (List<GameObject> cubeSide in cubeSides)
                {
                    if (cubeSide.Contains(face))
                    {
                        // Pick it up
                        _cubeState.PickUp(cubeSide);
                        PivotRotation pr = cubeSide[4].transform.parent.GetComponent<PivotRotation>();
                        pr.Init(_audioSource);
                        pr.Rotate(cubeSide);
                    }
                }
            }
        }
    }
}
