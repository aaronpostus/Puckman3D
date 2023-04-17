
using System.Collections;
using PacmanInput;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OttiPostLewis.Lab6
{
    public class MovementControl : MonoBehaviour
    {

        [SerializeField] private GameObject playerToMove;
        public float moveTime = 0.5f;
        private PacmanInputs inputScheme;
        float playerSpeed = 4f;
        float currentRotation;
        public float raySize;
        public float gridSize = 1f;
        private Transform playerTransform;
        private bool isMoving = false; // flag to check if player is moving
        private Vector3 targetPosition; 
        private void Awake()
        {
            inputScheme = new PacmanInputs();
            playerTransform = playerToMove.transform;
            raySize = 1f;



        }
        public void Initialize(InputAction moveAction)
        {

        }

        private void Update()
        {
            // initialize rays in all four directions
            Ray rayRight = new Ray(playerTransform.position, playerTransform.right);
            Ray rayLeft = new Ray(playerTransform.position, -playerTransform.right);
            Ray rayForward = new Ray(playerTransform.position, playerTransform.forward);
            Ray rayBackward = new Ray(playerTransform.position, -playerTransform.forward);

            bool canMoveForward = !Physics.Raycast(rayForward, raySize);
            bool canMoveBackward = !Physics.Raycast(rayBackward, raySize);
            bool canMoveRight = !Physics.Raycast(rayRight, raySize);
            bool canMoveLeft = !Physics.Raycast(rayLeft, raySize);

            Debug.DrawRay(rayRight.origin, rayRight.direction * raySize, canMoveRight ? Color.white : Color.red);
            Debug.DrawRay(rayLeft.origin, rayLeft.direction * raySize, canMoveLeft ? Color.white : Color.red);
            Debug.DrawRay(rayForward.origin, rayForward.direction * raySize, canMoveForward ? Color.white : Color.red);
            Debug.DrawRay(rayBackward.origin, rayBackward.direction * raySize, canMoveBackward ? Color.white : Color.red);

            if (!isMoving)
            {
                Vector3 direction = Vector3.zero;
                if (Input.GetKey(KeyCode.UpArrow) && canMoveForward && !isMoving)
                {
                    direction = Vector3.forward;
                    currentRotation = 0f;
                }
                else if (Input.GetKey(KeyCode.DownArrow) && canMoveBackward && !isMoving)
                {
                    
                    direction = Vector3.back;
                    currentRotation = 180f;
                }
                else if (Input.GetKey(KeyCode.LeftArrow) && canMoveLeft && !isMoving)
                {
                    direction = Vector3.left;
                    currentRotation = -90f;
                }
                else if (Input.GetKey(KeyCode.RightArrow) && canMoveRight && !isMoving)
                {
                    direction = Vector3.right;
                    currentRotation = 90f;
                }

                playerTransform.rotation = Quaternion.Euler(0f, currentRotation, 0f);

                if (direction != Vector3.zero)
                {
                    targetPosition = playerTransform.position + (direction * gridSize);
                    StartCoroutine(MovePlayer());
                }
            }
        }




        private IEnumerator MovePlayer()
        {
            isMoving = true;
            float elapsedTime = 0f;
            Vector3 startingPosition = playerTransform.position;

            while (elapsedTime < moveTime)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / moveTime);
                playerTransform.position = Vector3.Lerp(startingPosition, targetPosition, t);
                yield return null;
            }

            playerTransform.position = targetPosition;
            isMoving = false;
        }

    }
}