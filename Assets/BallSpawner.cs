using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sacristan.Utils;

public class BallSpawner : Singleton<BallSpawner>
{
    [SerializeField]
    private GameObject ballPrefab;

    public void SpawnBall()
    {
        GameObject spawnedBall = Instantiate(ballPrefab, transform.position, Quaternion.identity) as GameObject;
    }
}
