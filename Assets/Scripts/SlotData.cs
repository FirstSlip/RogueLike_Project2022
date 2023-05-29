using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotData : MonoBehaviour, IDropHandler
{
    public int id;
    public bool isFull = false;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().SetParent(GetComponent<RectTransform>(), false);
            eventData.pointerDrag.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 1);
        }
        id = eventData.pointerDrag.GetComponent<ItemInfo>().id;
    }
}
