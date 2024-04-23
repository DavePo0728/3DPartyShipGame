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
    float currentHp;
    [SerializeField]
    float damage;
    GameObject gameManager;
    GameManager shipHp;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
        shipHp = gameManager.GetComponent<GameManager>();
        currentHp = itemHp;
    }
    void getShoot(float damage)
    {
        currentHp -= damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (canbeDestroy == true)
        {
            //Debug.Log(other.tag);
            if (other.gameObject.tag == "Bullet" || other.gameObject.tag == "Bullet1")
            {
                BulletMove bulletScript = other.gameObject.GetComponent<BulletMove>();
                getShoot(bulletScript.damage);
                Destroy(other.gameObject);
            }
        }
        if (canbeDestroy == false)
        {
            if (other.gameObject.tag == "Bullet" || other.gameObject.tag == "Bullet1")
            {
                Destroy(other.gameObject);
            }
        }
        if (currentHp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (this.gameObject.name== "Wood(Clone)") {
            Debug.Log(this.gameObject.name + "hit" + other.gameObject.name);
        }
        if (destroyByShip == true && this.gameObject.tag != "Equipment")
        {
            //Debug.Log(other.tag);
            if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player2")
            {
                shipHp.GetHit(other.gameObject, damage);
                Destroy(this.gameObject);
            }
        }
        if (destroyByShip == true && this.gameObject.tag == "Equipment")
        {
            if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player2")
            {
                Destroy(this.gameObject);
            }
            if (other.gameObject.tag == "Bullet" || other.gameObject.tag == "Bullet1")
            {
                Destroy(other.gameObject);
            }
        }
    }
}
