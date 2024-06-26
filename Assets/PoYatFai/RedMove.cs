using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RedMove : MonoBehaviour
{
    Rigidbody shipRed;
    [SerializeField]
    GameManager shipHp;
    [SerializeField]
    GameObject p1SpawnPoint;
    [SerializeField]
    Attack redShipAttack;
    //Move
    [SerializeField]
    float speed;
    float maxVelocity = 4f;
    Vector2 movementInput;
    //Dash
    [SerializeField]
    float dashForce;
    bool canDash = true;
    bool isDashing = false;
    float dashingTime = 0.2f;
    float dashCooldown = 3.3f;
    [SerializeField]
    float dashPushForce;
    bool redIsTouching = false;
    [SerializeField]
    Image DashUI;
    [SerializeField]
    float touchForce;

    RedShipEnergyManager redEnergyManager;
    [SerializeField]
    BlueShipEnergyManager blueEnergyManager;
    [SerializeField]
    AudioSource getEnergySound;

    void Start()
    {
        redEnergyManager = gameObject.GetComponent<RedShipEnergyManager>();
        shipRed =GetComponent<Rigidbody>();
    }
    public void GetMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDashing|| redIsTouching)
            return;
        Vector3 movement = new Vector3(-movementInput.x, 0f, -movementInput.y);
        shipRed.velocity = movement * speed;

        //shipRed.velocity= movement* speed;
        shipRed.velocity = Vector3.ClampMagnitude(shipRed.velocity, maxVelocity);
    }
    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash == true)
        {
            StartCoroutine(OnDash());
        }
    }

    IEnumerator OnDash()
    {
        // Debug.Log("dash");
        canDash = false;
        isDashing = true;
        shipRed.AddForce(shipRed.velocity * dashForce, ForceMode.Impulse);
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        StartCoroutine(StartCountdown());
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    IEnumerator StartCountdown()
    {
        float duration = dashCooldown;
        float totalTime = 0;
        float startTime = Time.time;

        while (totalTime <= duration)
        {
            totalTime = Time.time - startTime;
            float currentValue = totalTime / duration;
            DashUI.fillAmount = currentValue;
            yield return null;
        }
    }
    IEnumerator OnTouch()
    {
        redIsTouching = true;
        yield return new WaitForSeconds(0.7f);
        redIsTouching = false;
    }
    void RespawnPlayer(GameObject player)
    {
            shipRed.velocity = Vector3.zero;
            shipHp.GetHit(this.gameObject, 50);
            gameObject.transform.position = p1SpawnPoint.transform.position;
            Physics.IgnoreLayerCollision(7, 6, true);
            StartCoroutine("MuTeKiTime");
    }
    IEnumerator MuTeKiTime()
    {
        yield return new WaitForSeconds(1.0f);
        Physics.IgnoreLayerCollision(7, 6, false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player2")
        {
            StartCoroutine(OnTouch());
            if (redIsTouching && !isDashing)
            {
                Rigidbody othership;
                othership = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 temp = Vector3.ClampMagnitude(shipRed.velocity * touchForce, touchForce);
                Vector3 temp1 = Vector3.ClampMagnitude(-shipRed.velocity * touchForce, -touchForce);
                othership.AddForce(temp, ForceMode.Impulse);
                shipRed.AddForce(temp1, ForceMode.Impulse);
                //Debug.Log("Redtouch");
            }
            if (redIsTouching&&isDashing)
            {
                Rigidbody othership;
                othership = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 temp = Vector3.ClampMagnitude(shipRed.velocity * dashPushForce, dashPushForce);
                othership.AddForce(temp, ForceMode.Impulse);
                blueEnergyManager.DropEnergy();
                //Debug.Log("RedDashtouch");

            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "torpedo1")
        {
            shipHp.GetHit(this.gameObject, 50);
        }
        if (other.gameObject.tag == "Bullet1")
        {
            shipHp.GetHit(this.gameObject, 10);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Respawn")
        {
            RespawnPlayer(gameObject);
        }
        if (other.gameObject.tag == "EnergyBar")
        {
            redEnergyManager.GetEnergy(other.gameObject);
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "Equipment"&& redShipAttack.returnSpecialWeapon())
        {
            redShipAttack.getEquipment(0);
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "Equipment1"&& redShipAttack.returnSpecialWeapon())
        {
            redShipAttack.getEquipment(1);
            Destroy(other.gameObject);
        }
    }
}
