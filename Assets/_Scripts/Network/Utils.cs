using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static int num_player;
    public static Transform[] listSpawnPoint;
    public static Vector3 GetSpawnPoint()
    {
        listSpawnPoint = FindSpawnPoint();

        return new Vector3 (0, 0, 0);
    }

    public static Transform[] FindSpawnPoint()
    {
        Transform[] t = new Transform[4];
        int i = 0;
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("SpawnPoint"))
        {
            t[i] = go.transform;
        }

        return t;
        
    }


    public static Vector3 GetRandomSpawnPoint()
    {
        return new Vector3(Random.Range(-10,10),1.1f,Random.Range(-10,10));
    }

}
