using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InputManager
{
    public Action Actions = null;

    public Vector3 TouchStartPosition { get => touchStartPosition; }
    public Vector3 TouchDirection { get => touchDirection; }

    private Vector3 touchStartPosition = Vector3.zero;
    private Vector3 touchDirection = Vector3.zero;

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
                touchStartPosition = touch.position;
            }
            else if(touch.phase == TouchPhase.Moved)
            {
                touchDirection = new Vector3(touch.position.x - touchStartPosition.x , touch.position.y - touchStartPosition.y, 0);
                touchDirection = Vector3.Normalize(touchDirection) * Mathf.Lerp(0,1, touchDirection.magnitude / MovementThreshold);
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                touchStartPosition = touchDirection = Vector3.zero;
            }
        }

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPosition = Input.mousePosition;
        }
        else if(Input.GetMouseButton(0))
        {
            touchDirection = new Vector3(Input.mousePosition.x - touchStartPosition.x, Input.mousePosition.y - touchStartPosition.y, 0);
            touchDirection = Vector3.Normalize(touchDirection) * Mathf.Lerp(0, 1, touchDirection.magnitude / MovementThreshold); ;

        }
        else if(Input.GetMouseButtonUp(0))
        {
            touchStartPosition = touchDirection = Vector3.zero;
        }
#endif

        if (Actions != null)
        {
            Actions.Invoke();
        }
    }

    public void Clear()
    {
        Actions = null;    
    }
}

