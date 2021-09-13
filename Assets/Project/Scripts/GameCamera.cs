using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.Scripts
{
    public class GameCamera : MonoBehaviour
    {
        public GameObject target;
        public float verticalOffset;

        private void FixedUpdate()
        {
            if (target == null) return;
            transform.position = new Vector3(
                0,
                target.transform.position.y + verticalOffset,
                transform.position.z);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Enemy")) return;
            other.gameObject.SetActive(false);
            
        }
    }
}
