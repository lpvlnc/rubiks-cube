using System.Collections.Generic;
using UnityEngine;

public class ReadCube : MonoBehaviour
{
    public Transform TUp;
    public Transform TLeft;
    public Transform TDown;
    public Transform TFront;
    public Transform TRight;
    public Transform TBack;
    public GameObject EmptyGO;
    private readonly int _layerMask = 1 << 6; // This layerMask is for the faces of the cube only
    private CubeState _cubeState;
    private CubeMap _cubeMap;
    private List<GameObject> _upRays = new();
    private List<GameObject> _leftRays = new();
    private List<GameObject> _downRays = new();
    private List<GameObject> _frontRays = new();
    private List<GameObject> _rightRays = new();
    private List<GameObject> _backRays = new();

    // Start is called before the first frame update
    void Start()
    {
        SetRayTransforms();
        _cubeState = FindFirstObjectByType<CubeState>();
        _cubeMap = FindFirstObjectByType<CubeMap>();
        ReadState();
        CubeState.Started = true;
    }

    public void ReadState()
    {
        _cubeState = FindFirstObjectByType<CubeState>();
        _cubeMap = FindFirstObjectByType<CubeMap>();

        // Set the state of each position in the list of sides so we know
        // what color is in what position
        _cubeState.Up = ReadFace(_upRays, TUp);
        _cubeState.Left = ReadFace(_leftRays, TLeft);
        _cubeState.Down = ReadFace(_downRays, TDown);
        _cubeState.Front = ReadFace(_frontRays, TFront);
        _cubeState.Right = ReadFace(_rightRays, TRight);
        _cubeState.Back = ReadFace(_backRays, TBack);

        // Update the map with the found positions
        _cubeMap.Set();
    }

    void SetRayTransforms()
    {
        // Populate the ray lists with raycasts eminating from the transform, angled towards the cube
        _upRays = BuildRays(TUp, new Vector3(90, 90, 0));
        _leftRays = BuildRays(TLeft, new Vector3(0, 180, 0));
        _downRays = BuildRays(TDown, new Vector3(270, 90, 0));
        _frontRays = BuildRays(TFront, new Vector3(0, 90, 0));
        _rightRays = BuildRays(TRight, new Vector3(0, 0, 0));
        _backRays = BuildRays(TBack, new Vector3(0, 270, 0));
    }

    List<GameObject> BuildRays(Transform rayTransform, Vector3 direction)
    {
        // The ray count is used to name the rays so we can be sure they are in the right order
        int rayCount = 0;
        List<GameObject> rays = new();

        // This creates 9 rays in the shape of the side of the cube with
        // Ray 0 at the top left and ray 8 at the bottom right
        // |0|1|2|
        // |3|4|5|
        // |6|7|8|

        for (int y = 1; y > -2; y--)
        {
            for (int x = -1; x < 2; x++)
            {
                Vector3 startPos = new(rayTransform.localPosition.x + x, rayTransform.localPosition.y + y, rayTransform.localPosition.z);
                GameObject rayStart = Instantiate(EmptyGO, startPos, Quaternion.identity, rayTransform);
                rayStart.name = rayCount.ToString();
                rays.Add(rayStart);
                rayCount++;
            }
        }

        rayTransform.localRotation = Quaternion.Euler(direction);
        return rays;
    }

    public List<GameObject> ReadFace(List<GameObject> rayStarts, Transform rayTransform)
    {
        List<GameObject> facesHit = new();
        foreach (GameObject rayStart in rayStarts)
        {
            Vector3 ray = rayStart.transform.position;
            RaycastHit hit;

            // Does the ray intersect any objects in the layerMask?
            if (Physics.Raycast(ray, rayTransform.forward, out hit, Mathf.Infinity, _layerMask))
            {
                Debug.DrawRay(ray, rayTransform.forward * hit.distance, Color.green);
                facesHit.Add(hit.collider.gameObject);
            }
            else
                Debug.DrawRay(ray, rayTransform.forward * 1000, Color.red);
        }
        return facesHit;
    }
}
