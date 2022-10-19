using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTrasform;
    private CanvasGroup canvasGroup;
    
    private void Awake()
    {
        rectTrasform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        rectTrasform.localPosition = new Vector3(0, 0, 1);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTrasform.anchoredPosition += eventData.delta / GameObject.Find("Canvas").GetComponent<Canvas>().scaleFactor /
        GameObject.Find("Menu").GetComponent<Transform>().localScale.x;
    }
}
