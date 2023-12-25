using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPouch : MonoBehaviour
{
    [SerializeField]
    public Item[] items;
    [SerializeField]
    private bool canPick;
    [SerializeField]
    private Inventory inven;

    private void Start()
    {
        inven = Parameter.instance.gameObject.GetComponent<Inventory>();
    }

    private void Update()
    {
        if (canPick && Input.GetKeyDown(KeyCode.V))
        { 
            for (int i = 0; i < items.Length; i++)
            { 
                inven.AcquireItem(items[i]);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player") 
        {
            canPick = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            canPick = false;
        }
    }
}
