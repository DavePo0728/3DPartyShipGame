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
    [SerializeField]
    GameObject leftUp, leftDown, rightUp, rightDown;
    float xMin, xMax, zMin, zMax;
    [SerializeField]
    float rectSize;
    void Start()
    {
        cam = Camera.main;
        //up = new Rect(0, Screen.height - rectSize, Screen.width, rectSize);
        //down = new Rect(0, 0, Screen.width, rectSize);
        //left = new Rect(0, 0, rectSize, Screen.height);
        //right = new Rect(Screen.width - rectSize, 0, rectSize, Screen.height);
    }

    void LateUpdate()
    {
        printScreenLocation();
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
        //if (zDistance < zMinDistance)
           // zDistance = zMinDistance;
        if (zDistance > zMaxDistance)
            zDistance = zMaxDistance;
        if (xDistance > zDistance)
            transform.position = Vector3.Lerp(new Vector3(transform.position.x, xDistance,transform.position.z), transform.position, 0.5f);
        if (xDistance < zDistance)
            transform.position = Vector3.Lerp(new Vector3(transform.position.x, zDistance,transform.position.z), transform.position, 0.5f);
        if (transform.position.x<2&&transform.position.x>-2&&transform.position.z<7.9f&& transform.position.z > 2.5f)
        {
            if (xDistance > zDistance)
                transform.position = Vector3.Lerp(new Vector3(xMid, xDistance, zMid + offest), transform.position, 0.5f);
            if (xDistance < zDistance)
                transform.position = Vector3.Lerp(new Vector3(xMid, zDistance, zMid + offest), transform.position, 0.5f);
        }

    }
    void printScreenLocation()
    {
        //Vector3 leftBottom = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        //Vector3 leftTop = cam.ScreenToWorldPoint(new Vector3(0, Screen.height, cam.nearClipPlane));
        //Vector3 rightTop = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.nearClipPlane));
        //Vector3 rightBottom = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, cam.nearClipPlane));
        //Debug.Log("Left Bottom: " + leftBottom+ "\nLeft Top: " + leftTop+ "\nRight Top: " + rightTop+ "\nRight Bottom: " + rightBottom);
        cam = GetComponent<Camera>();
        Vector3[] frustumCorners = new Vector3[4];
        // 計算遠裁剪面的四個角落
        cam.CalculateFrustumCorners(new Rect(0, 0, 1, 1), cam.farClipPlane, Camera.MonoOrStereoscopicEye.Mono, frustumCorners);

        // 轉換為世界坐標
        Vector3 worldSpaceCorner1 = cam.transform.TransformPoint(frustumCorners[0]);
        Vector3 worldSpaceCorner2 = cam.transform.TransformPoint(frustumCorners[1]);
        Vector3 worldSpaceCorner3 = cam.transform.TransformPoint(frustumCorners[2]);
        Vector3 worldSpaceCorner4 = cam.transform.TransformPoint(frustumCorners[3]);
        worldSpaceCorner1.y = worldSpaceCorner2.y;
        worldSpaceCorner4.y = worldSpaceCorner3.y;
        leftUp.transform.position = worldSpaceCorner1;
        leftDown.transform.position = worldSpaceCorner2;
        rightUp.transform.position = worldSpaceCorner3;
        rightDown.transform.position = worldSpaceCorner4;

    }
    
}
