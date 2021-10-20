using System.Collections;
using System.Collections.Generic;
using Defense.Manager;
using TMPro;
using UnityEngine;

namespace Defense.Ui
{
    public class GameStatePrinter : MonoBehaviour
    {
        private TMP_Text text;
        
        private void Awake()
        {
            text = GetComponent<TMP_Text>();
        }
        
        // Update is called once per frame
        void Update()
        {
            text.text = GameManager.Instance.state.ToString();
        }
    }
}
