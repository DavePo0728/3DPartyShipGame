using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    [SerializeField]
    GameObject barrel;
    [SerializeField]
    GameObject canonBulletPrefab;
    GameObject bullet;
    [SerializeField]
    GameObject turretBase;
    [SerializeField]
    GameObject canon;
    bool canonAttack =true;
    private float timeBetweenShots;
    private float timeSinceLastShot = 0.0f;
    [SerializeField]
    float rotateSpeed;
    Vector2 aimInput;
    bool rtDown = false;
    //[HideInInspector]
    public bool superAttack;
    [SerializeField]
    GameObject superBulletPrefab;

    [SerializeField]
    GameObject specialWeapon;
    [SerializeField]
    public GameObject currentSpecialWeapon;

    [SerializeField]
    GameObject TorpedoShootingPoint;
    [SerializeField]
    GameObject[] specialWeaponList;
    [SerializeField]
    GameObject[] TorpedoList;
    [SerializeField]
    int torpedoRemain=3;

    AudioSource gunShot;


    // Start is called before the first frame update
    void Start()
    {
        timeBetweenShots = 1 / (210 / 60.0f);
        turretBase = canon;
        gunShot = gameObject.GetComponent<AudioSource>();
        superAttack = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeSinceLastShot += Time.deltaTime;
        
        //Vector3 lookDirection = new Vector3(aimInput.x, 0, aimInput.y);
        //Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);

        //float step = rotateSpeed * Time.deltaTime;
        //transform.rotation = Quaternion.RotateTowards(lookRotation, transform.rotation, step);
        if (rtDown && timeSinceLastShot >= timeBetweenShots && canonAttack)
        {
            //Quaternion bulletRotation = new Quaternion(transform.rotation.x -90, transform.rotation.y, transform.rotation.z,transform.rotation.w);
            Instantiate(canonBulletPrefab, barrel.transform.position, transform.rotation);
            if (superAttack)
                Instantiate(superBulletPrefab, barrel.transform.position, transform.rotation);
            gunShot.Play();
            timeSinceLastShot = 0.0f;
        }
    }

    public void GetRotate(InputAction.CallbackContext context)
    {
        if (context.performed && turretBase == canon) {
            //if (context.ReadValue<Vector2>().x<-0.3|| context.ReadValue<Vector2>().x > 0.3 || context.ReadValue<Vector2>().y < -0.3 || context.ReadValue<Vector2>().y < -0.3)
            aimInput = context.ReadValue<Vector2>();
            Vector3 lookDirection = new Vector3(aimInput.x, 0, aimInput.y);
            Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            float step = rotateSpeed * Time.deltaTime;
            
            if(lookRotation.y<=0.9 && lookRotation.y >= -0.9)
            turretBase.transform.rotation = Quaternion.RotateTowards(lookRotation, transform.rotation, step);


        }else if(context.performed && turretBase == specialWeapon)
        {
            aimInput = context.ReadValue<Vector2>();
            Vector3 lookDirection = new Vector3(aimInput.x, 0, aimInput.y);
            Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            float step = rotateSpeed * Time.deltaTime;

            //Debug.Log(lookRotation.y);
            if (lookRotation.y >= 0.5 || lookRotation.y <= -0.5)
                turretBase.transform.rotation = Quaternion.RotateTowards(lookRotation, transform.rotation, step);
            TorpedoShootingPoint.transform.rotation = turretBase.transform.rotation;
        }
        
    }
    public void ChangeTurret(InputAction.CallbackContext context)
    {
        if (context.started) {
            if (turretBase == specialWeapon)
            {
                canonAttack = true;
                turretBase = canon;
            } else if (turretBase == canon&&currentSpecialWeapon !=null)
            {
                turretBase = specialWeapon;
                canonAttack = false;
            }
        }
    }
    public void CanonAttack(InputAction.CallbackContext context)
    {
        if (context.performed) {
            rtDown = true;
        }
        if (context.canceled)
        {
            rtDown = false;
        }
    }
    public void SpecialWeaponAttack(InputAction.CallbackContext context)
    {
        if (canonAttack==false&&context.performed&&currentSpecialWeapon!=null) {
            if (currentSpecialWeapon.tag == "TorpedoTurret")
            {
                if (torpedoRemain > 0)
                {
                    TorpedoList[torpedoRemain - 1].transform.position = TorpedoShootingPoint.transform.position;
                    TorpedoList[torpedoRemain - 1].transform.parent = null;
                    RedTorpedoAttack tp = TorpedoList[torpedoRemain - 1].GetComponent<RedTorpedoAttack>();
                    BlueTorpedoAttack tp1 = TorpedoList[torpedoRemain - 1].GetComponent<BlueTorpedoAttack>();
                    if (tp != null)
                        tp.enabled = true;
                    if (tp1 != null)
                        tp1.enabled = true;
                    Collider collider = TorpedoList[torpedoRemain - 1].GetComponent<Collider>();
                    collider.enabled = true;
                    torpedoRemain--;
                }
                if (torpedoRemain <= 0)
                {
                    Destroy(currentSpecialWeapon);
                    torpedoRemain = 3;
                    canonAttack = true;
                    turretBase = canon;
                }
            }
        }
    }
    public bool returnSpecialWeapon()
    {
        bool noWeapon = false;
        noWeapon = currentSpecialWeapon == null ? true : false;
        return noWeapon;
    }
    public void getEquipment(int equipmentNumber)
    {
        currentSpecialWeapon = Instantiate(specialWeaponList[equipmentNumber], specialWeapon.transform);
        
        if(currentSpecialWeapon.tag == "TorpedoTurret")
        {
            TorpedoList[0] = currentSpecialWeapon.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
            TorpedoList[1] = currentSpecialWeapon.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
            TorpedoList[2] = currentSpecialWeapon.transform.GetChild(2).GetChild(0).GetChild(0).gameObject;
        }
    }
}
