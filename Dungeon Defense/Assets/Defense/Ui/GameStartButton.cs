using System.Collections;
using System.Collections.Generic;
using Defense.Manager;
using UnityEngine;

namespace  Defense.Ui
{
   public class GameStartButton : MonoBehaviour
   {
      public void GameStart()
      {
         EventManager.Instance.Emit("onGameStart",null);
      }
   }
}
