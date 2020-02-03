using UnityEngine;

namespace Asteroids.Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] GameObject destroyParticlePrefab = null;
        public float speedMin = 0.5f;
        public float speedMax = 4f;
        private float speed;
        private Vector2 direction = new Vector2();
        private Rigidbody2D rb;
        private float inactiveObstaclesXLimit = 29;
        public enum Types { Triangle, Square };
        public Types type;
        int obstacleIndex;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void Create(int index)
        {
            obstacleIndex = index;
        }

        public void Spawn(Collider2D areaCollider)
        {
            gameObject.SetActive(true);
            RandomizeMovement();
        }

        public void Destroy()
        {

        }

        public void RandomizeMovement()
        {
            speed = Random.Range(speedMin, speedMax);
            direction.x = Random.Range(-1f, 1f);
            direction.y = Mathf.Sqrt(1 - Mathf.Pow(direction.x, 2));
            if (Random.Range(0, 2) == 0)
            {
                direction.y *= -1;
            }
            rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
            Debug.Log(rb.velocity);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                gameObject.SetActive(false);
                Instantiate(destroyParticlePrefab, transform.position, Quaternion.identity);
                FindObjectOfType<SpawningObstacles>().DestroyedObstacleWithindex(obstacleIndex);
            }
        }
    }
}
