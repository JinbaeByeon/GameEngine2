using System;
using System.Collections;
using System.Collections.Generic;
using DevionGames;
using Monster;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HpBarManager : MonoBehaviour
{
    public static HpBarManager Instance;
    
    [SerializeField] private Slider m_goPrefab;

    private List<GameObject> m_objectList = new List<GameObject>();
    private List<Slider> m_hpBarList = new List<Slider>();

    private Camera m_cam = null;
    // Start is called before the first frame update
    void Start()
    {
        m_cam = Camera.main;

        // GameObject[] tObjects = GameObject.FindGameObjectsWithTag("Rapid Monster");
        // for (int i = 0; i < tObjects.Length; ++i)
        // {
        //     Add(tObjects[i]);
        // }
        //
        // tObjects = GameObject.FindGameObjectsWithTag("Slow Monster");
        // for (int i = 0; i < tObjects.Length; ++i)
        // {
        //     Add(tObjects[i]);
        // }
    }

    private void Awake()
    {
        Instance = this;
    }

    public void Add(GameObject monster)
    {
        m_objectList.Add(monster);
        Slider tHpbar = Instantiate(m_goPrefab, monster.transform.position, Quaternion.identity, transform);
        m_hpBarList.Add(tHpbar);
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < m_objectList.Count; ++i)
        {
            if (m_objectList[i].CompareTag($"Rapid Monster"))
            {
                m_hpBarList[i].transform.position =
                    m_cam.WorldToScreenPoint(m_objectList[i].transform.position + new Vector3(0, 1.5f, 0));
                RapidMonster monster = m_objectList[i].ConvertTo<RapidMonster>();
                m_hpBarList[i].value = (float)monster.curHp / monster.maxHp;
            }
            else
            {
                m_hpBarList[i].transform.position =
                    m_cam.WorldToScreenPoint(m_objectList[i].transform.position + new Vector3(0, 4, 0));
                SlowMonster monster = m_objectList[i].ConvertTo<SlowMonster>();
                m_hpBarList[i].value = (float)monster.curHp / monster.maxHp;
            }

            if (m_objectList[i].activeSelf == false)
                m_hpBarList[i].transform.position = new Vector3(0, 100, 0);
        }
        
    }
}
