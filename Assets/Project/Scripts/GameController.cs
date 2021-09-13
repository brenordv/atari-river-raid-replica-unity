using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Project.Scripts
{
    public class GameController : MonoBehaviour
    {
        public Player player;
        public ObjectPool enemyPool;
        public GameObject fuelPrefab;
        public Text scoreText;
        public Text fuelText;
        public Text gameOverTitle;
        public Text gameOverSubTitle;
        public SoundPlayer soundPlayer;
        public AudioClip refuelSound;
        public float enemySpawnInterval = 1f;
        public float fuelSpawnInterval = 9f;
        public float horizontalLimit = 2.8f;
        public float speedDecreaseSpeed = 3f;
        public int pointsPerKill = 10;
        public float gameOverTimer = 3f;
        
        private int _score;
        private float _fuel = 100f;
        private bool _gameOver;
        private void Start()
        {
            StartCoroutine(SpawnEnemyRoutine());
            StartCoroutine(SpawnFuelRoutine());
            StartCoroutine(SpendFuelRoutine());
            player.OnFuelCollect += Refuel;
        }

        private void Update()
        {
            if (player != null) return;
            if (!_gameOver)
                StartCoroutine(GameOverAfterSeconds());
        }

        private IEnumerator GameOverAfterSeconds()
        {
            _gameOver = true;
            gameOverTitle.gameObject.SetActive(true);
            UpdateGameOverSubTitleText(gameOverTimer);
            
            gameOverSubTitle.gameObject.SetActive(true);
            var timeLeft = gameOverTimer;
            while (timeLeft > 0f)
            {
                UpdateGameOverSubTitleText(timeLeft);
                Debug.Log($"GO: {timeLeft}");
                yield return new WaitForSeconds(1);
                timeLeft -= 1f;
            }

            SceneManager.LoadScene("Game");
        }

        private IEnumerator SpendFuelRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                if (player == null)
                    break;
                _fuel -= speedDecreaseSpeed;

                if (_fuel <= 0)
                {
                    _fuel = 0;
                    Destroy(player.gameObject);
                }
                
                UpdateFuelText(_fuel);
            }
        }

        private IEnumerator SpawnFuelRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(1f, fuelSpawnInterval));
                if (player == null)
                    break;

                var fuel = Instantiate(fuelPrefab, transform);
                fuel.transform.position = GetRandomSpawnPoint();
                
            }
        }
        
        private IEnumerator SpawnEnemyRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(enemySpawnInterval);
                if (player == null)
                    break;
                var enemy = enemyPool.GetObj();
                enemy.transform.position = GetRandomSpawnPoint();
                enemy.GetComponent<Enemy>().OnDeath += AddPoints;
            }
        }

        private Vector3 GetRandomSpawnPoint()
        {
            return new Vector3(
                Random.Range(-horizontalLimit, horizontalLimit),
                player.transform.position.y + Screen.height / 100f,
                0);
        }

        private void AddPoints()
        {
            _score += pointsPerKill;
            scoreText.text = $"Score: {_score}";
        }

        private void Refuel()
        {
            _fuel = 100f;
            UpdateFuelText(_fuel);
            soundPlayer.Play(refuelSound);
        }

        private void UpdateFuelText(float fuel)
        {
            fuelText.text = $"Fuel: {fuel}";
        }

        private void UpdateGameOverSubTitleText(float seconds)
        {
            gameOverSubTitle.text = $"Better luck next time! A new game will start in {seconds} seconds...";
        }
    }
}