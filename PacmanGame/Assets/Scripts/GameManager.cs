﻿using UnityEngine;
using PacmanInput;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

namespace OttiPostLewis.Lab6
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] GameObject UIPrefab;
        private PacmanInputs inputScheme;
        public List<string> levels;
        public static int playerScore;
        public static int remainingLives;
        //[SerializeField] private MovementControl movementController;
        private MovementControl movementController;
        [SerializeField] private SoundManager soundManager;

        private int ghostMultiplier;

        public enum CameraModes : int { Isometric = 0, TopDown = 1, Mixed = 2 };
        public enum Gamemodes : int { Story = 0, Endless = 1 };
        public enum Gamestate : int { Loading = 0, GamePlay = 1 };
        public static int selectedCameraMode = (int) CameraModes.Isometric;
        public static int selectedGameMode = (int) Gamemodes.Story;
        public static int currentGameState = (int) Gamestate.Loading;
        // for story mode, this is the actual level we are on (LevelX scene)
        // for infinite mode, this is the total number of levels we have completed
        // (not indicative of the current scene)
        public static int currentLevel = 0;
        private static LevelController levelManager = null;
        public GameManager() {
            levels = new List<string>();

            levels.Add("Level1Final");
            levels.Add("Level2");
            levels.Add("Level2");

        }

        private void Awake()
        {
            inputScheme = new PacmanInputs();
            movementController.Initialize(inputScheme.Pacman.Movement);
            DontDestroyOnLoad(this);

        }
        private void Start()
        {
            playerScore = 0;
            remainingLives = 4;
           

        }

        private void OnEnable()
        {
            var _ = new QuitHandler(inputScheme.Pacman.Quit);
        }
        
        public void ConsumePellet(GameObject gameObject)
        {
            playerScore += 10;
            soundManager.PlaySound("Eat");

        }


        // This method will be called whenever a power pellet is consumed
        public void ConsumePowerPellet(GameObject gameObject)
        {
            playerScore += 10;
            //Chase Ghosts

        }

        //  This method will be called whenever pacman consumes a ghost. 
        public void ConsumeGhost()
        {
            playerScore += (200 * ghostMultiplier);

        }

       public void StartNextLevel() {
            currentLevel++;
            if(selectedGameMode == (int) Gamemodes.Story) {
                SceneManager.LoadScene(levels[currentLevel - 1], LoadSceneMode.Single);
            }
            else {
                SceneManager.LoadScene(levels[Random.Range(0,levels.Count)]);
            }
            movementController = GameObject.Find("PacmanPrefab").transform.GetChild(0).GetComponent<MovementControl>();
            InitializeCurrentLevel();
       }
        private void InitializeCurrentLevel() {
            GameObject levelPrefab = GameObject.Find("LevelPrefab");
            GameManager.levelManager = levelPrefab.GetComponent<LevelController>();
            Debug.Log("added levelcontroller");
        }
        public void PauseLevel() {
            currentGameState = (int) Gamestate.Loading;
            FreezePacman(true);
            FreezeGhosts(true);
        }
        // invoked by the UI once the countdown completes.
        public void StartLevel() {
            currentGameState = (int) Gamestate.GamePlay;
            FreezePacman(false);
            FreezeGhosts(false);
        }
        public void ResetGhostAndPacmanPositions() {
            levelManager.ResetGhostAndPacmanPositions();
            
        }
        public void FreezePacman(bool shouldFreeze) {
            MovementControl.canMove = !shouldFreeze;
        }
        public void FreezeGhosts(bool shouldFreeze) {
            Ghost.canMove = !shouldFreeze;
        }
        //  Method to advance when the player beats the level
        public void LevelWon()
        {
            currentGameState = (int) Gamestate.Loading;
            PauseLevel();
            Debug.Log("Level won!");
           //Load the scene for the new level


        }
        //  Method called when the player dies
        public void Die()
        {
            soundManager.PlaySound("Death");
            PauseLevel();
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
            Debug.Log("gm update");
            if (currentGameState == (int) Gamestate.GamePlay)
            {
                Debug.Log(GameManager.levelManager != null);
                /**if(levelManager.NumberOfPelletsInLevel() == 0) {

                    LevelWon();
                }**/
            }
            //Update UI Elements
        }
    }
}
