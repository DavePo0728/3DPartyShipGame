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
    Vector2 movementInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void GetMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        Debug.Log(movementInput.x + "+" + movementInput.y);
    }
    void Sprint()
    {

    }
    // Update is called once per frame
    void Update()
    {
        

        Vector3 movement = new Vector3(-movementInput.x, 0f, -movementInput.y);

        shipOne.MovePosition(shipOne.position + movement* speed * Time.deltaTime);
    }
}
