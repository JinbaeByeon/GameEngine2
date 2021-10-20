using UnityEngine;

namespace Input_System
{
    public class DisableObj : MonoBehaviour
    {
        public float disableTime;

        private void OnEnable()
        {
            CancelInvoke();
            Invoke("OnDisable",disableTime);   
        }

        private void OnDisable()
        {
            gameObject.SetActive(false);
        }
    }
}
