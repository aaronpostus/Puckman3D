using UnityEngine;
using PacmanInput;

namespace OttiPostLewis.Lab6
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private MovementControl movementController;
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
