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
    private bool _autoRotating = false;
    private float _sensibility = 0.4f;
    private float _speed = 400f;
    private AudioSource _audioSource;
    
    public void Init(AudioSource audioSource) {
        _audioSource = audioSource;
    }

    // Start is called before the first frame update
    void Start()
    {
        _cubeState = FindFirstObjectByType<CubeState>();
        _readCube = FindFirstObjectByType<ReadCube>();
    }

    // Update is called once per frame at the end
    void LateUpdate()
    {
        if (_dragging && !_autoRotating)
        {
            SpinSide(_activeSide);
            if (Input.GetMouseButtonUp(0))
            {
                _dragging = false;
                RotateToCorrectAngle();
            }
        }

        if (_autoRotating)
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

    public void StartAutoRotate(List<GameObject> side, float angle)
    {
        _cubeState.PickUp(side);
        Vector3 localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;
        targetQuaternion = Quaternion.AngleAxis(angle, localForward) * transform.localRotation;
        _activeSide = side;
        _autoRotating = true;
    }

    public void RotateToCorrectAngle()
    {
        Vector3 vector = transform.localEulerAngles;
        vector.x = Mathf.Round(vector.x / 90) * 90;
        vector.y = Mathf.Round(vector.y / 90) * 90;
        vector.z = Mathf.Round(vector.z / 90) * 90;
        targetQuaternion.eulerAngles = vector;
        _autoRotating = true;
        _audioSource.Play();
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
            CubeState.AutoRotating = false;
            _autoRotating = false;
            _dragging = false;
        }
    }
}
