using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RotatingCube : MonoBehaviour
{
    Vector2 _firstPressPos;
    Vector2 _secondPressPos;
    Vector2 _currentSwipe;
    Vector3 _previousMousePosition;
    Vector3 _mouseDelta;

    public GameObject target;
    public float Speed = 400f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Swipe();
        Drag();
    }

    void Drag()
    {
        // While the mouse is held down the cube can be moved around its central axis to provide visual feedback
        if (Input.GetMouseButton(1))
        {
            _mouseDelta = Input.mousePosition - _previousMousePosition;
            _mouseDelta *= 0.1f; // reduction of rotation speed can be done here
            bool axisDirection = _mouseDelta.x * _mouseDelta.y < 0;
            transform.rotation = Quaternion.Euler(axisDirection ? _mouseDelta.y : _mouseDelta.y * 0.2f,-_mouseDelta.x, axisDirection ? -_mouseDelta.y * 0.2f : -_mouseDelta.y) * transform.rotation;
        }
        else
        {
            // Rotate cube till its rotation become equal the targets rotation
            if (transform.rotation != target.transform.rotation)
            {
                var step = Speed * Time.deltaTime;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, step);
            }
        }
        _previousMousePosition = Input.mousePosition;
    }

    void Swipe()
    {
        if (Input.GetMouseButtonDown(1))
            _firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        if (Input.GetMouseButtonUp(1))
        {
            _secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            _currentSwipe = new Vector2(_secondPressPos.x - _firstPressPos.x, _secondPressPos.y - _firstPressPos.y);
            _currentSwipe.Normalize();

            if (LeftSwipe(_currentSwipe))
            {
                target.transform.Rotate(0, 90, 0, Space.World);
            }
            else if (RightSwipe(_currentSwipe))
            {
                target.transform.Rotate(0, -90, 0, Space.World);
            }
            else if (UpLeftSwipe(_currentSwipe))
            {
                target.transform.Rotate(90, 0, 0, Space.World);
            }
            else if (UpRightSwipe(_currentSwipe))
            {
                target.transform.Rotate(0, 0, -90, Space.World);
            }
            else if (DownLeftSwipe(_currentSwipe))
            {
                target.transform.Rotate(0, 0, 90, Space.World);
            }
            else if (DownRightSwipe(_currentSwipe))
            {
                target.transform.Rotate(-90, 0, 0, Space.World);
            }
        }
    }

    bool UpLeftSwipe(Vector2 swipe)
    {
        return _currentSwipe.y > 0 && _currentSwipe.x < 0f;
    }

    bool UpRightSwipe(Vector2 swipe)
    {
        return _currentSwipe.y > 0 && _currentSwipe.x > 0f;
    }

    bool LeftSwipe(Vector2 swipe)
    {
        return _currentSwipe.x < 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f;
    }

    bool DownLeftSwipe(Vector2 swipe)
    {
        return _currentSwipe.y < 0 && _currentSwipe.x < 0f;
    }

    bool DownRightSwipe(Vector2 swipe)
    {
        return _currentSwipe.y < 0 && _currentSwipe.x > 0f;
    }

    bool RightSwipe(Vector2 swipe)
    {
        return _currentSwipe.x > 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f;
    } 
}
