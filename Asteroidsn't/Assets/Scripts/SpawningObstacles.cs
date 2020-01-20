using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class SpawningObstacles : MonoBehaviour
    {
        public int squaresAmount = 30;
        public int trianglesAmount = 80;
        public GameObject squaresPrefab;
        public GameObject trianglesPrefab;
        public float activeRate = 3f;
        public Vector2 activeMin;
        public Vector2 activeMax;
        private List<GameObject> inactiveSquares;
        private List<GameObject> inactiveTriangles;
        private Vector2 inactiveObstaclesSpawnLocation = new Vector2(40, 0);
        private float timeSinceLastActivated;
        
        void Start()
        {
            inactiveSquares = new List<GameObject>();
            inactiveTriangles = new List<GameObject>();
            for (int i = 0; i < squaresAmount; i++)
            {
                GameObject obstacle = Instantiate(squaresPrefab, inactiveObstaclesSpawnLocation, Quaternion.identity);
                obstacle.SetActive(false);
                inactiveSquares.Add(obstacle);
            }
            for (int i = 0; i < trianglesAmount; i++)
            {
                GameObject obstacle = Instantiate(squaresPrefab, inactiveObstaclesSpawnLocation, Quaternion.identity);
                obstacle.SetActive(false);
                inactiveTriangles.Add(obstacle);
            }
        }
        
        void Update()
        {
            timeSinceLastActivated += Time.deltaTime;
            if (!FindObjectOfType<Game>().isGameOver && timeSinceLastActivated >= activeRate)
            {
                timeSinceLastActivated = 0;
                if (Random.Range(0, 2) == 1)
                {
                    if (inactiveSquares.Count > 0)
                    {
                        inactiveSquares[0].transform.SetPositionAndRotation(new Vector3(Random.Range(activeMin[0], activeMax[0]), Random.Range(activeMin[1], activeMax[1]), 0), transform.rotation);
                        inactiveSquares[0].GetComponent<Obstacle>().RandomizeMovement();
                        inactiveSquares[0].SetActive(true);
                        inactiveSquares.RemoveAt(0);
                    }
                }
                else
                {
                    if (inactiveTriangles.Count > 0)
                    {
                        inactiveTriangles[0].transform.SetPositionAndRotation(new Vector3(Random.Range(activeMin[0], activeMax[0]), Random.Range(activeMin[1], activeMax[1]), 0), transform.rotation);
                        inactiveTriangles[0].GetComponent<Obstacle>().RandomizeMovement();
                        inactiveTriangles[0].SetActive(true);
                        inactiveTriangles.RemoveAt(0);
                    }
                }
            }
            if (activeRate > 0.01)
            {
                activeRate -= Time.deltaTime / 200;
            }
        }

        public void AddToList(GameObject obstacle)
        {
            if (obstacle.GetComponent<Obstacle>().type == Obstacle.Types.Triangle)
            {
                inactiveTriangles.Add(obstacle);
            }
            else
            {
                inactiveSquares.Add(obstacle);
            }
        }

        public void MakeTriangles(Vector2 position)
        {
            if (!FindObjectOfType<Game>().isGameOver && inactiveTriangles.Count > 1)
            {
                inactiveTriangles[0].transform.SetPositionAndRotation(new Vector3(position[0] + 0.1f, position[1] + 0.1f, 0), transform.rotation);
                inactiveTriangles[0].GetComponent<Obstacle>().RandomizeMovement();
                inactiveTriangles.RemoveAt(0);
                inactiveTriangles[0].transform.SetPositionAndRotation(new Vector3(position[0] - 0.1f, position[1] - 0.1f, 0), transform.rotation);
                inactiveTriangles[0].GetComponent<Obstacle>().RandomizeMovement();
                inactiveTriangles.RemoveAt(0);
            }
        }

        public bool ContainsObstacle(GameObject obstacle)
        {
            if (inactiveTriangles.Contains(obstacle) || inactiveSquares.Contains(obstacle))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}