using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace OttiPostLewis.Lab6
{
    public class Pellet : MonoBehaviour
    {
        public List<GameObject> pellets;

        public GameManager gameManager;

   
        void Start()
        {
            gameManager = FindObjectOfType<GameManager>();

            gameManager.AddPellet(gameObject);

        }

        private void OnTriggerEnter(Collider other)
        {

            gameManager.ConsumePellet(gameObject);
            Destroy(gameObject);


        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

