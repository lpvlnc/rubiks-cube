using UnityEngine;

public class RotatingCube : MonoBehaviour
{
    Vector2 _firstPressPos;
    Vector2 _secondPressPos;
    Vector2 _currentSwipe;
    Vector3 _previousMousePosition;
    Vector3 _mouseDelta;
    public GameObject target;
    public float Speed = 400f;

    // Update is called once per frame
    void Update()
    {
        if (!CubeState.StartAnimationFinished)
            transform.Rotate(Vector3.up, 25f * Time.deltaTime, Space.World);
        else
        {
            Swipe();
            Drag();
        }
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

            if (LeftSwipe())
                target.transform.Rotate(0, 90, 0, Space.World);
            else if (RightSwipe())
                target.transform.Rotate(0, -90, 0, Space.World);
            else if (UpLeftSwipe())
                target.transform.Rotate(90, 0, 0, Space.World);
            else if (UpRightSwipe())
                target.transform.Rotate(0, 0, -90, Space.World);
            else if (DownLeftSwipe())
                target.transform.Rotate(0, 0, 90, Space.World);
            else if (DownRightSwipe())
                target.transform.Rotate(-90, 0, 0, Space.World);
        }
    }

    bool UpLeftSwipe()
    {
        return _currentSwipe.y > 0 && _currentSwipe.x < 0f;
    }

    bool UpRightSwipe()
    {
        return _currentSwipe.y > 0 && _currentSwipe.x > 0f;
    }

    bool LeftSwipe()
    {
        return _currentSwipe.x < 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f;
    }

    bool DownLeftSwipe()
    {
        return _currentSwipe.y < 0 && _currentSwipe.x < 0f;
    }

    bool DownRightSwipe()
    {
        return _currentSwipe.y < 0 && _currentSwipe.x > 0f;
    }

    bool RightSwipe()
    {
        return _currentSwipe.x > 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f;
    } 
}
