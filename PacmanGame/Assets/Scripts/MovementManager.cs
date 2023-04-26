using UnityEngine;
using PacmanInput;

namespace OttiPostLewis.Lab6
{
    // authored by alex otti
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

