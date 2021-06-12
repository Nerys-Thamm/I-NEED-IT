using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Pickupable Object that causes the drug effect
/// </summary>
public class DrugPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Movement_Normal>().pickedUp = true;
            Destroy(this.gameObject);
        }
    }
}
