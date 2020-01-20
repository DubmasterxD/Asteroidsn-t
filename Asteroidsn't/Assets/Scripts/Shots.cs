using UnityEngine;

namespace Asteroids
{
    public class Shots : MonoBehaviour
    {
        [SerializeField] float speed = 2;
        [SerializeField] float lifetime = 3;

        Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            rb.velocity = new Vector2(Mathf.Cos(Mathf.Deg2Rad * transform.localEulerAngles.z) * speed, Mathf.Sin(Mathf.Deg2Rad * transform.localEulerAngles.z) * speed);
            Destroy(gameObject, lifetime);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            FindObjectOfType<Stats>().AddPoint();
            Destroy(gameObject);
        }
    }
}
