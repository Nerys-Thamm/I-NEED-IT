using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzlecubeconstraints : MonoBehaviour
{
    public Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xGrid = ((this.transform.position.x * 10) % 20) / 10;
        float zGrid = ((this.transform.position.z * 10) % 20) / 10;
        //Debug.Log(xGrid + " : " + zGrid);
        body.constraints = RigidbodyConstraints.FreezeRotation;
        if (xGrid < -0.5f && xGrid > -1.5)
        {
            body.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }
        if (zGrid < -0.5f && zGrid > -1.5)
        {
            body.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
        }
    }
}
