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
    bool haveEnergy;
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
    [SerializeField]
    GameObject explosionEffect;
    GameObject RedShip, BlueShip;
    RedShipEnergyManager RedEnergyManager;
    BlueShipEnergyManager BlueEnergyManager;

    [SerializeField]
    AudioSource BreakEffect;

    private void Start()
    {
        RedShip = GameObject.FindGameObjectWithTag("Player");
        BlueShip = GameObject.FindGameObjectWithTag("Player2");
        RedEnergyManager = RedShip.GetComponent<RedShipEnergyManager>();
        BlueEnergyManager = BlueShip.GetComponent<BlueShipEnergyManager>();
        gameManager = GameObject.Find("GameManager");
        shipHp = gameManager.GetComponent<GameManager>();
        currentHp = itemHp;
        //if (gameObject.tag== "Bomb")
        //{
            BreakEffect = gameObject.GetComponent<AudioSource>();
        //}

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
            if (other.gameObject.tag == "Bullet")
            {
                BulletMove bulletScript = other.gameObject.GetComponent<BulletMove>();
                getShoot(bulletScript.damage);
                hpUI.SetActive(true);
                UpdateUI();
                if (currentHp <= 0)
                {
                    
                    if (BreakEffect != null)
                    {
                        BreakEffect.Play();
                        Collider hitbox = gameObject.GetComponent<Collider>();
                        GameObject child = gameObject.transform.GetChild(0).gameObject;
                        GameObject child1 = gameObject.transform.GetChild(1).gameObject;
                        child.SetActive(false);
                        child1.SetActive(false);
                        hitbox.enabled = false;
                    }
                    Invoke("InvokeDestroy", 0.6f);
                    if (haveEnergy == true)
                    {
                        RedEnergyManager.GetEnergy(other.gameObject);
                        Debug.Log("getEnergy");
                    }
                   
                }
                Destroy(other.gameObject);
                
            }
            if(other.gameObject.tag == "Bullet1")
            {
                BulletMove bulletScript = other.gameObject.GetComponent<BulletMove>();
                getShoot(bulletScript.damage);
                hpUI.SetActive(true);
                UpdateUI();
                if (currentHp <= 0)
                {
                    if (haveEnergy == true)
                    {
                        BlueEnergyManager.GetEnergy(other.gameObject);
                    }
                    if (BreakEffect != null)
                    {
                        BreakEffect.Play();
                        Collider hitbox = gameObject.GetComponent<Collider>();
                        GameObject child = gameObject.transform.GetChild(0).gameObject;
                        GameObject child1 = gameObject.transform.GetChild(1).gameObject;
                        child.SetActive(false);
                        child1.SetActive(false);
                        hitbox.enabled = false;
                    }
                    Invoke("InvokeDestroy", 0.6f);
                    if (haveEnergy == true)
                    {
                        BlueEnergyManager.GetEnergy(other.gameObject);
                        Debug.Log("getEnergy");
                    }
                }
                
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
    void InvokeDestroy()
    {
        Destroy(this.gameObject);
    }
    void UpdateUI()
    {
        hpImage.fillAmount = currentHp / itemHp;
    }
}
