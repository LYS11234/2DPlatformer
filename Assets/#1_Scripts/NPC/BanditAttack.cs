using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Schema;

public class BanditAttack : Action
{
    [SerializeField]
    private BanditManager banitManager;
    public override NodeStatus Tick(object nodeMemory, SchemaAgent agent)
    {
        banitManager.Attack();
        return NodeStatus.Success;
    }
}
