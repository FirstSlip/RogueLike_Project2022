using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject container;
    public GameObject[] slots;
    public GameObject equipment;
    public SkillTree sTree;
    public ItemDataBase IDB;

    void Awake()
    {
        slots = new GameObject[container.GetComponent<Transform>().childCount];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = container.GetComponent<Transform>().GetChild(i).gameObject;
        }
        AddItem(0);
        AddItem(1);
    }

    private void Update()
    {
        int count = equipment.transform.childCount;

        for (int i = 0; i < count; i++)
        {
            GameObject slot = equipment.transform.GetChild(i).gameObject;
            if (slot.transform.childCount == 1)
            {
                var currentItemStats = slot.transform.GetChild(0).gameObject.GetComponent<ItemInfo>().item.stats;
                foreach(var item in currentItemStats)
                {
                    if (item.Key == "Health") sTree.healthBuff = item.Value;
                    if (item.Key == "Strength") sTree.strengthBuff = item.Value;
                    if (item.Key == "Dexterity") sTree.dexterityBuff = item.Value;
                    if (item.Key == "Intelligence") sTree.intelligenceBuff = item.Value;
                }
            }
        }
    }
    public void AddItem(int id)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].GetComponent<SlotData>().isFull)
            {
                Item item = GameObject.Find("Canvas").GetComponent<ItemDataBase>().GetItem(id);

                GameObject prefab = (GameObject)Resources.Load(@"Prefabs/ItemCell");
                var instantiatedItemCell = Instantiate(prefab) as GameObject;
                ItemInfo info = instantiatedItemCell.GetComponent<ItemInfo>();

                instantiatedItemCell.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Items/" + item.title);
                instantiatedItemCell.name = GameObject.Find("Canvas").GetComponent<ItemDataBase>().GetItem(id).title;
                info.id = id;
                info.item = GameObject.Find("Canvas").GetComponent<ItemDataBase>().GetItem(id);

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
