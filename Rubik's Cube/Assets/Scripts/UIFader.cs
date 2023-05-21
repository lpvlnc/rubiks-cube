using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIFader : MonoBehaviour
{
    public Camera Camera;
    public List<CanvasGroup> UIElements;
    private bool _moveCamera = false;

    public void Update()
    {
        if (_moveCamera)
        {
            MoveCamera();
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
        Vector3 finalPosition = new(-13f, 9f, -10f);
        Camera.transform.position = Vector3.Lerp(Camera.transform.position, finalPosition, 2.0f * Time.deltaTime);
        if (Camera.transform.position.x > (finalPosition.x - 0.1f) &&
            Camera.transform.position.y > (finalPosition.y - 0.1f) &&
            Camera.transform.position.z > (finalPosition.z - 0.1f))
        {
            _moveCamera = false;
            CubeState.CameraAtPosition = true;
        }
    }

    public IEnumerator FadeCanvasGroup(List<CanvasGroup> uiElements, float end, float lerpTime = 1f)
    {
        float timeStartedLerping = Time.time;
        float timeSinceStarted = 0;
        float percentageComplete = 0;
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
