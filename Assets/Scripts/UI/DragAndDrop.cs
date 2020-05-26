using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Drag and Drop Variables")]
    public Canvas Canvas;
    public bool IsDragging;

    [Header("Visible Variables")]
    [SerializeField] private GameObject DragObject;
    [SerializeField] private Vector3 InitialDragPosition;

    private Action<Vector3> GenerateObjectAction;

    void Start() { IsDragging = false; }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsDragging = true;
        InitialDragPosition = eventData.pointerPressRaycast.worldPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DragObject = Instantiate(gameObject, InitialDragPosition, Quaternion.identity, transform.parent);
        DragObject.transform.SetParent(Canvas.transform);
        DragObject.transform.SetAsLastSibling();
        Destroy(DragObject.GetComponent<DragAndDrop>());
    }

    public void OnDrag(PointerEventData eventData)
    {
        DragObject.transform.position = ((Vector3)eventData.position) / Canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        IsDragging = false;
        Destroy(DragObject);
        GenerateObjectAction(Camera.main.ScreenToWorldPoint(eventData.pointerCurrentRaycast.screenPosition));
    }

    public void SetGenerateObjectAction(Action<Vector3> _action) { GenerateObjectAction = _action; }
}
