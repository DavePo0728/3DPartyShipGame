using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    [SerializeField]
    Rigidbody shipOne;
    [SerializeField]
    float speed;
    float maxVelocity = 4f;
    Vector2 movementInput;
    [SerializeField]
    GameManager shipHp;
    [SerializeField]
    GameObject p1SpawnPoint, p2SpawnPoint;
    Collider PlayerCollider;

    [SerializeField]
    float dashForce;
    bool canDash = true;
    bool isDashing = false;
    float dashingTime = 0.2f;
    float dashCooldown = 1f;

    bool isTouching = false;
    [SerializeField]
    float pushForce;

    [Header("Energy")]
    [SerializeField]
    GameObject energyBar;
    void Start()
    {
        PlayerCollider = gameObject.GetComponent<Collider>();
    }
    public void GetMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        //Debug.Log(movementInput.x + "+" + movementInput.y);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDashing||isTouching)
            return;

        Vector3 movement = new Vector3(-movementInput.x, 0f, -movementInput.y);
        shipOne.AddForce(movement * speed);

        //shipOne.velocity= movement* speed;
        shipOne.velocity = Vector3.ClampMagnitude(shipOne.velocity, maxVelocity);
    }
    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash == true)
        {
            StartCoroutine(OnDash());
        }
    }
    IEnumerator OnTouch()
    {
        isTouching = true;
        yield return new WaitForSeconds(1.0f);
        isTouching = false;
    }
    IEnumerator OnDash()
    {
       // Debug.Log("dash");
        canDash = false;
        isDashing = true;
        shipOne.AddForce(shipOne.velocity*dashForce,ForceMode.Impulse);
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag =="Player"|| collision.gameObject.tag == "Player2")
        {
            StartCoroutine(OnTouch());
            if (isTouching)
            {
                Rigidbody othership;
                othership = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 temp = Vector3.ClampMagnitude(shipOne.velocity * pushForce,pushForce);
                othership.AddForce(temp,ForceMode.Impulse);
                Instantiate(energyBar, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Player")
        {
            if (other.gameObject.tag == "Bullet1")
            {
                shipHp.GetHit(this.gameObject,10);
                Destroy(other.gameObject);
            }
            if (other.gameObject.tag == "Respawn")
            {
                RespawnPlayer(gameObject);
            }
        }
        if (gameObject.tag == "Player2")
        {
            if (other.gameObject.tag == "Bullet")
            {
                shipHp.GetHit(this.gameObject,10);
                Destroy(other.gameObject);
            }
            if (other.gameObject.tag == "Respawn")
            {
                RespawnPlayer(gameObject);
            }
        }
    }
    void RespawnPlayer(GameObject player)
    {
        if(gameObject.tag == "Player")
        {
            shipOne.velocity = Vector3.zero;
            shipHp.GetHit(this.gameObject, 50);
            gameObject.transform.position = p1SpawnPoint.transform.position;
            Physics.IgnoreLayerCollision(7, 6, true);
            StartCoroutine("MuTeKiTime");
        }
        if (gameObject.tag == "Player2")
        {
            shipOne.velocity = Vector3.zero;
            shipHp.GetHit(this.gameObject, 50);
            gameObject.transform.position = p2SpawnPoint.transform.position;
            Physics.IgnoreLayerCollision(7, 6, true);
            StartCoroutine("MuTeKiTime");
        }
    }
    IEnumerator MuTeKiTime()
    {
        yield return new WaitForSeconds(1.0f);
        Physics.IgnoreLayerCollision(7, 6, false);
    }
    
}

