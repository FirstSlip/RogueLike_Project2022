using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    public Item item;
    private ItemDataBase IDB;
    public int id;

    void Awake()
    {
        IDB = GameObject.Find("Canvas").GetComponent<ItemDataBase>();
    }
    void Start()
    {
        writeInfo();
    }

    public void writeInfo()
    {
        item = IDB.GetItem(id);
    }
}
