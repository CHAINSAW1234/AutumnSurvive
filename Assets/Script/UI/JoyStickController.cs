using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyStickController : MonoBehaviour
{
    [SerializeField] float _joystickRadius = 5.0f;


    [SerializeField]
    Image background;

    [SerializeField]
    Image knob;

    [SerializeField]
    Image panel;

    public void Start()
    {
        background.gameObject.SetActive(false);
        knob.gameObject.SetActive(false);

        BindEvent(panel.gameObject, ShowJoystick, Defines.UIEvent.Down);
        BindEvent(panel.gameObject, HideJoystick, Defines.UIEvent.Up);
        BindEvent(panel.gameObject, DragJoystickHandle, Defines.UIEvent.Drag);
    }

    private void ShowJoystick(PointerEventData data)
    {
        background.gameObject.SetActive(true);
        knob.gameObject.SetActive(true);

        Vector2 position = Camera.main.ScreenToWorldPoint(data.position);
        background.transform.position = position;

        BindEvent(knob.gameObject, DragJoystickHandle, Defines.UIEvent.Drag);
    }

    private void HideJoystick(PointerEventData data)
    {
        background.gameObject.SetActive(false);
        knob.gameObject.SetActive(false);

        BindEvent(knob.gameObject, DragJoystickHandle, Defines.UIEvent.Drag);
    }

    private void DragJoystickHandle(PointerEventData data)
    {
        Vector2 position = Camera.main.ScreenToWorldPoint(data.position);

        Vector2 dir = position - (Vector2)background.transform.position;

        dir = dir.normalized *  _joystickRadius;

        knob.transform.localPosition = dir;

    }


    public static void BindEvent(GameObject go, Action<PointerEventData> action, Defines.UIEvent type = Defines.UIEvent.Click)
    {
        UI_EventHandler evt = Utils.GetOrAddComponent<UI_EventHandler>(go);

        switch (type)
        {
            case Defines.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            case Defines.UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
            case Defines.UIEvent.Down:
                evt.OnDownHandler -= action;
                evt.OnDownHandler += action;
                break;
            case Defines.UIEvent.Up:
                evt.OnUpHandler -= action;
                evt.OnUpHandler += action;
                break;
        }
    }
    public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        public Action<PointerEventData> OnClickHandler = null;
        public Action<PointerEventData> OnDragHandler = null;
        public Action<PointerEventData> OnDownHandler = null;
        public Action<PointerEventData> OnUpHandler = null;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (OnDownHandler != null)
                OnDownHandler.Invoke(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (OnUpHandler != null)
                OnUpHandler.Invoke(eventData);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (OnClickHandler != null)
                OnClickHandler.Invoke(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (OnDragHandler != null)
                OnDragHandler.Invoke(eventData);
        }
    }

}
