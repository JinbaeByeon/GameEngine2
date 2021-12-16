using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterCount : MonoBehaviour
{
    public static int MonsterCnt = 0;
    public Text text_MosnterCount;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        text_MosnterCount.text = "남은 몬스터 : " + MonsterCnt + " / 20";
    }
}
