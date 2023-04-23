using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PacmanCamera : MonoBehaviour
{
    [SerializeField] float maxY, medY, minY, middleY1, middleY2, speed, eatSpeed, isometricX, topdownX, mixedX;
    [SerializeField] GameObject pacman;
    private MouseInput mouseInput;
    private InputAction mousePosition, mouseClick;
    private Transform transform;
    private enum State : int { PacmanCursorFollow = 0, CameraSelected = 1 };
    private enum CameraModes : int { Isometric = 0, TopDown = 1, Mixed = 2 };
    private int state, selectedCamera;
    private float animationTime;
    // Start is called before the first frame update
    void Start()
    {
        mouseInput = new MouseInput();
        mousePosition = mouseInput.Mouse.Position;
        mouseClick = mouseInput.Mouse.Click;
        mousePosition.Enable();
        mouseClick.Enable();
        state = (int) State.PacmanCursorFollow;
        transform = pacman.transform;
        selectedCamera = (int) CameraModes.Isometric;
        //added so compiler doesnt complain
        animationTime = 0f;
        //transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(state == (int) State.PacmanCursorFollow) {
            if(mouseClick.WasPressedThisFrame()) {
                state = (int) State.CameraSelected;
                if(selectedCamera == (int) CameraModes.Isometric) {
                    //transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
                    //animationTime = 3.5f;
                }
                //else if(selectedCamera )
                else {
                    transform.position = new Vector3(transform.position.x, minY, transform.position.z);
                    animationTime = 4f;
                }
            }
            else {
                float y = mousePosition.ReadValue<Vector2>().y;
                Vector3 position = transform.position;
                Debug.Log(y);
                // move up
                if(y > middleY1) {
                    if(transform.position.y < maxY) {
                        transform.position = new Vector3(position.x, position.y + (speed * Time.deltaTime), position.z);
                    }
                    selectedCamera = (int) CameraModes.Isometric;
                }
                else if(y > middleY2) {
                    if(transform.position.y < medY) {
                        transform.position = new Vector3(position.x, position.y + (speed * Time.deltaTime), position.z);
                    }
                    else if (transform.position.y > medY) {
                        transform.position = new Vector3(position.x, position.y - (speed * Time.deltaTime), position.z);
                    }
                    selectedCamera = (int) CameraModes.TopDown;
                }
                else {
                    if(transform.position.y > minY) {
                        transform.position = new Vector3(position.x, position.y - (speed * Time.deltaTime), position.z);
                    }
                    selectedCamera = (int) CameraModes.Mixed;
                }
            }
        }
        else {
            if(animationTime <= 0f) {
                // start game
            } else {
                /**if(selectedCamera == (int) CameraModes.Isometric && transform.position.x > storyMinX) {
                    transform.position = new Vector3(transform.position.x - (eatSpeed * Time.deltaTime), transform.position.y, transform.position.z);
                }
                else if(selectedCamera == (int) Gamemodes.Endless && transform.position.x > endlessMinX) {
                    transform.position = new Vector3(transform.position.x - (eatSpeed * Time.deltaTime), transform.position.y, transform.position.z);
                }
                animationTime -= Time.deltaTime;**/
            }
        } 
    }
    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
