using UnityEngine;

namespace Asteroids
{
    public class Moving : MonoBehaviour
    {
        [SerializeField] float playSpaceEdge = 5.07f;

        PlayerController player;

        private void Awake()
        {
            player = FindObjectOfType<PlayerController>();
        }

        void Update()
        {
            Move();
            StayInsidePlaySpace();
        }

        private void Move()
        {
            float deltaX = player.Velocity.x * 10 * Time.deltaTime;
            float deltaY = player.Velocity.y * 10 * Time.deltaTime;
            Vector3 newPosition = new Vector3(transform.position.x + deltaX, transform.position.y + deltaY, transform.position.z);
            transform.SetPositionAndRotation(newPosition, transform.rotation);
        }

        private void StayInsidePlaySpace()
        {
            if (transform.position.x > playSpaceEdge)
            {
                transform.SetPositionAndRotation(new Vector3(-playSpaceEdge, transform.position.y, 0), transform.rotation);
            }
            else if (transform.position.x < -playSpaceEdge)
            {
                transform.SetPositionAndRotation(new Vector3(playSpaceEdge, transform.position.y, 0), transform.rotation);
            }
            if (transform.position.y > playSpaceEdge)
            {
                transform.SetPositionAndRotation(new Vector3(transform.position.x, -playSpaceEdge, 0), transform.rotation);
            }
            else if (transform.position.y < -playSpaceEdge)
            {
                transform.SetPositionAndRotation(new Vector3(transform.position.x, playSpaceEdge, 0), transform.rotation);
            }
        }
    }
}
