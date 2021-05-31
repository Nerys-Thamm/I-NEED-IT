using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement_Normal : MonoBehaviour
{
    [Range(1, 10)]
    public float speed_mult;

    [Range(1, 50)]
    public float camera_rotate_sensitivity;

    public Transform Camera;
    public Transform FollowCam;

    Vector2 moveVal;
    Vector3 direction;
    float cameraangledelta;
    void OnMove(InputValue value)
    {
        moveVal = value.Get<Vector2>();
    }

    void OnJump()
    {
        this.GetComponent<Rigidbody>().AddForce(new Vector3(0, 500, 0));
    }

    void OnRotateCamera(InputValue value)
    {
        cameraangledelta = value.Get<float>();
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

        direction = forward * moveVal.y + right * moveVal.x;

        transform.Translate(direction * Time.deltaTime * speed_mult);
        FollowCam.Rotate(new Vector3(0, cameraangledelta * Time.deltaTime * camera_rotate_sensitivity, 0));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(this.gameObject.transform.position, this.gameObject.transform.position + (direction * speed_mult));
    }
}
