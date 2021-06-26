using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnZone : MonoBehaviour
{
    public Transform Location;
    public BoxCollider TriggerBox;
    private void Awake()
    {
        TriggerBox = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Movement_Normal CharMotor = other.GetComponent<Movement_Normal>();

            CharMotor.RespawnLocation = Location;
        }
    }

    private void OnDrawGizmos()
    {
        Color test = Color.green;
        test.a = 0.2f;
        Gizmos.color = test;
        //Gizmos.DrawWireCube(transform.position + TriggerBox.center, TriggerBox.size);
        Gizmos.DrawCube(transform.position + TriggerBox.center, TriggerBox.size);
    }
}
