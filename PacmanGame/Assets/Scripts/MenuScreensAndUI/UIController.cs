using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace OttiPostLewis.Lab6 {
    public class UIController : MonoBehaviour
    {
        private TextMeshProUGUI scoreUI, levelUI, livesUI, imageUI, countdownUIText, countdownUIValue;
        private GameManager gm;
        private float animationTime = 4f;
        private bool countingDown = true, pacmanDeathAnimation = false;
        private GameObject countdown, imageObj, countdownNum, deathObj;

        private TextMeshProUGUI GetNestedChildTextMesh(int childNo) {
            return transform.GetChild(0).gameObject.transform.GetChild(childNo).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }
        // Start is called before the first frame update
        void Start()
        {
            this.gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            this.levelUI = GetNestedChildTextMesh(0);
            this.scoreUI = GetNestedChildTextMesh(1);
            this.livesUI = GetNestedChildTextMesh(2);
            // countdown game obj
            this.countdown = transform.GetChild(1).gameObject;
            this.deathObj = transform.GetChild(1).transform.GetChild(3).gameObject;
            this.imageObj = countdown.transform.GetChild(0).gameObject;
            this.imageUI = imageObj.GetComponent<TextMeshProUGUI>();
            this.countdownUIText = countdown.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            this.countdownUIText = countdown.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

            this.countdownNum = countdown.transform.GetChild(2).gameObject;
            this.countdownUIValue = countdownNum.GetComponent<TextMeshProUGUI>();
        }
        void PacmanDeathAnimation() {
            animationTime = 6f;
            countingDown = true;
            deathObj.SetActive(true);
            pacmanDeathAnimation = true;
        }

        // Update is called once per frame
        void Update()
        {
            scoreUI.text = GameManager.playerScore + "";
            levelUI.text = GameManager.currentLevel + "";
            livesUI.text = GameManager.remainingLives + "";
            if(countingDown) {
                animationTime -= Time.deltaTime;
                if(pacmanDeathAnimation && animationTime < 4f) {
                    countdownNum.SetActive(true);
                    imageObj.SetActive(true);
                }
                if(animationTime > 1f) {
                    countdownUIValue.text = Mathf.Ceil(animationTime - 1) + "";
                }
                else if(animationTime > 0f){
                    countdownUIText.text = "GO! GO! GO! GO!";
                    gm.StartLevel();
                    countdownNum.SetActive(false);
                    imageObj.SetActive(false);
                    deathObj.SetActive(false);
                    pacmanDeathAnimation = false;
                }
                else {
                    countdown.SetActive(false);
                    countingDown = false;
                }
            }
        }
    }
}
