using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace OttiPostLewis.Lab6 {
        // authored by aaron post
    public class PacmanAnimation : MonoBehaviour
    {
        [SerializeField] float maxY, middleY, minY, speed, eatSpeed, endlessMinX, storyMinX;
        [SerializeField] GameObject pacman;
        private MouseInput mouseInput;
        private InputAction mousePosition, mouseClick;
        private Transform transform;
        private enum State : int { PacmanCursorFollow = 0, GamemodeSelected = 1 };
        private int state, selectedGame;
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
            selectedGame = (int) GameManager.Gamemodes.Story;
            //added so compiler doesnt complain
            animationTime = 0f;
            //transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
        }

        // Update is called once per frame
        void Update()
        {
            if(state == (int) State.PacmanCursorFollow) {
                if(mouseClick.WasPressedThisFrame()) {
                    state = (int) State.GamemodeSelected;
                    if(selectedGame == (int) GameManager.Gamemodes.Story) {
                        transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
                        animationTime = 3.5f;
                    }
                    else {
                        transform.position = new Vector3(transform.position.x, minY, transform.position.z);
                        animationTime = 4f;
                    }
                }
                else {
                    float y = mousePosition.ReadValue<Vector2>().y;
                    Vector3 position = transform.position;
                    // move up
                    if(y > middleY) {
                        if(transform.position.y < maxY) {
                            transform.position = new Vector3(position.x, position.y + (speed * Time.deltaTime), position.z);
                        }
                        selectedGame = (int) GameManager.Gamemodes.Story;
                    }
                    // move down 
                    else if (y < middleY) {
                        if(transform.position.y > minY) {
                            transform.position = new Vector3(position.x, position.y - (speed * Time.deltaTime), position.z);
                        }
                        selectedGame = (int) GameManager.Gamemodes.Endless;
                    }
                }
            }
            else {
                if(animationTime <= 0f) {
                    // start game
                    GameManager.selectedGameMode = selectedGame;
                    SceneManager.LoadScene("CameraSelect", LoadSceneMode.Single);
                } else {
                    if(selectedGame == (int) GameManager.Gamemodes.Story && transform.position.x > storyMinX) {
                        transform.position = new Vector3(transform.position.x - (eatSpeed * Time.deltaTime), transform.position.y, transform.position.z);
                    }
                    else if(selectedGame == (int) GameManager.Gamemodes.Endless && transform.position.x > endlessMinX) {
                        transform.position = new Vector3(transform.position.x - (eatSpeed * Time.deltaTime), transform.position.y, transform.position.z);
                    }
                    animationTime -= Time.deltaTime;
                }
            } 
        }
        void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject);
        }
    }
}