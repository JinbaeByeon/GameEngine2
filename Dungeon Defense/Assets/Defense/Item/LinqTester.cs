using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinqTester : MonoBehaviour
{
    public ItemDatabase ItemDatabase;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in ItemDatabase.ItemDatas)
        {
            // 이런식으로 제어 가능
            //print(item.itemName);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
