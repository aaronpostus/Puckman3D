using UnityEngine;
using PacmanInput;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

namespace OttiPostLewis.Lab6
{

    public class MovementManager : MonoBehaviour
    {

        [SerializeField] MovementControl movementController;
        private PacmanInputs inputScheme;

        private void Awake()
        {
            inputScheme = new PacmanInputs();
            movementController.Initialize(inputScheme.Pacman.Movement);
        }

        private void OnEnable()
        {
            var _ = new QuitHandler(inputScheme.Pacman.Quit);
        }


    }
}

