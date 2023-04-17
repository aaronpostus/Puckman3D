using UnityEngine;
using PacmanInput;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

namespace OttiPostLewis.Lab6
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private MovementControl movementController;
        private PacmanInputs inputScheme;

        public List<GameObject> pellets;

        public int playerScore;
        public int remainingLives;
        private int ghostMultiplier;
        public TextMeshProUGUI scoreText;

        private void Awake()
        {
            pellets = new List<GameObject>();
            inputScheme = new PacmanInputs();
            movementController.Initialize(inputScheme.Pacman.Movement);
        }

        private void Start()
        {

            // Initialize the coin count to 0
            playerScore = 0;
            scoreText.text = "Score: " + playerScore;
            remainingLives = 4;

        }

        private void OnEnable()
        {
            var _ = new QuitHandler(inputScheme.Pacman.Quit);
        }

        public void AddPellet(GameObject gameObject)
        {
            pellets.Add(gameObject);

        }

        // This method will be called whenever a coin is picked up by the player
        public void ConsumePellet(GameObject gameObject)
        {
            playerScore += 10;
            pellets.Remove(gameObject);
           
        }


        // This method will be called whenever a coin is picked up by the player
        public void ConsumePowerPellet()
        {
            playerScore += 10;

        }


        public void ConsumeGhost()
        {
            playerScore += (200 * ghostMultiplier);


        }

        //Method to change scene when the game is over
        public void GameOver()
        {

           

        }


        public void OnPlayerDeath()
        {
            if (remainingLives > 0)
            {
                remainingLives--;
                playerScore = 0;
            }


        }

     

        public void Update()
        {
            scoreText.text = "Coins: " + playerScore;
        }
    }
}
