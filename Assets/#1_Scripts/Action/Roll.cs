using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Roll : MonoBehaviour
{
    
    void Update()
    {
        TryRoll();
    }

    private void TryRoll()
    {
        if (PlayerManager.instance.currentRollTime >= PlayerManager.instance.rollTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (PlayerManager.instance.parameter.currentSp > 0)
                {
                    if (PlayerManager.instance.parameter.currentSp - 30 >= 0)
                        PlayerManager.instance.parameter.currentSp -= 30;
                    else
                        PlayerManager.instance.parameter.currentSp = 0;
                    PlayerManager.instance.isAttack = true;
                    StartCoroutine(Rolling());
                    PlayerManager.instance.currentRollTime = 0f;
                }
            }
        }
        else
            PlayerManager.instance.currentRollTime += Time.deltaTime;
    }

    private IEnumerator Rolling()
    {
        PlayerManager.instance.playerAnim.SetTrigger("Roll");

        int _direction;
        if (PlayerManager.instance.playerSpriteRenderer.flipX)
            _direction = -1;
        else
        {
            _direction = 1;
        }
        Vector3 _moveHorizontal = PlayerManager.instance.transform.right * _direction;
        Vector3 _vel = 3f * _moveHorizontal;
        PlayerManager.instance.isRoll = true;
        yield return PlayerManager.instance.frameTime;
        for (int i = 0; i < 30; i++)
        {
            yield return PlayerManager.instance.frameTime;
            PlayerManager.instance.playerRigidbody.MovePosition(transform.position + _vel * Time.smoothDeltaTime);
            yield return PlayerManager.instance.frameTime;
        }
        PlayerManager.instance.isRoll = false;
        PlayerManager.instance.isAttack = false;
    }
}
