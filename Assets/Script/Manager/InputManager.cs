using System;
using UnityEngine;

public class InputManager
{
    public Action Actions = null;

    public Vector3 TouchStartPosition { get; private set; } = Vector3.zero;
    public Vector3 TouchDirection { get; private set; } = Vector3.zero;
    public float TouchDirectionMagnitude { get; private set; } = 0f;

    private const float movementThreshold = 2f;

    public void Update()
    {
        if(Time.timeScale == 0)
        {
            ResetInputData();
            return;
        }

        Vector3 inputPosition = Vector3.zero;
        bool isInputBegan = false;
        bool isInputMoved = false;
        bool isInputEnded = false;

#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            inputPosition = touch.position;
            isInputBegan = touch.phase == TouchPhase.Began;
            isInputMoved = touch.phase == TouchPhase.Moved;
            isInputEnded = touch.phase == TouchPhase.Ended;
        }
#elif UNITY_EDITOR
        inputPosition = Input.mousePosition;
        isInputBegan = Input.GetMouseButtonDown(0);
        isInputMoved = Input.GetMouseButton(0);
        isInputEnded = Input.GetMouseButtonUp(0);
#endif

        HandleInput(inputPosition, isInputBegan, isInputMoved, isInputEnded);

        if (Actions != null)
        {
            Actions.Invoke();
        }
    }

    private void HandleInput(Vector3 inputPosition, bool began, bool moved, bool ended)
    {
        inputPosition.z = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(inputPosition);

        if (began)
        {
            TouchStartPosition = worldPosition;
        }
        else if (moved)
        {
            TouchDirection = worldPosition - TouchStartPosition;
            TouchDirectionMagnitude = Mathf.Min(1f, TouchDirection.magnitude / movementThreshold);
            TouchDirection = TouchDirection.normalized;
        }
        else if (ended)
        {
            ResetInputData();
        }
    }

    private void ResetInputData()
    {
        TouchStartPosition = TouchDirection = Vector3.zero;
        TouchDirectionMagnitude = 0;
    }

    public void Clear()
    {
        Actions = null;
        ResetInputData();
    }
}

