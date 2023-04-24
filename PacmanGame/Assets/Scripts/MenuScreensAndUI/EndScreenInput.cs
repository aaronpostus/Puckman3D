using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace OttiPostLewis.Lab6 {
    public class EndScreenInput : MonoBehaviour
    {
        private MouseInput mouseInput;
        private InputAction mouseClick;
        // Start is called before the first frame update
        void Start()
        {
            mouseInput = new MouseInput();
            mouseClick = mouseInput.Mouse.Click;
            GameManager.Instance = null;
            SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}