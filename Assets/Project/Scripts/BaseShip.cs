using Project.Scripts.UI;
using UnityEngine;

namespace Project.Scripts
{
    public class BaseShip : MonoBehaviour
    {
        public float speed;
        public GameObject bulletPrefab;
        public float bulletSpeed;
        public AudioClip shootingSound;
        public AudioClip deathSound;
        public SoundPlayer soundPlayer;
        [TagSelector] public string opponentTag;
        [TagSelector] public string opponentBulletTag;

        public delegate void ShipHandler();

        public event ShipHandler OnDeath;
        
        protected void Fire()
        {
            var bullet = Instantiate(bulletPrefab, transform.parent);
            bullet.transform.position = transform.position;
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);
            soundPlayer.Play(shootingSound);
            Destroy(bullet, 3f);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            ProcessOnTriggerEnter2D(other);
        }

        protected virtual void ProcessOnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(opponentTag) && !other.CompareTag(opponentBulletTag)) return;
            
            if (CompareTag("Player"))
                Destroy(gameObject);
            else
                gameObject.SetActive(false);
            
            soundPlayer.Play(deathSound);
            Destroy(other.gameObject);
            OnDeath?.Invoke();
        }
    }
}
