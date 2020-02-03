using Asteroids.Player;
using UnityEngine;

namespace Asteroids.Moving
{
    public class ScrollingBackground : MonoBehaviour
    {
        Vector2 uvOffset = new Vector2(0, 0);

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
