using UnityEngine;

namespace Asteroids
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

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            RandomizeMovement();
        }

        public void RandomizeMovement()
        {
            speed = Random.Range(speedMin, speedMax);
            direction.x = Random.Range(-1f, 1f);
            direction.y = Mathf.Sqrt(1 - Mathf.Pow(direction.x, 2));
            if (Random.Range(0, 2) == 0)
            {
                direction[1] *= -1;
            }
            rb.velocity = new Vector2(direction[0] * speed, direction[1] * speed);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                gameObject.SetActive(false);
                Instantiate(destroyParticlePrefab, transform.position, Quaternion.identity);
                if (!FindObjectOfType<SpawningObstacles>().ContainsObstacle(gameObject))
                {
                    Vector2 tmp = Vector2.negativeInfinity;
                    if (type == Types.Square)
                    {
                        tmp = transform.position;
                    }
                    transform.SetPositionAndRotation(new Vector3(inactiveObstaclesXLimit + 5, transform.position.y, 0), transform.rotation);
                    if (tmp[0] != float.NegativeInfinity)
                    {
                        FindObjectOfType<SpawningObstacles>().MakeTriangles(tmp);
                    }
                    FindObjectOfType<SpawningObstacles>().AddToList(gameObject);
                }
            }
        }
    }
}
