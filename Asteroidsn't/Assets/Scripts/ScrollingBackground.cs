using UnityEngine;

namespace Asteroids
{
    public class ScrollingBackground : MonoBehaviour
    {
        Vector2 uvOffset = Vector2.zero;

        PlayerController player;

        private void Awake()
        {
            player = FindObjectOfType<PlayerController>();   
        }

        void Update()
        {
            MoveBackgroundTexture();
        }

        private void MoveBackgroundTexture()
        {
            uvOffset -= player.Velocity * Time.deltaTime;
            GetComponent<Renderer>().materials[0].SetTextureOffset("_MainTex", uvOffset);
        }
    }
}
