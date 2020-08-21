using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AI
{
    [AddComponentMenu("AI/PlayerControl")]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerControl : MonoBehaviour
    {
        #region Variables
        [Header("Reference Variables")]
        public float speed = 1500;
        public Rigidbody2D _playerRigidbody;

        private float _moveH, _moveV;
        #endregion
        void Start()
        {
            _playerRigidbody = GetComponent<Rigidbody2D>();
        }

        void Update()
        {

        }

        private void FixedUpdate()
        {
            _moveH = Input.GetAxis("Horizontal");
            _moveV = Input.GetAxis("Vertical");

            Vector2 moveDirection = new Vector2(_moveH, _moveV);

            _playerRigidbody.AddForce(moveDirection * speed);
        }
    }
}