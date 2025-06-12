using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public Action Actions = null;

    public Vector3 TouchStartPosition { get; private set; } = Vector3.zero;
    public Vector3 TouchDirection { get; private set; } = Vector3.zero;

    public float TouchDirectionMagnitude { get; private set; } = 0f;
    
    private const float MovementThreshold = 150f;
    public void Update()
    {

#if UNITY_EDITOR
#else
        if (Input.touchCount == 0)
        {
            return;
        }
#endif

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                TouchStartPosition = touch.position;
            }
            else if(touch.phase == TouchPhase.Moved)
            {
                TouchDirection = new Vector3(touch.position.x - TouchStartPosition.x , touch.position.y - TouchStartPosition.y, 0);
                TouchDirectionMagnitude = Mathf.Lerp(0, 1, TouchDirection.magnitude / MovementThreshold);
                TouchDirection = Vector3.Normalize(TouchDirection);
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                TouchStartPosition = TouchDirection = Vector3.zero;
                TouchDirectionMagnitude = 0;
            }
        }

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            TouchStartPosition = Input.mousePosition;
        }
        else if(Input.GetMouseButton(0))
        {
            TouchDirection = new Vector3(Input.mousePosition.x - TouchStartPosition.x, Input.mousePosition.y - TouchStartPosition.y, 0);
            TouchDirectionMagnitude = Mathf.Lerp(0, 1, TouchDirection.magnitude / MovementThreshold);
            TouchDirection = Vector3.Normalize(TouchDirection);

        }
        else if(Input.GetMouseButtonUp(0))
        {
            TouchStartPosition = TouchDirection = Vector3.zero;
            TouchDirectionMagnitude = 0;
        }
#endif

        if (!Utils.NullCheck(Actions))
        {
            Actions.Invoke();
        }
    }

    public void Clear()
    {
        Actions = null;    
    }
}

