using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    #region Singleton
    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this);
    }
    #endregion

    #region Components
    [Header("# Components")]
    [SerializeField]
    private SpriteRenderer playerSpriteRenderer;

    [SerializeField]
    private BoxCollider2D playerCol;

    [SerializeField]
    private Rigidbody2D playerRigidbody;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private Animator playerAnim;

    [SerializeField]
    private BoxCollider2D attackPoint;
    [SerializeField]
    private BoxCollider2D attackPoint2;
    [SerializeField]
    private BoxCollider2D guardPoint;

    [SerializeField]
    private Parameter parameter;
    #endregion
    [Space(10)]

    #region Variables
    [Header("# Variables")]

    #region Player Movement
    [Header("Player Movement")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpForce;
    private bool isMove;
    #endregion
    [Space(5)]
    #region Player State
    [Header("Player State")]
    private bool isGround;
    #endregion
    [Space(5)]
    #region Attack
    [Header("Attack")]
    [SerializeField]
    private float atkSpeed;
    [SerializeField]
    private float atkTime;
    [SerializeField]
    private float currentAtkTime;
    [SerializeField]
    private float comboTime;
    [SerializeField]
    private float currentComboTime;
    private bool isCombo;
    #endregion
    #region Guard
    private bool isGuard;
    [SerializeField]
    private float guardTime;
    [SerializeField]
    private float currentGuardTime;
    #endregion
    [Space(5)]
    #region etc
    private WaitForSeconds waitTime = new WaitForSeconds(0.1f);
    private WaitForEndOfFrame frameTime = new WaitForEndOfFrame();

    #endregion
    #endregion
    void Start()
    {
        playerAnim.SetBool("Grounded", true);
    }

    // Update is called once per frame
    void Update()
    {
        //CheckGround();
        CheckAirSpeed();
        TryJump();
        PlayerMove();      
        TryAttack();
        ComboCheck();
        TryGuard();
    }

    #region Player Movement
    private void PlayerMove()
    {
        if (Input.GetButton("Horizontal"))
        {
            if (isGround && !isGuard)
            {
                if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    playerSpriteRenderer.flipX = true;
                    attackPoint.transform.localPosition = new Vector3(-0.249f, 0.028f, 0);
                    attackPoint2.transform.localPosition = new Vector3(-0.249f, 0.028f, 0);
                    guardPoint.transform.localPosition = new Vector3(-0.138f, 0, 0);
                }
                else
                {
                    playerSpriteRenderer.flipX = false;
                    attackPoint.transform.localPosition = new Vector3(0.248f, 0.028f, 0);
                    attackPoint2.transform.localPosition = new Vector3(0.248f, 0.028f, 0);
                    guardPoint.transform.localPosition = new Vector3(0.137f, 0, 0);
                }
                isMove = true;
                playerAnim.SetInteger("AnimState", 1);
                Vector3 _moveHorizontal = transform.right * Input.GetAxisRaw("Horizontal");
                Vector3 _vel = moveSpeed * _moveHorizontal;
                playerRigidbody.MovePosition(transform.position + _vel * Time.deltaTime);
            }

            else
            {

            }
        }
        else
        {
            playerAnim.SetInteger("AnimState", 0);
            isMove = false;
        }
    }

    private void TryJump()
    {
        if(Input.GetKeyDown(KeyCode.Z)&& isGround && !isGuard)
        {
            isGround = false;
            Jump();
        }
    }
    
    private void Jump()
    {
        playerRigidbody.velocity = playerTransform.up * jumpForce;
        playerAnim.SetTrigger("Jump");
        playerAnim.SetBool("Grounded", isGround);    
    }

    #endregion

    #region Player Action
    private void TryAttack()
    {
        if (currentAtkTime >= atkTime)
        {
            if (Input.GetKeyDown(KeyCode.X) && !isMove && !isGuard && parameter.currentSp > 0)
            {
                StartCoroutine(Attack());
                currentAtkTime = 0f;
                parameter.currentSp -= 20;
            }
            
        }
        else
            currentAtkTime += Time.deltaTime;
    }

    private IEnumerator Attack()
    {

        
        if (currentComboTime == 0f)
        { 
            playerAnim.SetTrigger("Attack1");
            yield return waitTime;
            attackPoint.gameObject.SetActive(true);
            isCombo = true;
            yield return waitTime;
            attackPoint.gameObject.SetActive(false);
        }
        else if(currentComboTime <= comboTime && 0 < currentComboTime)
        {
            playerAnim.SetTrigger("Attack2");
            attackPoint2.gameObject.SetActive(true);
            yield return waitTime;
            currentComboTime = 0f;
            isCombo = false;
            attackPoint2.gameObject.SetActive(false);
        }
       
    }

    private void ComboCheck()
    {
        if(isCombo)
        {
            currentComboTime += Time.deltaTime;
        }
        if(currentComboTime >= comboTime)
        {
            isCombo = false;
            currentComboTime = 0f;
        }
    }

    private void TryGuard()
    {
        if (currentGuardTime >= guardTime)
        {
            if (Input.GetKeyDown(KeyCode.C) && !isMove)
            {
                Guard();
                currentGuardTime = 0f;
            }

            
        }
        
        else
            currentGuardTime += Time.deltaTime;
        if (Input.GetKeyUp(KeyCode.C))
        {
            GuardCancel();
        }
    }

    private void Guard()
    {
        isGuard = true;
        playerAnim.SetTrigger("Block");
        guardPoint.gameObject.SetActive(true);
        playerAnim.SetBool("IdleBlock", true);
    }

    private void GuardCancel()
    {
        isGuard = false;
        
        playerAnim.SetBool("IdleBlock", false);
        guardPoint.gameObject.SetActive(false);
    }
    #endregion

    #region Player State
    private void CheckGround()
    {
        isGround = Physics2D.Raycast(playerTransform.position, Vector3.down, 0.01f);
        Debug.Log($"playerTansform = {playerTransform.position}");
        playerAnim.SetBool("Grounded", isGround);
    }
    private void CheckAirSpeed()
    {
        playerAnim.SetFloat("AirSpeedY", playerRigidbody.velocity.y);
        if(playerRigidbody.velocity.y == 0)
        {
            isGround = true;
            playerAnim.SetBool("Grounded", isGround);
        }
    }
    #endregion
}
