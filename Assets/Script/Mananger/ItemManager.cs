using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    [SerializeField] private List<ItemBase> itemList;

    [SerializeField] private List<ItemBase> currentItemList;


    private void Awake()
    {

    }

    void Start()
    {
        InitItemList();
    }

    public void InitItemList()
    {
        var itemArray = Resources.LoadAll<ItemBase>("ItemSO");
        if (itemArray == null) { return; }
        for (int i = 0; i < itemArray.Length; i++)
        {
            itemList.Add(itemArray[i]);
        }
    }

    public void AddCurrentItems(ItemBase addItem)
    {
        currentItemList.Add(addItem);
    }

    public ItemBase RandomItemRetrun()
    {
        ItemBase item;

        while (true)
        {
            var randNumber = UnityEngine.Random.Range(0, itemList.Count);

            if (currentItemList.Contains(itemList[randNumber]))
            {
                continue;
            }
            else
            {
                item = itemList[randNumber];
                break;
            }
        }

        return item;
    }


}
