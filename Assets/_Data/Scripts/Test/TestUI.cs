using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TestUI : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerDownHandler
{
    [SerializeField] private Button a;

    private void Update()
    {
        if (Pointer.current != null && Pointer.current.press.wasPressedThisFrame)
            Debug.Log("POINTER PRESS detected: " + Pointer.current.device.name);

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            Debug.Log("MOUSE detected");

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            Debug.Log("TOUCH detected");

        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            Debug.Log("SPACE detected");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Up");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Down");
    }
}
