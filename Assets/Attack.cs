using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    [SerializeField]
    float testX, testY, testZ;
    [SerializeField]
    float rotateSpeed;
    Vector2 aimInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = new Vector3(aimInput.x, 0, aimInput.y);
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);

        float step = rotateSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(lookRotation, transform.rotation, step);
    }
    public void GetRotate(InputAction.CallbackContext context)
    {
        aimInput=context.ReadValue<Vector2>();
        Debug.Log(aimInput.x);
    }
}
