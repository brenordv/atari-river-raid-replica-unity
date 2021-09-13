using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.Scripts
{
    public class Player : BaseShip
    {
        public float horizontalSpeed = 3f;
        public float verticalSpeed = .7f;
        public float horizontalLimit = 2.8f;

        private Rigidbody2D _rigidbody2D;

        public delegate void PlayerHandler();

        public event PlayerHandler OnFuelCollect;
        
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            MovePlayer();
            EnsurePlayerWithinScene();
            
            if (!Input.GetButtonDown("Fire1")) return;
            Fire();
        }

        private void EnsurePlayerWithinScene()
        {
            var position = transform.position;
            if (position.x > horizontalLimit)
            {
                transform.position = new Vector3(horizontalLimit, position.y, 0);
            }
            else if (position.x < -horizontalLimit)
            {
                transform.position = new Vector3(-horizontalLimit, position.y, 0);
            }
        }

        private void MovePlayer()
        {
            var horizontalVelocity = Input.GetAxis("Horizontal") * horizontalSpeed;
            _rigidbody2D.velocity = new Vector2(horizontalVelocity, verticalSpeed);
        }

        protected override void ProcessOnTriggerEnter2D(Collider2D other)
        {
            base.ProcessOnTriggerEnter2D(other);
            if (!other.CompareTag("Fuel")) return;
            OnFuelCollect?.Invoke();
            Destroy(other.gameObject);
        }
    }
}
