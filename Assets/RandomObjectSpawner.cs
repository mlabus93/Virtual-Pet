using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomObjectSpawner : MonoBehaviour {

    // spawns random objects into the game to help or hurt player
    // populated through editor
    public List<GameObject> _objects;
    public List<Transform> _spawnPoints;
    public float spawnTime = 3f;

    void Start()
    {
        InvokeRepeating("SpawnObject", 5f, spawnTime);
    }

    int Checkval(int val)
    {
        if (val < 0)
            return 0;
        else
        {
            return val;
        }
    }
    
    void SpawnObject()
    {
        Debug.Log("THE COUNT IS: " + _objects.Count);
        int objIndex = Checkval(Random.Range(0, _objects.Count - 1));
        int transIndex = Checkval(Random.Range(0, _spawnPoints.Count - 1));
        Instantiate(_objects[objIndex], _spawnPoints[transIndex].position, _spawnPoints[transIndex].rotation);
    }
}
