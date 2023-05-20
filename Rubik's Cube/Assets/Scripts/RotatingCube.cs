using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RotatingCube : MonoBehaviour
{
    Vector2 _firstPressPos;
    Vector2 _secondPressPos;
    Vector2 _currentSwipe;

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
        // Rotate cube till its rotation become equal the targets rotation
        if (transform.rotation != target.transform.rotation)
        {
            var step = Speed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, step);
        }
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

    bool LeftSwipe(Vector2 swipe)
    {
        return _currentSwipe.x < 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f;
    }

    bool RightSwipe(Vector2 swipe)
    {
        return _currentSwipe.x > 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f;
    }

    bool UpLeftSwipe(Vector2 swipe)
    {
        return _currentSwipe.y > 0 && _currentSwipe.x < 0f;
    }

    bool UpRightSwipe(Vector2 swipe)
    {
        return _currentSwipe.y > 0 && _currentSwipe.x > 0f;
    }

    bool DownLeftSwipe(Vector2 swipe)
    {
        return _currentSwipe.y < 0 && _currentSwipe.x < 0f;
    }

    bool DownRightSwipe(Vector2 swipe)
    {
        return _currentSwipe.y < 0 && _currentSwipe.x > 0f;
    }
}
