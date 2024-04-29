using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoAttack : MonoBehaviour
{
    [SerializeField]
    float bulletSpeed;
    public float damage;
    float lifeTime = 3.0f;

    void Update()
    {
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
        Destroy(this.gameObject, lifeTime);
    }
}
