using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement_Normal : MonoBehaviour
{
    [Range(0, 10)]
    public float speedmult;

    public Transform Camera;

    Vector2 moveVal;

    void OnMove(InputValue value)
    {
        moveVal = value.Get<Vector2>();
    }

    void OnJump()
    {
        this.GetComponent<Rigidbody>().AddForce(new Vector3(0, 500, 0));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var forward = Camera.forward;
        var right = Camera.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        var direction = forward * moveVal.y + right * moveVal.x;

        transform.Translate(direction * Time.deltaTime * speedmult);
    }
}
