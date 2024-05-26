using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHit : MonoBehaviour
{
    ItemMove itemMove;
    GameObject expolsionEffect;
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
    AudioSource breakSound;
    [SerializeField]
    GameObject[] randomEquipment;
    [SerializeField]
    float[] dropRate;

    private void Start()
    {
        if(gameObject.tag == "Bomb"|| gameObject.tag == "Wood")
        {
            explosionEffect = gameObject.transform.GetChild(2).gameObject;
            itemMove = gameObject.GetComponent<ItemMove>();
        }
        RedShip = GameObject.FindGameObjectWithTag("Player");
        BlueShip = GameObject.FindGameObjectWithTag("Player2");
        RedEnergyManager = RedShip.GetComponent<RedShipEnergyManager>();
        BlueEnergyManager = BlueShip.GetComponent<BlueShipEnergyManager>();
        gameManager = GameObject.Find("GameManager");
        shipHp = gameManager.GetComponent<GameManager>();
        currentHp = itemHp;
            breakSound = gameObject.GetComponent<AudioSource>();

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
                    if(gameObject.tag == "Bomb" || gameObject.tag == "Wood")
                    {
                        itemMove.enabled = false;
                        explosionEffect.SetActive(true);
                    }
                    if(gameObject.tag == "Wood")
                    {
                        GameObject itemToSpawn = GetRandomGameObject();
                        if(itemToSpawn!=null)
                        Instantiate(itemToSpawn, transform.position, Quaternion.identity);
                    }
                    if (breakSound != null)
                    {
                        breakSound.Play();
                        Collider hitbox = gameObject.GetComponent<Collider>();
                        GameObject child = gameObject.transform.GetChild(0).gameObject;
                        GameObject child1 = gameObject.transform.GetChild(1).gameObject;
                        child.SetActive(false);
                        child1.SetActive(false);
                        hitbox.enabled = false;
                    }
                    Destroy(gameObject, 0.6f);
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
                    if (gameObject.tag == "Bomb")
                    {
                        itemMove.enabled = false;
                        explosionEffect.SetActive(true);
                    }
                    if (haveEnergy == true)
                    {
                        BlueEnergyManager.GetEnergy(other.gameObject);
                    }
                    if (breakSound != null)
                    {
                        breakSound.Play();
                        Collider hitbox = gameObject.GetComponent<Collider>();
                        GameObject child = gameObject.transform.GetChild(0).gameObject;
                        GameObject child1 = gameObject.transform.GetChild(1).gameObject;
                        child.SetActive(false);
                        child1.SetActive(false);
                        hitbox.enabled = false;
                    }
                    Destroy(gameObject, 0.6f);
                    if (haveEnergy == true)
                    {
                        BlueEnergyManager.GetEnergy(other.gameObject);
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
    GameObject GetRandomGameObject()
    {
        int totalWeight = 100;

        int randomNumber = Random.Range(1, totalWeight);
        Debug.Log(randomNumber);
        for (int i = 0; i < dropRate.Length; i++)
        {
            if (randomNumber < dropRate[i])
            {
                return randomEquipment[i];
            }
        }

        return null; // Should never happen
    }
    private void OnCollisionEnter(Collision other)
    {
        if (destroyByShip == true)
        {
            if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player2")
            {
                if (gameObject.tag == "Bomb")
                {
                    itemMove.enabled = false;
                    explosionEffect.SetActive(true);
                }
                shipHp.GetHit(other.gameObject, damage);
                if (breakSound != null)
                {
                    breakSound.Play();
                    Collider hitbox = gameObject.GetComponent<Collider>();
                    GameObject child = gameObject.transform.GetChild(0).gameObject;
                    GameObject child1 = gameObject.transform.GetChild(1).gameObject;
                    child.SetActive(false);
                    child1.SetActive(false);
                    hitbox.enabled = false;
                }
                Destroy(gameObject, 0.6f);
            }
        }
    }
    void UpdateUI()
    {
        hpImage.fillAmount = currentHp / itemHp;
    }
}
