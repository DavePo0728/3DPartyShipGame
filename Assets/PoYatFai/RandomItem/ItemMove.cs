using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    public float flowSpeed;
    float lifeTime = 15.0f;
    float timer;
    [SerializeField]
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(this.gameObject.layer, 8,true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        transform.Translate(Vector3.forward * flowSpeed*Time.deltaTime);
        if(timer >= lifeTime)
        {
            Destroy(this.gameObject);
        }
    }
}
