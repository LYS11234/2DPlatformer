using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private float damage;

    private WaitForSeconds waitTime = new WaitForSeconds(0.3f);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Attack traget: {collision.transform.name}");
        if (collision != null)
            collision.GetComponent<Monster>().Damage(damage);
        this.transform.localPosition = new Vector3(this.transform.localPosition.x + 0.001f, 0.028f, 0);
        this.transform.localPosition = new Vector3(this.transform.localPosition.x - 0.001f, 0.028f, 0);
    }

}
