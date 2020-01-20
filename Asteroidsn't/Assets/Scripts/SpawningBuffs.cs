using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class SpawningBuffs : MonoBehaviour
    {
        [SerializeField] List<Buff> buffsPrefabs;
        [SerializeField] Vector3 minResp = new Vector3(-5, -5, 0);
        [SerializeField] Vector3 maxResp = new Vector3(5, 5, 0);
        [SerializeField] Vector3 waitPlace = new Vector3(0, 30, 0);
        [SerializeField] float spawnTime = 60;
        [SerializeField] float maxStayTime = 5;

        float timeSinceLastSpawned;
        float currentStayTime;
        List<Buff> buffs;
        Buff currentBuff;

        Game game;

        private void Awake()
        {
            game = FindObjectOfType<Game>();
        }

        void Start()
        {
            buffs = new List<Buff>();
            foreach(Buff buff in buffsPrefabs)
            {
                buffs.Add(Instantiate(buff));
                buff.Deactivate();
            }
        }
        
        void Update()
        {
            timeSinceLastSpawned += Time.deltaTime;
            currentStayTime += Time.deltaTime;
            if (!game.isGameOver && timeSinceLastSpawned >= spawnTime)
            {
                SpawnRandomBuff();
            }
            if (!game.isGameOver && currentStayTime >= maxStayTime)
            {
                DeactivateCurrentBuff();
            }
        }

        private void SpawnRandomBuff()
        {
            timeSinceLastSpawned = 0;
            currentBuff = buffs[Random.Range(0, 3)];
            currentBuff.Activate(Random.Range(minResp[0], maxResp[0]), Random.Range(minResp[1], maxResp[1]));
            currentStayTime = 0;
        }

        private void DeactivateCurrentBuff()
        {
            currentBuff.gameObject.SetActive(false);
        }
    }
}
