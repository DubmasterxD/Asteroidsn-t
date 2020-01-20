using UnityEngine;

namespace Asteroids
{
    public class Buff : MonoBehaviour
    {
        enum Bonuses { Plus, ASBoost, Destroy };
        [SerializeField] Bonuses bonus = default;

        Stats stats;

        private void Awake()
        {
            stats = FindObjectOfType<Stats>();
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
            for (int i = 0; i < 100 / FindObjectOfType<SpawningObstacles>().activeRate; i++)
            {
                stats.AddPoint();
            }
        }

        public void Activate(float positionX, float positionY)
        {
            gameObject.SetActive(true);
            transform.SetPositionAndRotation(new Vector3(positionX, positionY, 0), Quaternion.identity);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
