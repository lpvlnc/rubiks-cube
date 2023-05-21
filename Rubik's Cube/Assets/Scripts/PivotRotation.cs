using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotRotation : MonoBehaviour
{
    private CubeState _cubeState;
    private ReadCube _readCube;
    private List<GameObject> _activeSide;
    private Vector3 _localForward;
    private Vector3 _mouseRef;
    private Vector3 _rotation;
    private Quaternion targetQuaternion;
    private bool _dragging = false;
    private bool autoRotating = false;
    private float _sensibility = 0.4f;
    private float _speed = 400f;
    

    // Start is called before the first frame update
    void Start()
    {
        _cubeState = FindObjectOfType<CubeState>();
        _readCube = FindObjectOfType<ReadCube>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_dragging)
        {
            SpinSide(_activeSide);
            if (Input.GetMouseButtonUp(0))
            {
                _dragging = false;
                RotateToCorrectAngle();
            }
        }

        if (autoRotating)
            AutoRotate();
    }

    private void SpinSide(List<GameObject> side)
    {
        // Reset the rotation
        _rotation = Vector3.zero;

        // Current mouse position minus the last mouse position
        Vector3 mouseOffset = (Input.mousePosition - _mouseRef);

        if (side == _cubeState.Up)
            _rotation.y = (mouseOffset.x + mouseOffset.y) * _sensibility * 1;

        if (side == _cubeState.Left)
            _rotation.z = (mouseOffset.x + mouseOffset.y) * _sensibility * 1;

        if (side == _cubeState.Down)
            _rotation.y = (mouseOffset.x + mouseOffset.y) * _sensibility * -1;

        if (side == _cubeState.Front)
            _rotation.x = (mouseOffset.x + mouseOffset.y) * _sensibility * -1;

        if (side == _cubeState.Right)
            _rotation.z = (mouseOffset.x + mouseOffset.y) * _sensibility * -1;

        if (side == _cubeState.Back)
            _rotation.x = (mouseOffset.x + mouseOffset.y) * _sensibility * 1;


        // Rotate
        transform.Rotate(_rotation, Space.Self);

        // store mouse
        _mouseRef = Input.mousePosition;
    }

    public void Rotate(List<GameObject> side)
    {
        _activeSide = side;
        _mouseRef = Input.mousePosition;
        _dragging = true;
        // Create a vector to rotate around
        _localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;
    }

    public void RotateToCorrectAngle()
    {
        Vector3 vector = transform.localEulerAngles;
        // Round vector to nearest 90 degress
        vector.x = Mathf.Round(vector.x / 90) * 90;
        vector.y = Mathf.Round(vector.y / 90) * 90;
        vector.z = Mathf.Round(vector.z / 90) * 90;

        targetQuaternion.eulerAngles = vector;
        autoRotating = true;
    }

    private void AutoRotate()
    {
        _dragging = false;
        var step = _speed * Time.deltaTime;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetQuaternion, step);
        
        // If within one degree, set angle to target angle and end the rotation
        if (Quaternion.Angle(transform.localRotation, targetQuaternion) <= 1)
        {
            transform.localRotation = targetQuaternion;
            // Unparent the little cubes
            _cubeState.PutDown(_activeSide, transform.parent);
            _readCube.ReadState();
            autoRotating = false;
            _dragging = false;
        }
    }
}
