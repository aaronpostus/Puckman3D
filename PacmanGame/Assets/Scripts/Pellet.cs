﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace OttiPostLewis.Lab6
{
    public class Pellet : MonoBehaviour
    {
        public List<GameObject> pellets;

        public GameManager gameManager;

        // Use this for initialization
        void Start()
        {
            gameManager = GameObject.FindObjectOfType<GameManager>();

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
