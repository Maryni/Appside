using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DrugDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    #region Inspectora variables

    [SerializeField] private Canvas canvas;
    [SerializeField] private Text text;

    #endregion Inspectora variables

    #region private variables

    private RectTransform rectTransform;
    private Vector2 startTransform;
    private Vector2 endTransform;
    private bool callbackOnDoublejump;
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
        startTransform = rectTransform.anchoredPosition;
        endTransform = Vector2.zero;
    }

    private void FixedUpdate()
    { 
        //Cursor.lockState = CursorLockMode.None;
        if (drugStarted)
        {
            actionOnDraging?.Invoke();
        }

        //second try, but not working
        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);

        //    // obtain touch position
        //    Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

        //    // get touch to take a deal with
        //    switch (touch.phase)
        //    {
        //        // if you touches the screen
        //        case TouchPhase.Began:
        //            OnBeginDragFunction();
        //            break;

        //        // you move your finger
        //        case TouchPhase.Moved:
        //            OnDragFunction(touchPos);
        //            break;

        //        // you release your finger
        //        case TouchPhase.Ended:
        //            OnEndDragFunction();
        //            break;
        //    }
        //}
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

    //Down functions was called in EventTrigger component
    public void OnEndDragFunction()
    {
        endTransform = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = startTransform;
        actionOnEndDrug?.Invoke();
        drugStarted = false;
        text.text = rectTransform.anchoredPosition.ToString();
    }

    public void OnBeginDragFunction()
    {
        drugStarted = true;
        actionOnStartDrug?.Invoke();
        text.text = rectTransform.anchoredPosition.ToString();
    }

    public void OnDragFunction()
    {
        //not working
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (!(Camera.main is null))
            {
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                rectTransform.anchoredPosition += touchPos;
            }
        }

        endTransform = rectTransform.anchoredPosition;
        text.text = rectTransform.anchoredPosition.ToString();
    }

    public void OnDragFunction(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
        endTransform = rectTransform.anchoredPosition;
        text.text = eventData.delta.ToString();
    }

    public void OnDragFunction(Vector2 pos)
    {
        rectTransform.anchoredPosition += pos;
        endTransform = rectTransform.anchoredPosition;
        text.text = rectTransform.anchoredPosition.ToString();
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

    // private void OnMouseDrag()
    // {
    //     if (Input.touchCount > 0)
    //     {
    //         Touch touch = Input.GetTouch(0);
    //         Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
    //         OnDragFunction(touchPos);
    //     }
    //     text.text = rectTransform.anchoredPosition.ToString();
    // }

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
        text.text = rectTransform.anchoredPosition.ToString();
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrug");
        OnBeginDragFunction();
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrug");
        OnDragFunction(eventData);
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrug");
        OnEndDragFunction();
    }

    #endregion Interface realize
}