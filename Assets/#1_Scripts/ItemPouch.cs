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
    [SerializeField]
    private CoinManager coinManager;

    private void Start()
    {
        inven = Parameter.instance.gameObject.GetComponent<Inventory>();
    }

    private void Update()
    {
        if (canPick && Input.GetKeyDown(KeyCode.V) && canPick)
        { 
            for (int i = 0; i < items.Length; i++)
            { 
                inven.AcquireItem(items[i]);
                
            }
            Database.Instance.gold += coinManager.value;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"collider Found! {collision.gameObject.name}");
        if (collision.gameObject.name == "Player") 
        {
            canPick = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            canPick = false;
        }
    }
}
