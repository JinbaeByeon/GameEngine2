using UnityEngine;

namespace Defense
{
    public class MyGizmo : MonoBehaviour
    {
        public Color _color = Color.blue;
        public float _radius = 1f;

        private void OnDrawGizmos()
        {
            // 기즈모색 설정
            Gizmos.color = _color;
        
            // 구체 모양의 기즈모 생성
            Gizmos.DrawSphere(transform.position, _radius);
        }
    }
}
