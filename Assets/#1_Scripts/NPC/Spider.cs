using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Monster
{
    [SerializeField]
    private int direction;
    [SerializeField]
    private int moveCount;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float moveTime;
    [SerializeField]
    private float currentMoveTime;
    private WaitForSeconds waitTime = new WaitForSeconds(0.1f);

    protected override void Update()
    {
        base.Update();
        if(canMove)
            RandomDirection();
    }

    private void RandomDirection()
    {
        if (currentMoveTime >= moveTime)
        {
            direction = Random.Range(-1, 2);
            moveCount = Random.Range(0, 6);
            Move(direction);
            currentMoveTime = 0;
        }
        else
            currentMoveTime += Time.deltaTime;
    }

    private void Move(int _direction)
    {
        StartCoroutine(MoveCoroutine(_direction));
    }

    private IEnumerator MoveCoroutine(int _direction)
    {
        if (_direction != 0)
        {
            if (_direction < 0)
                sprite.flipX = true;
            else if (_direction > 0)
                sprite.flipX = false;

            Vector3 _vel = transform.right * speed * _direction;
            for (int i = 0; i < moveCount; i++)
            {
                mobRd.MovePosition(transform.position + _vel * Time.deltaTime);
                yield return waitTime;
            }

            anim.SetBool("isMove", true);
        }
        if (_direction == 0 || moveCount == 0)
        {
            anim.SetBool("isMove", false);
        }
    }

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
}
