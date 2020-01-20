using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroids
{
    public class Game : MonoBehaviour
    {
        [SerializeField] GameObject gameOverUI = null;

        public bool isGameOver { get; private set; } = false;

        void Awake()
        {
            Screen.SetResolution(1280, 720, false);
        }

        void Update()
        {
            if (isGameOver && Input.GetKeyDown(KeyCode.Space))
            {
                ReloadGame();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExitGame();
            }
        }

        public void GameOver()
        {
            isGameOver = true;
            gameOverUI.SetActive(true);
        }

        private void ReloadGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void ExitGame()
        {
            Application.Quit();
        }
    }
}
