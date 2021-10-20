using UnityEngine;

namespace Defense.Player
{
    public class Player : MonoBehaviour
    {
        // Start is called before the first frame update
        public float speed = 10;
        void Start()
        {
    
        }

        // Update is called once per frame
        void Update()
        {
            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");
            var vec = new Vector3(horizontalInput,0,verticalInput);        
        
            transform.rotation = Quaternion.LookRotation(vec);
            transform.Translate(vec*Time.deltaTime*speed,Space.World);
        }
    }
}
