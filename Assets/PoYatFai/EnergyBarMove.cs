using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnergyBarMove : MonoBehaviour
{
   // LineRenderer debugLine;
    Vector3 startPoint;
    Vector3 endPoint;
    Vector3 bezierControlPoint;
    [SerializeField]
    float curveHeight;
    [SerializeField]
    float radius;
    [SerializeField]
    int resolution;
    Vector3[] path;
    Collider barCollider;
    [SerializeField]
    ItemMove itemMove;
    // Start is called before the first frame update
    void Start()
    {
       // debugLine = gameObject.GetComponent<LineRenderer>();
        SetPosition();
        GetPath();
        barCollider = gameObject.GetComponent<Collider>();
        //debugLine.positionCount = path.Length;
      //  debugLine.SetPositions(path);

        this.gameObject.transform.DOPath(path, 0.8f).SetEase(Ease.Linear);
        Invoke("EnableCollider", 1.0f);
        itemMove = GetComponent<ItemMove>();
    }
    void EnableCollider()
    {
        barCollider.enabled = true;
        itemMove.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {

    }
    void SetPosition()
    {
        startPoint = transform.position;
        endPoint = GetLandingPos();
        bezierControlPoint = (startPoint + endPoint) * 0.5f + (Vector3.up * curveHeight);
    }
    Vector3 GetLandingPos()
    {
            Vector3 randomPosition = Random.insideUnitSphere * radius + transform.position;
        randomPosition.y = transform.position.y;

        return randomPosition;
    }
    void GetPath()
    {
        path = new Vector3[resolution];
        for (int i = 0; i < resolution; i++)
        {
            var t = (i + 1) / (float)resolution;
            path[i] = GetBezierPoint(t, startPoint, bezierControlPoint, endPoint);
        }
    }
    static Vector3 GetBezierPoint(float t, Vector3 start, Vector3 center, Vector3 end)
    {
        return (1 - t) * (1 - t) * start + 2 * t * (1 - t) * center + t * t * end;
    }
}
