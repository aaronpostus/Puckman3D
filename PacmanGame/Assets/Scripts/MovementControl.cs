using UnityEngine;
using UnityEngine.InputSystem;

namespace OttiPostLewis.Lab6
{
    public class MovementControl : MonoBehaviour
    {

        [SerializeField] private GameObject playerToMove;
        private InputAction moveAction;
        float playerSpeed = 4f;
        float raySize;
        private Transform playerTransform;
        private Vector3 forwardDirection, rightDirection, leftDirection, backwardDirection;
        private bool canMoveForward, canMoveBackward, canMoveRight, canMoveLeft;
        Quaternion targetRotation;

        public void Initialize(InputAction moveAction)
        {
            this.moveAction = moveAction;
            moveAction.Enable();
        }

        private void Start()
        {
            playerTransform = playerToMove.transform;
            raySize = 0.6f;
            targetRotation = Quaternion.identity;

            SetDirections();
            // Calculate direction vectors
      
        }

        private void SetDirections()
        {
            forwardDirection = playerTransform.forward;
            rightDirection = playerTransform.right;
            leftDirection = -playerTransform.right;
            backwardDirection = -playerTransform.forward;

        }


        private void LockMovement()
        {
            Ray rayRight = new Ray(playerTransform.position, rightDirection);
            Ray rayLeft = new Ray(playerTransform.position, leftDirection);
            Ray rayForward = new Ray(playerTransform.position, forwardDirection);
            Ray rayBackward = new Ray(playerTransform.position, backwardDirection);

            canMoveForward = !Physics.Raycast(rayForward, raySize);
            canMoveBackward = !Physics.Raycast(rayBackward, raySize);
            canMoveRight = !Physics.Raycast(rayRight, raySize);
            canMoveLeft = !Physics.Raycast(rayLeft, raySize);

            Debug.DrawRay(rayRight.origin, rightDirection * raySize, canMoveRight ? Color.white : Color.red);
            Debug.DrawRay(rayLeft.origin, leftDirection * raySize, canMoveLeft ? Color.white : Color.red);
            Debug.DrawRay(rayForward.origin, forwardDirection * raySize, canMoveForward ? Color.white : Color.red);
            Debug.DrawRay(rayBackward.origin, backwardDirection * raySize, canMoveBackward ? Color.white : Color.red);
        }

        private void Update()
        {
            LockMovement();

            Vector2 direction = moveAction.ReadValue<Vector2>();
            Vector3 movementDirection = Vector3.zero;

            if (direction.x > 0f && canMoveRight)
            {
                targetRotation = Quaternion.LookRotation(rightDirection);
                movementDirection = rightDirection;
            }
            else if (direction.x < 0f && canMoveLeft)
            {
                targetRotation = Quaternion.LookRotation(leftDirection);
                movementDirection = leftDirection;
            }
            else if (direction.y > 0f && canMoveForward)
            {
                targetRotation = Quaternion.LookRotation(forwardDirection);
                movementDirection = forwardDirection;
            }
            else if (direction.y < 0f && canMoveBackward)
            {
                targetRotation = Quaternion.LookRotation(backwardDirection);
                movementDirection = backwardDirection;
            }

            playerTransform.rotation = targetRotation;
            playerTransform.Translate(movementDirection * Time.deltaTime * playerSpeed, Space.World);
        }





    }
}