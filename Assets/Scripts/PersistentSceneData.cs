using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentSceneData : MonoBehaviour
{
    public int DrugsPickedUp;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void IncreasePickedup()
    {
        DrugsPickedUp++;
    }

    public int GetDrugs()
    {
        return DrugsPickedUp;
    }

}
