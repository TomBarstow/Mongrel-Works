using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TestMapGeneratorCA : MonoBehaviour
{
    public GameObject[] thing;
    public int xMax;
    public int yMax;
    public int zMax;
    public int gridSpacing = 10;
    public Vector3 gridOrigin;

    //Offset for perlin noise map
    Vector3 shift = new Vector3(0, 0, 0);
    //Zoom for perlin noise map
    float zoom = 0.05f;

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        int mapAreax = xMax;
        int mapAreay = yMax;
        int mapAreaz = zMax;

        for (int x = 0; x < mapAreax; x++)
        {
                for (int z = 0; z < mapAreaz; z++)
                {
                    //Spawn position
                    Vector3 spawnPosition = new Vector3(x * gridSpacing, 0, z * gridSpacing) + gridOrigin;

                    Vector3 pos = zoom * (new Vector3(x, 0, z)) + shift;
                    float noisezx = Mathf.PerlinNoise(pos.z, pos.x);

                    GameObject thingClone = Instantiate(thing[0], spawnPosition, Quaternion.identity);
                    thingClone.transform.localScale = new Vector3(1, noisezx * 50, 1);
                    
                }
        }
    }
}