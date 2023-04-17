using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OttiPostLewis.Lab6
{
    public class EventManager : MonoBehaviour
    {
        public int playerScore;
        public int remainingLives;
        public TextMeshProUGUI scoreText;


        private int ghostMultiplier;


        private void Start()
        {
            // Initialize the coin count to 0
            playerScore = 0;
            scoreText.text = "Score: " + playerScore;
            remainingLives = 4;

        }

        // This method will be called whenever a coin is picked up by the player
        public void ConsumePellet()
        {
            playerScore += 10; 

        }


        // This method will be called whenever a coin is picked up by the player
        public void ConsumePowerPellet()
        {
            playerScore += 10;

        }


        public void ConsumeGhost()
        {
            playerScore += 200;


        }


        //Method to update the text displayed on screen
        private void UpdateCoinText()
        {
            scoreText.text = "Coins: " + playerScore;
        }

        //Method to change scene when the game is over
        public void GameOver()
        {

            SceneManager.LoadScene("GameOver");

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