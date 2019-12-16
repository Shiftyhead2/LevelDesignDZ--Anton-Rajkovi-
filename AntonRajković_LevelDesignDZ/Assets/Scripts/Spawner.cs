using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int Count;
    public GameObject[] Enemies;
    public Transform[] SpawnPoints;

    private void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        for(int i = 0; i < Count; i++)
        {
            int RandomEnemy = Random.Range(0, Enemies.Length);
            int RandomSpawn = Random.Range(0, SpawnPoints.Length);
            GameObject Enemy = Instantiate(Enemies[RandomEnemy], SpawnPoints[RandomSpawn].position, SpawnPoints[RandomSpawn].rotation) as GameObject;
            Enemy.name = "Zombie";
            //Debug.Log("Spawned this enemy " + Enemy.name + " at this spawnpoint " + SpawnPoints[RandomSpawn].name);
        }
    }
}
