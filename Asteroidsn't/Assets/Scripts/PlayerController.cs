using UnityEngine;

namespace Asteroids
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] Transform gun = null;
        [SerializeField] GameObject shotPrefab = null;
        [SerializeField] float turnSpeed = 4f;
        [SerializeField] float maxSpeed = 0.3f;
        [SerializeField] float gainSpeed = 0.02f;

        Vector2 velocity = new Vector2(0, 0);
        public Vector2 Velocity { get => velocity; }

        float timeSinceLastShot = 0;
        float currSpeed = 0;

        Animator anim;
        Game game;
        Stats stats;

        void Start()
        {
            anim = GetComponent<Animator>();
            stats = GetComponent<Stats>();
            game = FindObjectOfType<Game>();
        }

        void Update()
        {
            if (!game.isGameOver)
            {
                Move();
                TbhIdkWhatItDOes();
                Shoot();
            }
            else
            {
                UpdateAnimation(0);
                currSpeed = 0;
            }
        }

        private void TbhIdkWhatItDOes()
        {
            velocity.x -= Mathf.Cos(Mathf.Deg2Rad * gameObject.transform.localEulerAngles.z) * currSpeed * Time.deltaTime;
            velocity.y -= Mathf.Sin(Mathf.Deg2Rad * gameObject.transform.localEulerAngles.z) * currSpeed * Time.deltaTime;
            if (Mathf.Pow(velocity[0], 2) + Mathf.Pow(velocity[1], 2) > maxSpeed)
            {
                velocity.x += Mathf.Cos(Mathf.Deg2Rad * gameObject.transform.localEulerAngles.z) * currSpeed * Time.deltaTime;
                velocity.y += Mathf.Sin(Mathf.Deg2Rad * gameObject.transform.localEulerAngles.z) * currSpeed * Time.deltaTime;
            }
            velocity.x -= velocity[0] / 6 * 5 * Time.deltaTime;
            velocity.y -= velocity[1] / 6 * 5 * Time.deltaTime;
        }

        private void Move()
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Accelerate();
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Turn(turnSpeed);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Turn(-turnSpeed);
            }
            DecelerateOverTime();
            UpdateAnimation(currSpeed / maxSpeed);
        }

        private void UpdateAnimation(float time)
        {
            anim.Play("PlayerMovement", 0, time);
        }

        private void Shoot()
        {
            timeSinceLastShot += Time.deltaTime;
            if (Input.GetKey(KeyCode.Space))
            {
                if (timeSinceLastShot >= stats.ShotsDelay)
                {
                    Instantiate(shotPrefab, gun.position, transform.rotation);
                }
            }
        }

        private void DecelerateOverTime()
        {
            currSpeed -= gainSpeed / 3;
            if (currSpeed < 0)
            {
                currSpeed = 0;
            }
        }

        private void Accelerate()
        {
            currSpeed += gainSpeed;
            if (currSpeed > maxSpeed)
            {
                currSpeed = maxSpeed;
            }
        }

        private void Turn(float turnSpeed)
        {
            transform.RotateAround(Vector3.zero, Vector3.forward, turnSpeed);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            FindObjectOfType<Stats>().LooseLife();
        }
    }
}
