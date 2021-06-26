using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Movement_Normal player;

    public void SetCanMove(int type)
    {
        player.CanMove = type == 1 ? true : false;
    }
}
