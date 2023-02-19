using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGenerator: MonoBehaviour
{
    public GameObject road;
    public GameObject obstacle;
    private int maxRoadScene = 30;
    private int maxObstacleScene = 100;
    private int countRoads;
    private int countObstacles;
    private void Awake()
    {
        for (int i = 1; i <= maxRoadScene; i++)
        {
            countRoads = i;
            CreateNewRoad(countRoads);
        }

        for (int i = 1; i <= maxObstacleScene; i++)
        {
            countObstacles = i;
            CreateNewObstacle(countObstacles);
        }
    }

    private void Update()
    {
        GameObject[] roads = GameObject.FindGameObjectsWithTag("Ground");

        if (roads.Length < maxRoadScene)
        {
            countRoads++;
            CreateNewRoad(countRoads);
        }

        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        if (obstacles.Length < maxObstacleScene)
        {
            countObstacles++;
            CreateNewObstacle(countObstacles);
        }
    }

    void CreateNewRoad(int index)
    {
        GameObject roadGo = Instantiate(road, new Vector3(0f, 0f, index * 200f), Quaternion.identity);
        roadGo.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        roadGo.transform.SetParent(this.transform);
        roadGo.layer = LayerMask.NameToLayer("RoadLayer");
    }

    void CreateNewObstacle(int index)
    {
        int sort = Random.Range(0, 6);
        float posX = 0f;

        if (sort == 0 || sort == 3)
        {
            posX = -7f;
        } 
        else if (sort == 1 || sort == 4)
        {
            posX = 0f;
        } 
        else if (sort == 2 || sort == 5)
        {
            posX = 7f;
        }
         
        GameObject obstacleGo = Instantiate(obstacle, new Vector3(posX, 0f, index * 30f), Quaternion.identity);
        obstacleGo.transform.SetParent(this.transform);
    }
}
