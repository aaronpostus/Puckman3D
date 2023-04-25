using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Debug = UnityEngine.Debug;

namespace OttiPostLewis.Lab6
{
    public class MovementControl : MonoBehaviour
    {

        [SerializeField] public GameObject playerToMove;
        public static bool canMove = false;
        public LayerMask layerMask;
        private InputAction moveAction;
        public static GameObject playerObj;
        [SerializeField] public Rigidbody rigidbody;
        float playerSpeed = 4f;
        float raySize;
        public static Transform playerTransform;
        private Vector3 forwardDirection, rightDirection, leftDirection, backwardDirection;
        private bool canMoveForward, canMoveBackward, canMoveRight, canMoveLeft;
        private GameManager gm;

        Quaternion targetRotation;

        //  Pacman States
        public enum PacmanState
        {
            Flee,
            Chase
        }
        public static PacmanState currentState;

        //  This method initializes the input action for moving the game object.
        public void Initialize(InputAction moveAction)
        {
            this.moveAction = moveAction;
            moveAction.Enable();
        
        }

        //  This method is called when the script instance is being loaded. It sets the player object and player transform variables.
        void Awake() {
            playerObj = playerToMove;
            playerTransform = playerToMove.transform;
        }

        private void Start()
        {
            raySize = 0.45f;
            gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            targetRotation = Quaternion.identity;
            currentState = PacmanState.Flee;
            SetDirections();
        }
        //  Check if Pacman collides with any of the NPC's
        private void OnTriggerEnter(Collider other)
        {
            if ((other.gameObject.name == "Blinky") || (other.gameObject.name == "Pinky") || (other.gameObject.name == "Inky") || (other.gameObject.name == "Clyde"))
            {
                    
                    if (currentState == PacmanState.Flee)
                    {
                    gm.Die();
                    }  
            }
        }

        // This method sets the current state of the game object to "Chase" and triggers the method below
        public void CallFlee()
        {
            currentState = PacmanState.Chase;
            float delay = 6.0f;
            StartCoroutine(ChangeStateAfterDelay(PacmanState.Flee, delay));
        }
        //  This method changes the current state of the game object after 6 seconds
        private IEnumerator ChangeStateAfterDelay(PacmanState newState, float delay)
        {
            //Debug.Log("Starting coroutine");
            yield return new WaitForSeconds(delay);
            //Debug.Log("Coroutine finished");

            currentState = newState;
        }


        
        // This method sets the directions based on the gameobjects transform
        private void SetDirections()
        {
            forwardDirection = playerTransform.forward;
            rightDirection = playerTransform.right;
            leftDirection = -playerTransform.right;
            backwardDirection = -playerTransform.forward;

        }
        //This method casts rays and sets booleans that are used to lock movement
        private void LockMovement()
        {
            Ray rayRight = new Ray(playerTransform.position, rightDirection);
            Ray rayLeft = new Ray(playerTransform.position, leftDirection);
            Ray rayForward = new Ray(playerTransform.position, forwardDirection);
            Ray rayBackward = new Ray(playerTransform.position, backwardDirection);

            //Booleans used to lock movement
            canMoveForward = !Physics.Raycast(rayForward, raySize);
            canMoveBackward = !Physics.Raycast(rayBackward, raySize);
            canMoveRight = !Physics.Raycast(rayRight, raySize);
            canMoveLeft = !Physics.Raycast(rayLeft, raySize);

            Debug.DrawRay(rayRight.origin, rightDirection * raySize, canMoveRight ? Color.white : Color.red);
            Debug.DrawRay(rayLeft.origin, leftDirection * raySize, canMoveLeft ? Color.white : Color.red);
            Debug.DrawRay(rayForward.origin, forwardDirection * raySize, canMoveForward ? Color.white : Color.red);
            Debug.DrawRay(rayBackward.origin, backwardDirection * raySize, canMoveBackward ? Color.white : Color.red);
        }

        //  This method reads the input direction for movement and sets the target rotation and movement direction of the
        // game object. It also updates the game object's transform accordingly.
        private void ReadMovementInput()
        {
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

        private void Update()
        {
           if (canMove) {

                LockMovement();

                ReadMovementInput();
            }
       }
    }
}