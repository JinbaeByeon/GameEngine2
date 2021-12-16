using System;
using Defense.Manager;
using Unity.VisualScripting;
using UnityEngine;

namespace Defense.Ui
{
    public class RestartButton : MonoBehaviour
    {
        private void Start()
        {
            gameObject.SetActive(false);
            EventManager.Instance.On("onGameReStart",OnGameReStart);
            EventManager.Instance.On("onGameEnd",OnGameEnd);
        }

        public void RestartButtonDown()
        {
            EventManager.Instance.Emit("onGameReStart",null);
        }

        void OnGameReStart(object param)
        {
            gameObject.SetActive(false);
        }
        
        void OnGameEnd(object param)
        {
            gameObject.SetActive(true);
        }

    }
}
