using UnityEngine;

namespace TurnBasedStrategy
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private Animator unitAnimatorComponent;
        private Vector3 targetPosition;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SetTargetPosition(MouseWorld.GetPosition());
            }

            MoveToTargetPosition();
        }

        private void MoveToTargetPosition()
        {
            float stopDistance = .1f;
            float moveSpeed = 4f;
            float rotateSpeed = 10f;

            if (Vector3.Distance(transform.position, targetPosition) > stopDistance)
            {
                Vector3 moveDirection = (targetPosition - transform.position).normalized;
                transform.position += moveDirection * moveSpeed * Time.deltaTime;

                transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

                unitAnimatorComponent.SetBool("IsWalking", true);
            }
            else
            {
                unitAnimatorComponent.SetBool("IsWalking", false);
            }
        }

        private void SetTargetPosition(Vector3 _targetPosition)
        {
            this.targetPosition = _targetPosition;
        }
    }
}