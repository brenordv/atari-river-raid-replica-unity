using System.Collections;
using UnityEngine;

namespace Project.Scripts
{
    public class Enemy : BaseShip
    {
        [Range(0, 1)] public float steeringChance = .3f;
        public float maxShootingInterval = 6f;

        private void OnEnable()
        {
            StartCoroutine(ShootingRoutine());
            Move();
        }

        private void Move()
        {
            var x = 0f;
            if (steeringChance > 0.0f && Random.Range(0, 1) <= steeringChance)
            {
                x = Random.Range(-1f, 1.1f);
            }

            GetComponent<Rigidbody2D>().velocity = new Vector2(x, speed);
        }

        private IEnumerator ShootingRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(0f, maxShootingInterval));
                Fire();
            }
        }
    }
}