using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    BoxCollider2D bound;
    void Start()
    {
        bound = this.GetComponent<BoxCollider2D>();
        CameraManager.instance.bound = bound;
    }
}
