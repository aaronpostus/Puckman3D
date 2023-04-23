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
        [SerializeField] private SoundManager soundManager;
        private PacmanInputs inputScheme;

        public List<GameObject> pellets;

        public int playerScore;
        public int remainingLives; 
        private int ghostMultiplier;
        public TextMeshProUGUI scoreText;

        public static GameManager Instance { get; private set; }
        private void Awake() 
        {  
            pellets = new List<GameObject>();
            inputScheme = new PacmanInputs();
            movementController.Initialize(inputScheme.Pacman.Movement);
                
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this; 
            } 
        }

        private void Start()
        {

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

        
        public void ConsumePellet(GameObject gameObject)
        {
            playerScore += 10;
            pellets.Remove(gameObject);
            soundManager.PlaySound("Eat");

        }


        // This method will be called whenever a power pellet is consumed
        public void ConsumePowerPellet()
        {
            playerScore += 10;
            //Chase Ghosts

        }

        //  This method will be called whenever pacman consumes a ghost. 
        public void ConsumeGhost()
        {
            playerScore += (200 * ghostMultiplier);


        }

       

        //  Method to advance when the player beats the level
        public void LevelWon()
        {

           //Load the scene for the new level


        }

        //  Method called when the player dies
        public void OnPlayerDeath()
        {
            if (remainingLives > 0)
            {
                ResetLevel();
             
                
            }
            else
            {
                GameOver();
            }


        }


        //  Method to reset the level
        public void ResetLevel()
        {
            remainingLives--;
            playerScore = 0;


        }


        //  Method to change scene when the game is over
        public void GameOver()
        {

            //Change the scene

        }



        public void Update()
        {
            if (pellets.Count == 0)
            {
                LevelWon();
            }
            //Update UI Elements
        }
    }
}
