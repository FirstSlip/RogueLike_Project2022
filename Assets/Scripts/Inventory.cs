using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject container;
    public GameObject[] slots;

    void Awake()
    {
        slots = new GameObject[container.GetComponent<Transform>().childCount];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = container.GetComponent<Transform>().GetChild(i).gameObject;
        }
    }

    public void AddItem(string name)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].GetComponent<SlotData>().isFull)
            {
                Item item = GameObject.Find("Canvas").GetComponent<ItemDataBase>().GetItem(name);

                GameObject prefab = (GameObject)Resources.Load(@"Prefabs/ItemCell");
                var instantiatedItemCell = Instantiate(prefab) as GameObject;

                instantiatedItemCell.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Items/" + item.title);
                instantiatedItemCell.name = name;

                var itemCellTrans = instantiatedItemCell.GetComponent<RectTransform>();
                itemCellTrans.SetParent(slots[i].GetComponent<RectTransform>(), false);
                itemCellTrans.localPosition = new Vector3(0, 0, 1);

                slots[i].GetComponent<SlotData>().isFull = true;
                slots[i].GetComponent<SlotData>().id = item.id;
                break;
            }
        }
    }
}
