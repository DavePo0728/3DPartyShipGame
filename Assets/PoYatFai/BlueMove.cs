using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BlueMove : MonoBehaviour
{

    Rigidbody shipBlue;
    [SerializeField]
    GameManager shipHp;
    [SerializeField]
    GameObject p2SpawnPoint;
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
    [SerializeField]
    Image DashUI;
    //Touch
    bool blueIsTouching = false;
    [SerializeField]
    float touchForce;

    BlueShipEnergyManager blueEnergyManager;
    [SerializeField]
    RedShipEnergyManager redEnergyManager;


    void Start()
    {
        blueEnergyManager = gameObject.GetComponent<BlueShipEnergyManager>();
        shipBlue = GetComponent<Rigidbody>();
    }
    public void GetMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        //Debug.Log(movementInput.x + "+" + movementInput.y);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDashing||blueIsTouching)
            return;
        Vector3 movement = new Vector3(-movementInput.x, 0f, -movementInput.y);
        shipBlue.velocity =movement * speed;

        //shipOne.velocity= movement* speed;
        shipBlue.velocity = Vector3.ClampMagnitude(shipBlue.velocity, maxVelocity);
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
        shipBlue.AddForce(shipBlue.velocity * dashForce, ForceMode.Impulse);
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
    void RespawnPlayer(GameObject player)
    {
            shipBlue.velocity = Vector3.zero;
            shipHp.GetHit(this.gameObject, 50);
            gameObject.transform.position = p2SpawnPoint.transform.position;
            Physics.IgnoreLayerCollision(7, 6, true);
            StartCoroutine("MuTeKiTime");
    }
    IEnumerator MuTeKiTime()
    {
        yield return new WaitForSeconds(1.0f);
        Physics.IgnoreLayerCollision(7, 6, false);
    }
    IEnumerator OnTouch()
    {
        blueIsTouching = true;
        yield return new WaitForSeconds(0.7f);
        blueIsTouching = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(OnTouch());
            if (blueIsTouching)
            {
                Rigidbody othership;
                othership = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 temp = Vector3.ClampMagnitude(shipBlue.velocity * touchForce, touchForce);
                Vector3 temp1 = Vector3.ClampMagnitude(-shipBlue.velocity * touchForce, -touchForce);
                othership.AddForce(temp, ForceMode.Impulse);
                shipBlue.AddForce(temp1, ForceMode.Impulse);
                Debug.Log("Bluetouch");
            }
            if (blueIsTouching&&isDashing)
            {
                Rigidbody othership;
                othership = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 temp = Vector3.ClampMagnitude(shipBlue.velocity * dashPushForce, dashPushForce);
                othership.AddForce(temp, ForceMode.Impulse);
                redEnergyManager.DropEnergy();
                Debug.Log("BlueDashtouch");

            }
        }   
    }
    private void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.tag == "Bullet")
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
                blueEnergyManager.GetEnergy(other.gameObject);
                Destroy(other.gameObject);
            }
    }
}
