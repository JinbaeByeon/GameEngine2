using System.Collections;
using System.Collections.Generic;
using Defense.Manager;
using Defense.Monster;
using UnityEngine;
using UnityEngine.UI;

public class MonsterCount : MonoBehaviour
{
    public static int MonsterCnt = 0;
    public Text text_MosnterCount;
    public static int KillCnt = 0;

    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.On("onGameReStart",OnGameReStart);
    }

    private void OnGameReStart(object param)
    {
        MonsterCnt = 0;
        KillCnt = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (KillCnt >= 0) 
            text_MosnterCount.text = "남은 몬스터 : " + (MonsterCnt-KillCnt) + " / 20";
        if (KillCnt == 20)
        {
            EventManager.Instance.Emit("onGameBoss",null);
            boss.SetActive(true);
            KillCnt = -1;
        }
    }
}
