using System;
using System.Collections.Generic;
using UnityEngine;

namespace Defense.Manager
{
    public class EventManager : MonoBehaviour
    {
        private Dictionary<string, List<Action<object>>> _eventDatabase;
     
        public static EventManager Instance;
     
        private void Awake()
        {
            Instance = this;
            _eventDatabase = new Dictionary<string,List<Action<object>>>();
        }
        // Start is called before the first frame update
        public void On(string eventName,Action<object> action)
        {
            if(false == _eventDatabase.ContainsKey(eventName))
                _eventDatabase.Add(eventName,new List<Action<object>>());
            
            _eventDatabase[eventName].Add(action);
        }
    
        public void Emit(string eventName,object param)
        {
            if(false == _eventDatabase.ContainsKey(eventName))
                print($"{eventName}라는 이벤트가 존재하지 않습니다.");
            else if(_eventDatabase[eventName].Count>0)
            {
                foreach(var action in _eventDatabase[eventName])
                {
                    action.Invoke(param);
                }
            }
        }
    }
}
