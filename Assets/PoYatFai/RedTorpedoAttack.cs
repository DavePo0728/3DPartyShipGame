using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedTorpedoAttack : MonoBehaviour
{
    [SerializeField]
    GameObject boomEffect;
    [SerializeField]
    float bulletSpeed;
    float lifeTime = 10.0f;

    void Update()
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
        Destroy(this.gameObject, lifeTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player2")
        {
            Instantiate(boomEffect, transform.position,new Quaternion(0,90,180,0));
            Destroy(gameObject);
        }
    }
}
