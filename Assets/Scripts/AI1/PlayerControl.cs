﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AI1
{
    [AddComponentMenu("AI/Player Control")]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerControl : MonoBehaviour
    {
        #region Variables
        [Header("Reference Variables")]
        public Rigidbody2D _playerRigidbody;
        [Range(0, 1000)]
        public float health;
        public float heal, maxHealth;
        public int speed, strength, killCount;
        

        private float _moveH, _moveV;
        #endregion
        #region Start
        void Start()
        {
            _playerRigidbody = GetComponent<Rigidbody2D>();

            //set stats
            speed = 150;
            health = 100;
            strength = 30;
            heal = 2;
            maxHealth = 1000;
        }
        #endregion
        #region Update
        void Update()
        {
            if (health <= 0) //if no health
            {
                Destroy(gameObject);
            }

            if (health < maxHealth) //if hurt
            {
                health += heal * Time.deltaTime; //heal
            }
        }
        #endregion
        #region FixedUpdate
        private void FixedUpdate()
        {
            _moveH = Input.GetAxis("Horizontal");
            _moveV = Input.GetAxis("Vertical");

            Vector2 moveDirection = new Vector2(_moveH, _moveV);

            _playerRigidbody.AddForce(moveDirection * speed);
        }
        #endregion
    }
}