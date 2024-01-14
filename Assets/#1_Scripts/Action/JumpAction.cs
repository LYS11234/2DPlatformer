using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class JumpAction : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (!PlayerManager.instance.isDead)
            TryJump();
    }


    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Z) && PlayerManager.instance.isGround && !PlayerManager.instance.isGuard && PlayerManager.instance.canMove && Parameter.instance.currentSp > 0)
        {
            PlayerManager.instance.isGround = false;
            Jump();
        }
    }

    private void Jump()
    {
        if (Input.GetAxisRaw("Horizontal") == 0)
            PlayerManager.instance.playerRigidbody.velocity = PlayerManager.instance.playerTransform.up * PlayerManager.instance.jumpForce;

        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            PlayerManager.instance.playerRigidbody.velocity = PlayerManager.instance.jumpDirectionL * PlayerManager.instance.jumpForce;
        }

        else if (Input.GetAxisRaw("Horizontal") > 0)
            PlayerManager.instance.playerRigidbody.velocity = PlayerManager.instance.jumpDirectionR * PlayerManager.instance.jumpForce;
        if (PlayerManager.instance.parameter.currentSp >= 100)
            PlayerManager.instance.parameter.currentSp -= 100;
        else
            PlayerManager.instance.parameter.currentSp = 0;

        PlayerManager.instance.playerAnim.SetTrigger("Jump");
        PlayerManager.instance.playerAnim.SetBool("Grounded", PlayerManager.instance.isGround);
    }
}
