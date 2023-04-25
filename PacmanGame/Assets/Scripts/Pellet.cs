using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace OttiPostLewis.Lab6
{
    public class Pellet : MonoBehaviour
    {
        [SerializeField] bool superPellet;
        private GameManager gameManager;
        private GameObject pacmanObject;
        MovementControl movementControl;

        void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            
        }

        private void OnTriggerEnter(Collider other)
        {   
            if(superPellet) {

                gameManager.ConsumePowerPellet(gameObject);

                pacmanObject = GameObject.Find("PacmanPrefab/PacMan");
                movementControl = pacmanObject.GetComponent<MovementControl>();
                movementControl.CallFlee();

            } else {
                gameManager.ConsumePellet(gameObject);
            }

            Destroy(gameObject);

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

