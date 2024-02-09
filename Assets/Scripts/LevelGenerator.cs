using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [field: SerializeField]
    public GameObject RoadPrefab {get; private set;}

    [field: SerializeField]
    public GameObject[] ObstaclePrefabs {get; private set;}

    [field: SerializeField]
    public GameObject Player {get; private set;}
    
    private GameObject[] roads;
    private List<GameObject>[] objects;
    private readonly int _countRoads = 3;
    private readonly float _roadLength = 200f;

    void Start()
    {
        roads = new GameObject[_countRoads];
        objects = new List<GameObject>[_countRoads];
        for (int i=0;i<_countRoads;i++)
        {
            GameObject road = Instantiate(RoadPrefab);
            road.transform.position = new Vector3(0, 0, _roadLength*i);
            roads[i] = road;
            objects[i] = new List<GameObject>();
            AddObstacles(objects[i], _roadLength*i);
        }
    }

    void Update()
    {
        for (int i=0;i<roads.Length;i++)
        {
            if (roads[i].transform.position.z + _roadLength < Player.transform.position.z)
            {
                roads[i].transform.Translate(new Vector3(0, 0, _roadLength*roads.Length), Space.World);
                AddObstacles(objects[i], roads[i].transform.position.z);
            }
        }
    }

    void AddObstacles(List<GameObject> objectList, float roadStart)
    {
        for (int i=0;i<objectList.Count;i++)
        {
            Destroy(objectList[i]);
        }
        objectList.Clear();
        int count = UnityEngine.Random.Range(5, 25);
        for (int i=0;i<count;i++)
        {
            GameObject newObject = Instantiate(ObstaclePrefabs[UnityEngine.Random.Range(0, ObstaclePrefabs.Length)]);
            objectList.Append(newObject);
            newObject.transform.position = new Vector3(UnityEngine.Random.Range(-8, 8), 0, roadStart + UnityEngine.Random.Range(0, _roadLength));
        }
    }
}
