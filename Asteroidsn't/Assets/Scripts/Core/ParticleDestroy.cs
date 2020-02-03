using UnityEngine;

namespace Asteroids.Core
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
