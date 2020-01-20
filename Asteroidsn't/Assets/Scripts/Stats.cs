using UnityEngine;
using UnityEngine.UI;

namespace Asteroids
{
    public class Stats : MonoBehaviour
    {
        [SerializeField] Text pointsText = null;
        [SerializeField] Text livesLeftText = null;
        [SerializeField] float shotsDelay = 0.2f;

        int points = 0;
        int livesLeft = 3;

        public float ShotsDelay { get => shotsDelay; }

        public void AddPoint()
        {
            points++;
            pointsText.text = "Score : " + points;
        }

        public void IncreaseAttackSpeed(float multiplier)
        {
            shotsDelay *= multiplier;
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
            Destroy(gameObject);
            FindObjectOfType<Game>().GameOver();
        }
    }
}
