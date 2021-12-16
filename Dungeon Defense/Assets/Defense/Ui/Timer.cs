using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Defense.Manager;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float LimitTime;
    public Text text_Timer;
    
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.On("onGameReStart",OnGameReStart);
        EventManager.Instance.On("onGameBoss",OnGameBoss);
    }

    private void OnGameReStart(object param)
    {
        LimitTime = 60;
    }

    private void OnGameBoss(object param)
    {
        LimitTime = 60;
    }
    // Update is called once per frame
    void Update()
    {
        if(LimitTime>0)
            LimitTime -= Time.deltaTime;
        
        text_Timer.text = "남은 시간 : " + Mathf.Round(LimitTime);
        if (LimitTime < 0)
        {
            LimitTime = 0;
            EventManager.Instance.Emit("onGameEnd",null);
        }
    }
}
