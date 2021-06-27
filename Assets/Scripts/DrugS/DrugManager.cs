using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugManager : MonoBehaviour
{
    DrugLocation[] AllDrugsInScene;

    private void Awake()
    {
        AllDrugsInScene = FindObjectsOfType<DrugLocation>();
    }

    public void RespawnDrugs()
    {
        foreach(DrugLocation drugs in AllDrugsInScene)
        {
            drugs.RespawnDrug();
        }
    }
}
