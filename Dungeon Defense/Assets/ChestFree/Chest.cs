using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public ItemIcon itemIconPrefab;
    public Transform target;

    public GameObject UICanvas;
    public Transform chestUITransform;
    public ItemDatabase itemDatabase;
    public List<string> Items;

    private ItemSlot[] _itemSlots;

    private void Awake()
    {
        _itemSlots = chestUITransform.GetComponentsInChildren<ItemSlot>();
    }

    void Start()
    { 
        UICanvas.SetActive(false);

        var containItems = Items
            .Select(itemName => itemDatabase.ItemDatas
                .FirstOrDefault(item => item.itemName == itemName))
            .ToList();

        for (var i = 0; i < containItems.Count(); i++)
        {
            var icon = Instantiate(itemIconPrefab, UICanvas.transform);
            icon.SetItemIcon(containItems[i].itemImage);
            icon.transform.position = _itemSlots[i].transform.position;

        }
    }
    
    void Update()
    {
        float toPlayerDistance = DistanceToPoint(target.position, transform.position);
        
        if (toPlayerDistance < 5f)
        {
            
            if (Input.GetKeyDown(KeyCode.E))
                UICanvas.SetActive(!UICanvas.activeSelf);
        }
        else
        {
            //print("멀다");
        }
    }
    
    public float DistanceToPoint(Vector3 a, Vector3 b)
    {
        return (float)Math.Sqrt(Math.Pow(a.x - b.x, 2) + Math.Pow(a.z - b.z, 2));
    }
}
