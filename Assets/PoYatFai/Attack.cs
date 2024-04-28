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
    [SerializeField]
    GameObject specialWeapon;
    [SerializeField]
    GameObject currentSpecialWeapon;
    bool canonAttack =true;
    private float timeBetweenShots;
    private float timeSinceLastShot = 0.0f;


    [SerializeField]
    float rotateSpeed;
    Vector2 aimInput;
    bool rtDown = false;

    // Start is called before the first frame update
    void Start()
    {
        timeBetweenShots = 1 / (210 / 60.0f);
        turretBase = canon;
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
            
        }
        
    }
    public void ChangeTurret(InputAction.CallbackContext context)
    {
        if (context.started) {
            if (turretBase == specialWeapon)
            {
                canonAttack = true;
                turretBase = canon;
            } else if (turretBase == canon)
            {
                turretBase = specialWeapon;
                canonAttack = false;
            }
            //Debug.Log(turretBase.name);
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
        if (canonAttack==false&&context.performed) { 
        
        }
    }
}
