using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class SpawningObstacles : MonoBehaviour
    {
        [SerializeField] ObstacleSpawner[] obstaclesPrefabs = null;
        [SerializeField] int spawnRateBoostInterval = 10;
        [SerializeField] float spawnRateMultiplier = 1.1f;
        [SerializeField] Collider2D spawnArea = null;

        float timer = 0;
        float currentSpawnRateMultiplier = 1; 
        List<Obstacle>[] obstacles;
        GameManager game;
        
        [System.Serializable]
        struct ObstacleSpawner
        {
            public Obstacle obstaclePrefab;
            public float startingSpawnRate;
            public float firstSpawn;
            public int obstaclesSpawnAfterDeathIndex;
            public int obstaclesSpawnAfterDeathCount;
        }

        private void Awake()
        {
            game = FindObjectOfType<GameManager>();
        }

        private void Start()
        {
            obstacles = new List<Obstacle>[obstaclesPrefabs.Length];
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (!game.isGameOver)
            {
                SpawnObstacles();
            }
        }

        private void SpawnObstacles()
        {
            for (int index = 0; index < obstaclesPrefabs.Length; index++)
            {
                if (timer >= obstaclesPrefabs[index].firstSpawn && timer % (obstaclesPrefabs[index].startingSpawnRate * currentSpawnRateMultiplier) < Time.deltaTime)
                {
                    SpawnObstacleWithIndex(index);
                }
            }
        }

        private void SpawnObstacleWithIndex(int index)
        {
            if (obstacles[index] == null)
            {
                obstacles[index] = new List<Obstacle>();
                obstacles[index].Add(InstantiateObstacleWithIndex(index));
                obstacles[index][0].gameObject.SetActive(false);
            }
            Obstacle obstacleToSpawn = null;
            if (obstacles[index][0].gameObject.activeInHierarchy)
            {
                obstacleToSpawn = InstantiateObstacleWithIndex(index);
            }
            else
            {
                obstacleToSpawn = obstacles[index][0];
                obstacles[index].RemoveAt(0);
            }
            obstacles[index].Add(obstacleToSpawn);
            obstacleToSpawn.Spawn(spawnArea);
        }

        private Obstacle InstantiateObstacleWithIndex(int index)
        {
            Obstacle instantiatedObstacle = Instantiate(obstaclesPrefabs[index].obstaclePrefab, new Vector3(3, 3, 0), new Quaternion(0, 0, 0, 1), transform);
            instantiatedObstacle.Create(index);
            return instantiatedObstacle;
        }

        public void DestroyedObstacleWithindex(int index)
        {
            for(int i=0; i < obstaclesPrefabs[index].obstaclesSpawnAfterDeathCount; i++)
            {
                SpawnObstacleWithIndex(obstaclesPrefabs[index].obstaclesSpawnAfterDeathIndex);
            }
        }
    }
}