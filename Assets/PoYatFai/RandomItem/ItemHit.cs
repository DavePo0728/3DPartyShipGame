using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHit : MonoBehaviour
{
    [SerializeField]
    Transform cameraTransform;
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
    GameObject hpUI;
    [SerializeField]
    Image hpImage;

    private void Start()
    {
        
        gameManager = GameObject.Find("GameManager");
        shipHp = gameManager.GetComponent<GameManager>();
        currentHp = itemHp;

        if(canbeDestroy == true)
        {
            cameraTransform = Camera.main.transform;
            hpUI = gameObject.transform.GetChild(0).gameObject;
            hpImage = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponentInChildren<Image>();
            hpUI.transform.LookAt(2 * hpUI.transform.position - cameraTransform.position);
        }

    }
    private void Update()
    {
        
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
                hpUI.SetActive(true);
                UpdateUI();
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
        if (destroyByShip == true && this.gameObject.tag != "Equipment")
        {
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
    void UpdateUI()
    {
        hpImage.fillAmount = currentHp / itemHp;
    }
}
