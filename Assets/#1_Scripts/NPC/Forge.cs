using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forge : AllienceNPC
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnTriggerStay2D(Collider2D other)
    {
        base.OnTriggerStay2D(other);
        if(other.gameObject.name == "Player")
        {
            if(Input.GetKeyDown(KeyCode.V))
            {

            }
        }
    }

    //private IEnumerator DialogueCoroutine()
    //{
        
    //}

    private IEnumerator UpgradeCoroutine()
    {
        yield return null;
    }


    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
    }
}
