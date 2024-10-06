using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public ItemBase setItemData;
    [SerializeField] private SpriteRenderer displayItemImage;

    private void Awake()
    {
        //setItemData = ItemManager.Instance.RandomItemRetrun();
    }

    private void Start()
    {
        //displayItemImage.gameObject.SetActive(false);

        SetAndShowItem();
    }

    public void SetAndShowItem()
    {
        displayItemImage.gameObject.SetActive(true);
        displayItemImage.sprite = setItemData.itemIcon;
    }

    public void VacateItemSlot()
    {
        displayItemImage.gameObject.SetActive(false);
        setItemData = null;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (setItemData == null) { return; }

        if (col.gameObject.CompareTag("Player"))
        {
            _ = StartCoroutine(col.gameObject.GetComponent<PlayerController>().GetItemMotions(setItemData.itemIcon));

            setItemData.ItemGain();

            ItemManager.Instance.AddCurrentItems(setItemData);

            VacateItemSlot();
        }
    }
}
