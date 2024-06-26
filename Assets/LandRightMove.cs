using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandRightMove : MonoBehaviour
{
    [SerializeField]
    GameObject startpoint;
    [SerializeField]
    float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Finish")
        gameObject.transform.position = startpoint.transform.position;
    }
}
