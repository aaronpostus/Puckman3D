using UnityEngine;
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

        public List<GameObject> pellets;
        public List<string> levels;
        public static int playerScore;
        public static int remainingLives;
        [SerializeField] private MovementControl movementController;
        [SerializeField] private SoundManager soundManager;

        private int ghostMultiplier;

        public enum CameraModes : int { Isometric = 0, TopDown = 1, Mixed = 2 };
        public enum Gamemodes : int { Story = 0, Endless = 1 };
        public enum Gamestate : int { Loading = 0, GamePlay = 1 };
        public static int selectedCameraMode = (int) CameraModes.Isometric;
        public static int selectedGameMode = (int) Gamemodes.Story;
        public int currentGameState = (int) Gamestate.Loading;
        // for story mode, this is the actual level we are on (LevelX scene)
        // for infinite mode, this is the total number of levels we have completed
        // (not indicative of the current scene)
        public static int currentLevel = 0;
        private LevelController levelManager = null;
        private List<Ghost> ghostsInCurrentLevel = new List<Ghost>();
        // singleton instance
        private static GameManager instance = null;
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            }
            set {
                instance = value;
            }
        }
        public GameManager() {
            levels = new List<string>();

            levels.Add("Level1Final");
            levels.Add("Level2");
            levels.Add("Level2");
            pellets = new List<GameObject>();
         
        }
        private void Start()
        {
            playerScore = 0;
            remainingLives = 4;
            inputScheme = new PacmanInputs();
            movementController.Initialize(inputScheme.Pacman.Movement);

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

       public void StartNextLevel() {
            currentLevel++;
            ghostsInCurrentLevel = new List<Ghost>();
            if(selectedGameMode == (int) Gamemodes.Story) {
                SceneManager.LoadScene(levels[currentLevel - 1], LoadSceneMode.Single);
            }
            else {
                SceneManager.LoadScene(levels[Random.Range(0,levels.Count)]);
            }
            InitializeCurrentLevel();
       }
        private void InitializeCurrentLevel() {
            Scene scene = SceneManager.GetSceneByName(levels[currentLevel-1]);
            GameObject[] objectsInScene = scene.GetRootGameObjects();
            PauseLevel();
            foreach(GameObject gameObject in objectsInScene) {
                if(gameObject.name.Equals("LevelPrefab")) {
                    this.levelManager = gameObject.GetComponent<LevelController>();
                    break;
                }
            }
        }
        public void PauseLevel() {
            FreezePacman(true);
            FreezeGhosts(true);
        }
        // invoked by the UI once the countdown completes.
        public void StartLevel() {
            FreezePacman(false);
            FreezeGhosts(false);
        }
        public void ResetGhostAndPacmanPositions() {
            Vector3 oldPacManPosition = MovementControl.playerTransform.position;
            if(GameManager.selectedGameMode == (int) GameManager.Gamemodes.Story) {
                MovementControl.playerTransform.position = new Vector3();
            }

            
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
            if (currentGameState == (int) Gamestate.GamePlay && pellets.Count == 0)
            {
                LevelWon();
            }
            //Update UI Elements
        }
    }
}
