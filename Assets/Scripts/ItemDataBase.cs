using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDataBase : MonoBehaviour
{
    public List<Item> items = new List<Item>(); //Лист со всеми предметами в игре

    private void Awake()
    {
        BuildDataBase();
    }

    public Item GetItem(int id)
    {
        return items.Find(item => item.id == id);
    }

    public Item GetItem(string itemName)
    {
        return items.Find(item => item.title == itemName);
    }

    public GameObject CreateItem(Vector3 pos)
    {
        var rnd = new System.Random();
        GameObject prefab = (GameObject)Resources.Load(@"Prefabs/Item");
        int id = rnd.Next(0, items.Count);
        Item currentItem = GetItem(2);

        var instantiatedItem = Instantiate(prefab) as GameObject;

        instantiatedItem.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(@"Sprites/Items/" + currentItem.title);
        instantiatedItem.name = currentItem.title;

        var itemTrans = instantiatedItem.GetComponent<RectTransform>();
        itemTrans.position = new Vector3(pos.x, pos.y, 0);
        itemTrans.localScale = new Vector3(0.4f, 0.4f, 1);

        return instantiatedItem;
    }

    void BuildDataBase() // создаем базу предметов.
    {
        items = new List<Item>()
        {
            //new Item(0, "Health potion", "Самый обычный каменный меч.",
            //new Dictionary<string, int>
            //{
            //    {"Attack", 10 }
            //}),

            //new Item(1, "Diamond Sword", "Мощный алмазный меч.",
            //new Dictionary<string, int>
            //{
            //    {"Attack", 15 }
            //}),

            //new Item(4, "Stone Shield", "Каменный щит.",
            //new Dictionary<string, int>
            //{
            //    {"Defence", 10 }
            //}),

            new Item(2, "Heal Potion", "Исцелит все твои душевные раны.",
            new Dictionary<string, int>
            {
                {"Heal", 20 }
            }),

            //new Item(3, "Old Shoe", "Мусор, но можешь продать.",
            //new Dictionary<string, int>
            //{
            //    {"Value", 30 }
            //})
        };
    }
}
