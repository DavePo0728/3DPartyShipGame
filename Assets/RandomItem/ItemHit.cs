using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHit : MonoBehaviour
{
    [SerializeField]
    bool canbeDestroy;
    [SerializeField]
    bool destroyByShip;
    [SerializeField]
    float itemHp;
    [SerializeField]
    float damage;
    GameObject gameManager;
    GameManager shipHp;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
        shipHp = gameManager.GetComponent<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (canbeDestroy == true)
        {
            //Debug.Log(other.tag);
            if (other.tag == "Bullet" || other.tag == "Bullet1")
            {
                Destroy(this.gameObject);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (destroyByShip == true&&this.gameObject.tag!="Equipment")
        {
            //Debug.Log(other.tag);
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
            {
                shipHp.GetHit(collision.gameObject,damage);
                Destroy(this.gameObject);
            }
        }else if(destroyByShip == true && this.gameObject.tag == "Equipment")
        {
            Destroy(this.gameObject);
        }
    }
}
