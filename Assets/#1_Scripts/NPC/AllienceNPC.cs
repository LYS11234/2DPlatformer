using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllienceNPC : NPC
{
    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.X))
        {

        }
    }
}
