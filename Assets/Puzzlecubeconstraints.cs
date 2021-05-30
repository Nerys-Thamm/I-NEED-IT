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
        if (Mathf.Abs(body.velocity.x) < 0)
        {
            body.constraints = RigidbodyConstraints.FreezePositionZ;
        }
        if (Mathf.Abs(body.velocity.z) < 0)
        {
            body.constraints = RigidbodyConstraints.FreezePositionX;
        }
    }
}
