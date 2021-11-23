using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DrugDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    #region private variables

    private RectTransform rectTransform;
    private Vector2 startTransform;
    private Vector2 endTransform;
    private bool callbackOnDoublejump;
    private Movement movement;
    private UnityAction actionOnStartDrug;
    private UnityAction actionOnEndDrug;
    private UnityAction actionOnDraging;
    private bool drugStarted;

    #endregion private variables

    #region Unity functions

    private void OnValidate()
    {
        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }
    }

    private void Start()
    {
        if (movement == null)
        {
            movement = FindObjectOfType<Movement>();
        }
        startTransform = rectTransform.anchoredPosition;
        endTransform = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if (drugStarted)
        {
            actionOnDraging?.Invoke();
        }
    }

    #endregion Unity functions

    #region public functions

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

    public bool GetCallback()
    {
        return callbackOnDoublejump;
    }

    public void SetActionToStartDrug(UnityAction action)
    {
        actionOnStartDrug += action;
    }

    public void SetActionToEndDrug(UnityAction action)
    {
        actionOnEndDrug += action;
    }

    public void SetActionToDragging(UnityAction action)
    {
        actionOnDraging += action;
    }

    #endregion public functions

    #region private functions

    private IEnumerator Dragging()
    {
        actionOnDraging?.Invoke();
        yield return new WaitForFixedUpdate();
    }

    #endregion private functions

    #region Interface realize

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointDown");
        if (eventData.clickCount == 2)
        {
            callbackOnDoublejump = true;
            Debug.Log("double click");
        }
        else
        {
            callbackOnDoublejump = false;
        }
        actionOnDraging?.Invoke();
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrug");
        actionOnStartDrug?.Invoke();
        drugStarted = true;
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
        actionOnEndDrug?.Invoke();
        drugStarted = false;
    }

    #endregion Interface realize
}