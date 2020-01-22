using UnityEngine;
using UnityEngine.UI;

namespace Asteroids
{
    public class Stats : MonoBehaviour
    {
        [SerializeField] Text pointsText = null;
        [SerializeField] Text livesLeftText = null;
        [SerializeField] float startingShotsDelay = 0.2f;

        int points = 0;
        int livesLeft = 3;
        public float shotsDelay { get; private set; } = 1;

        private void Start()
        {
            shotsDelay = startingShotsDelay;
        }

        public void AddPoints(int pointsToAdd)
        {
            points += pointsToAdd;
            pointsText.text = "Score : " + points;
        }

        public void IncreaseAttackSpeed(float multiplier)
        {
            shotsDelay /= multiplier;
        }

        public void AddLife()
        {
            livesLeft++;
            livesLeftText.text = "Lives : " + livesLeft;
        }

        public void LooseLife()
        {
            livesLeft--;
            livesLeftText.text = "Lives : " + livesLeft;
            if (livesLeft == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            //TODO death
            Destroy(gameObject);
            FindObjectOfType<GameManager>().GameOver();
        }
    }
}
