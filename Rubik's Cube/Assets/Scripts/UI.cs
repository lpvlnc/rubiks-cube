using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public Camera Camera;
    public List<CanvasGroup> UIElements;
    public GameObject Cube;
    private bool _moveCamera = false;
    public float time = 0f;
    private float _slerpSpeed = 0.2f;

    public void Update()
    {
        if (_moveCamera)
            MoveCamera();
        
        if (CubeState.StartAnimationFinished)
        {
            if (CubeState.Shuffling || CubeState.Solving)
            {
                UIElements[2].interactable = false;
                UIElements[3].interactable = false;
            }
            else
            {
                UIElements[2].interactable = true;
                UIElements[3].interactable = true;
            }
        }
    }

    public void FadeIn()
    {
        _moveCamera = true;
        StartCoroutine(FadeCanvasGroup(UIElements, 1));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(UIElements, 0));
    }

    public void MoveCamera()
    {
        Vector3 finalCameraPosition = new(-13f, 9f, -10f);
        Camera.transform.position = Vector3.Lerp(Camera.transform.position, finalCameraPosition, 2.0f * Time.deltaTime);

        Quaternion finalCubeRotation = new(0f, 0f, 0f, 1f);
        Cube.transform.localRotation = Quaternion.Slerp(Cube.transform.rotation, finalCubeRotation, time);
        time += Time.deltaTime * _slerpSpeed;

        if (Camera.transform.position.x > (finalCameraPosition.x - 0.1f) &&
            Camera.transform.position.y > (finalCameraPosition.y - 0.1f) &&
            Camera.transform.position.z > (finalCameraPosition.z - 0.1f) &&
            Cube.transform.rotation.x == finalCubeRotation.x &&
            Cube.transform.rotation.y < 1f &&
            Cube.transform.rotation.z == finalCubeRotation.z)
        {
            _moveCamera = false;
            CubeState.StartAnimationFinished = true;
        }
    }

    public IEnumerator FadeCanvasGroup(List<CanvasGroup> uiElements, float end, float lerpTime = 1f)
    {
        float timeStartedLerping = Time.time;
        float timeSinceStarted;
        float percentageComplete;
        while (true)
        {
            timeSinceStarted = Time.time - timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;
            for (int i = 0; i < uiElements.Count; i++)
            {
                float currentValue;
                if (i == 0 && end == 1)
                    currentValue = Mathf.Lerp(uiElements[i].alpha, 0, percentageComplete);
                else
                    currentValue  = Mathf.Lerp(uiElements[i].alpha, end, percentageComplete);
                uiElements[i].alpha = currentValue;
                if (end == 1)
                    uiElements[i].interactable = true;
                else
                    uiElements[i].interactable = false;
            }
            if (percentageComplete >= 1) break;
            yield return new WaitForEndOfFrame();
        }
    }
}
