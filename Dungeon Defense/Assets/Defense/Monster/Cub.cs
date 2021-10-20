using UnityEngine;

namespace Defense.Monster
{
    public class Cub : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Invoke("Dead",3);   
        }

        void Dead()
        {
            gameObject.SetActive(false);
        }
    }
}
