using UnityEngine;

namespace Asteroids
{
    public class ParticleDestroy : MonoBehaviour
    {
        [SerializeField] float delayBeforeDestroy = .3f;

        void Start()
        {
            Destroy(gameObject, delayBeforeDestroy);
        }
    }
}
