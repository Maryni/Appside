using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrugDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Vector2 startTransform;
    [SerializeField] private Vector2 endTransform;

    private void OnValidate()
    {
        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }
    }

    private void Start()
    {
        startTransform = rectTransform.anchoredPosition;
        endTransform = Vector2.zero;
    }

    public Vector2 GetStartPosition()
    {
        if (startTransform != Vector2.zero)
        {
            return startTransform;
        }
        else
            return Vector2.zero;
    }

    public Vector2 GetEndPosition()
    {
        if (endTransform != Vector2.zero)
        {
            return endTransform;
        }
        else
            return Vector2.zero;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointDown");
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrug");
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrug");
        rectTransform.anchoredPosition += eventData.delta;
        endTransform = rectTransform.anchoredPosition;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrug");
        endTransform = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = startTransform;
    }
}