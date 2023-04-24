using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace OttiPostLewis.Lab6
{
    public class Pellet : MonoBehaviour
    {
        [SerializeField] bool superPellet;
        private GameManager gameManager;
        void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(superPellet) {
                gameManager.ConsumePowerPellet(gameObject);
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

