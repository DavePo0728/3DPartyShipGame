using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField]
    float bulletSpeed;
    public float damage;
    float lifeTime=3.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-Vector3.forward*bulletSpeed*Time.deltaTime);
        Destroy(this.gameObject, lifeTime);
    }
    
}
