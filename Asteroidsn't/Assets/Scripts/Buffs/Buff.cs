using Asteroids.Player;
using UnityEngine;

namespace Asteroids.Buffs
{
    public class Buff : MonoBehaviour
    {
        enum Bonuses { Plus, ASBoost, Destroy };
        [SerializeField] Bonuses bonus = default;

        float maxStayTime = 1;
        float currentStayTime = 0;

        Stats stats;

        private void Awake()
        {
            stats = FindObjectOfType<Stats>();
        }

        private void Update()
        {
            currentStayTime += Time.deltaTime;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                GiveBonus();
                Deactivate();
            }
        }

        private void GiveBonus()
        {
            switch (bonus)
            {
                case Bonuses.Plus:
                    stats.AddLife();
                    break;
                case Bonuses.ASBoost:
                    stats.IncreaseAttackSpeed(7 / 8f);
                    break;
                case Bonuses.Destroy:
                    Bonus3Action();
                    break;
                default:
                    break;
            }
        }

        private void Bonus3Action() //TODO
        {
            stats.AddPoints(100);
        }

        public void Activate(Collider2D spawnArea)
        {
            gameObject.SetActive(true);
            transform.position = spawnArea.bounds.size;
        }

        public void Activate(float positionX, float positionY)
        {
            gameObject.SetActive(true);
            transform.SetPositionAndRotation(new Vector3(positionX, positionY, 0), Quaternion.identity);
        }

        public void Set(float newMaxStayTime)
        {
            maxStayTime = newMaxStayTime;
            Deactivate();
        }

        private void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
