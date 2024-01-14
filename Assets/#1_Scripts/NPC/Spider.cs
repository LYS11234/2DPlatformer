using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Monster
{    
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!isDead)
        {
            FindPlayer();
        }
    }

    

    //protected override IEnumerator Move(int _direction)
    //{
    //    base.Move(_direction);
    //}

    //protected override private IEnumerator MoveCoroutine(int _direction)
    //{
    //    base.MoveCoroutine(_direction);
    //}

    protected override void DropItem(Transform _transform)
    {
        base.DropItem(_transform);
    }

    protected override void Dead()
    {
        anim.SetBool("isMove", false);
        sprite.flipY = true;
        base.Dead();

    }

    protected override void OnCollisionStay2D(Collision2D col)
    {
        base.OnCollisionStay2D(col);

    }

    protected override void FindPlayer()
    {
        base.FindPlayer();
    }
}
