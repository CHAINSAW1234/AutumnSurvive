using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyStickController : MonoBehaviour
{
    [SerializeField]
    private Image background;
    [SerializeField]
    private Image knob;
    [SerializeField]
    private Image panel;

    private const float joystickRadius = 0.8f;

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

        DragJoystickHandle(data);
    }

    private void HideJoystick(PointerEventData data)
    {
        background.gameObject.SetActive(false);
        knob.gameObject.SetActive(false);
    }

    private void DragJoystickHandle(PointerEventData data)
    {
        Vector2 parentPosition = knob.transform.parent.position;
        Vector2 position = Camera.main.ScreenToWorldPoint(data.position);

        Vector2 dir = position - parentPosition;
        dir =  Math.Min(dir.magnitude, joystickRadius) * dir.normalized;

        knob.transform.position = parentPosition + dir;
    }

    public static void BindEvent(GameObject obj, Action<PointerEventData> action, Defines.UIEvent type = Defines.UIEvent.Click)
    {
        UIEventHandler eventHandler = Utils.GetOrAddComponent<UIEventHandler>(obj);

        switch (type)
        {
            case Defines.UIEvent.Click:
                eventHandler.OnClickHandler -= action;
                eventHandler.OnClickHandler += action;
                break;
            case Defines.UIEvent.Drag:
                eventHandler.OnDragHandler -= action;
                eventHandler.OnDragHandler += action;
                break;
            case Defines.UIEvent.Down:
                eventHandler.OnDownHandler -= action;
                eventHandler.OnDownHandler += action;
                break;
            case Defines.UIEvent.Up:
                eventHandler.OnUpHandler -= action;
                eventHandler.OnUpHandler += action;
                break;
        }
    }
    
    public class UIEventHandler : MonoBehaviour, IPointerClickHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
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
