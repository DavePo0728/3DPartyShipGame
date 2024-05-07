using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomIn : MonoBehaviour
{
    [SerializeField]
    Transform[] players;
    Camera cam;
    [SerializeField]
    float offest;
    [SerializeField]
    float xMinDistance, xMaxDistance, zMinDistance, zMaxDistance;

    float xMin, xMax, zMin, zMax;


    void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        xMin = xMax = players[0].transform.position.x;
        zMin = zMax = players[0].transform.position.z;
        for (int i = 1; i < players.Length; i++)
        {
            if (players[i].position.x < xMin)
                xMin = players[i].position.x;
            if (players[i].position.x > xMax)
                xMax = players[i].position.x;
            if (players[i].position.z < zMin)
                zMin = players[i].position.z;
            if (players[i].position.z > zMax)
                zMax = players[i].position.z;
        }
        float xMid = (xMin + xMax) / 2;
        float zMid = (zMin + zMax) / 2;
        float xDistance = xMax - xMin;
        float zDistance = zMax - zMin;

        if (xDistance < xMinDistance)
            xDistance = xMinDistance;
        if (xDistance > xMaxDistance)
            xDistance = xMaxDistance;
        if (zDistance < zMinDistance)
            zDistance = zMinDistance;
        if (zDistance > zMaxDistance)
            zDistance = zMaxDistance;
        if (xDistance > zDistance)
            transform.position = new Vector3(xMid, xDistance, zMid + offest);
        if (xDistance < zDistance)
            transform.position = new Vector3(xMid, zDistance, zMid + offest);
    }

    
}
