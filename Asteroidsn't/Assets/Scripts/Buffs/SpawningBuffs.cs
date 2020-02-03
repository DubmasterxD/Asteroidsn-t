using Asteroids.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Buffs
{
    public class SpawningBuffs : MonoBehaviour
    {
        [SerializeField] List<Buff> buffsPrefabs;
        [SerializeField] Collider2D spawnArea;
        [SerializeField] float spawnInterval = 60;
        [SerializeField] float maxStayTime = 5;

        float timeSinceLastSpawned;
        List<Buff> buffs;

        GameManager game;

        private void Awake()
        {
            game = FindObjectOfType<GameManager>();
        }

        void Start()
        {
            CreateBuffs();
        }

        private void CreateBuffs()
        {
            buffs = new List<Buff>();
            foreach (Buff buff in buffsPrefabs)
            {
                Buff newBuff = Instantiate(buff);
                newBuff.Set(maxStayTime);
                buffs.Add(newBuff);
            }
        }

        void Update()
        {
            timeSinceLastSpawned += Time.deltaTime;
            if (!game.isGameOver && timeSinceLastSpawned >= spawnInterval)
            {
                SpawnRandomBuff();
                timeSinceLastSpawned = 0;
            }
        }

        private void SpawnRandomBuff()
        {
            Buff buff = buffs[Random.Range(0, buffs.Count)];
            buff.Activate(spawnArea);
        }
    }
}
