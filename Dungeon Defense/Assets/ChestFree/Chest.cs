using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public ItemIcon itemIconPrefab;
    public Transform target;

    public Canvas UICanvas;
    public Transform chestUITransform;
    public ItemDatabase itemDatabase;
    public List<string> Items;

    public GameObject player;
    private static readonly int shop = Animator.StringToHash("shop");

    private ItemSlot[] _itemSlots;

    private void Awake()
    {
        _itemSlots = chestUITransform.GetComponentsInChildren<ItemSlot>();
    }

    void Start()
    { 
        UICanvas.gameObject.SetActive(false);

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
            //print("가깝다");
            if (Input.GetKeyDown(KeyCode.E))
                UICanvas.gameObject.SetActive(!UICanvas.gameObject.activeSelf);
            
            Animator playerAnimator = player.GetComponent<Animator>();
            if (UICanvas.gameObject.activeSelf == true)
            {
                playerAnimator.SetBool(shop,true);
            }
            else
            {
                playerAnimator.SetBool(shop,false);
            }
        }
        else
        {
            UICanvas.gameObject.SetActive(false);
        }
    }
    
    public float DistanceToPoint(Vector3 a, Vector3 b)
    {
        return (float)Math.Sqrt(Math.Pow(a.x - b.x, 2) + Math.Pow(a.z - b.z, 2));
    }
}
