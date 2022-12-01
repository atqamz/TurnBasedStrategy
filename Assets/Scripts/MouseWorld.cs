using UnityEngine;

namespace TurnBasedStrategy
{
    public class MouseWorld : MonoBehaviour
    {
        public static MouseWorld instance;
        [SerializeField] private LayerMask mouseWorldLayerMask;

        private void Awake()
        {
            instance = this;
        }


        public static Vector3 GetPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, instance.mouseWorldLayerMask);
            return hitInfo.point;
        }
    }
}