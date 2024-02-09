using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Callbacks;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    [field: SerializeField]
    public GameObject roadPrefab {get; private set;}

    [field: SerializeField]
    public GameObject[] obstaclePrefabs {get; private set;}

    [field: SerializeField]
    public GameObject player {get; private set;}
    
    private GameObject[] roads;
    private List<GameObject>[] objects;
    private int countRoads = 3;
    private float roadLength = 200f;

    // Start is called before the first frame update
    void Start()
    {
        roads = new GameObject[countRoads];
        objects = new List<GameObject>[countRoads];
        for (int i=0;i<countRoads;i++)
        {
            GameObject road = Instantiate(roadPrefab);
            road.transform.position = new Vector3(0, 0, roadLength*i);
            roads[i] = road;
            objects[i] = new List<GameObject>();
            AddObstacles(objects[i], roadLength*i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i=0;i<roads.Length;i++)
        {
            if (roads[i].transform.position.z + roadLength < player.transform.position.z)
            {
                roads[i].transform.Translate(new Vector3(0, 0, roadLength*roads.Length), Space.World);
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
            GameObject newObject = Instantiate(obstaclePrefabs[UnityEngine.Random.Range(0, obstaclePrefabs.Length)]);
            objectList.Append(newObject);
            newObject.transform.position = new Vector3(UnityEngine.Random.Range(-8, 8), 0, roadStart + UnityEngine.Random.Range(0, roadLength));
        }
    }
}
