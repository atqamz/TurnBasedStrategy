using UnityEngine;

namespace TurnBasedStrategy
{
    public class Unit : MonoBehaviour
    {
        private Vector3 targetPosition;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                SetTargetPosition(new Vector3(4, 0, 4));
            }

            MoveToTargetPosition();
        }

        private void MoveToTargetPosition()
        {
            float stopDistance = .1f;
            float moveSpeed = 4f;

            if (Vector3.Distance(transform.position, targetPosition) > stopDistance)
            {
                Vector3 moveDirection = (targetPosition - transform.position).normalized;
                transform.position += moveDirection * moveSpeed * Time.deltaTime;
            }
        }

        private void SetTargetPosition(Vector3 _targetPosition)
        {
            this.targetPosition = _targetPosition;
        }
    }
}