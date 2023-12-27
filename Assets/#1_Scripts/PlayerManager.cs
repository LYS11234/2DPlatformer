using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            Destroy(this.gameObject);
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

    [SerializeField]
    public ButtonGUI gui;
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
    private Vector3 jumpDirectionR;
    private Vector3 jumpDirectionL;
    #endregion
    [Space(5)]
    #region Player State
    [Header("Player State")]
    public bool isGround;
    public bool canMove = true;
    public bool isDead;
    #endregion
    [Space(5)]
    #region Attack
    [Header("Attack")]

    public bool isAttack;
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
    public bool canAttack = true;
    #endregion
    #region Guard
    public bool isGuard;
    [SerializeField]
    private float guardTime;
    [SerializeField]
    private float currentGuardTime;
    #endregion
    [Space(5)]
    #region Roll
    [Header("Roll")]
    [SerializeField]
    private float currentRollTime;
    [SerializeField]
    private float rollTime;
    public bool isRoll;
    #endregion
    #region etc
    private WaitForSeconds waitTime = new WaitForSeconds(0.1f);
    private WaitForEndOfFrame frameTime = new WaitForEndOfFrame();
    #endregion
    #endregion
    void Start()
    {
        playerAnim.SetBool("Grounded", true);
        jumpDirectionL.Set(-1,4,0);
        jumpDirectionR.Set(1,4,0);
        jumpDirectionL = jumpDirectionL.normalized;
        jumpDirectionR = jumpDirectionR.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            //CheckGround();
            CheckAirSpeed();
            TryJump();
            PlayerMove();
            TryAttack();
            ComboCheck();
            TryGuard();
            TryRoll();
            Dead();
        }
    }

    #region Player Movement
    private void PlayerMove()
    {
        if (Input.GetButton("Horizontal"))
        {
            if (isGround && !isGuard && !isAttack && canMove)
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
        }
        else
        {
            playerAnim.SetInteger("AnimState", 0);
            isMove = false;
        }
    }

    private void TryJump()
    {
        if(Input.GetKeyDown(KeyCode.Z)&& isGround && !isGuard && canMove && Parameter.instance.currentSp > 0)
        {
            isGround = false;
            Jump();
        }
    }
    
    private void Jump()
    {
        if(Input.GetAxisRaw("Horizontal") == 0)
            playerRigidbody.velocity = playerTransform.up * jumpForce;
        
        else if(Input.GetAxisRaw("Horizontal") < 0)
        {
            playerRigidbody.velocity = jumpDirectionL * jumpForce;
        }

        else if(Input.GetAxisRaw("Horizontal") > 0)
            playerRigidbody.velocity = jumpDirectionR * jumpForce;
        if(parameter.currentSp >= 100)
            parameter.currentSp -= 100;
        else
            parameter.currentSp = 0;
        
        playerAnim.SetTrigger("Jump");
        playerAnim.SetBool("Grounded", isGround);
    }



    #endregion

    #region Player Action
    private void TryAttack()
    {
        if (currentAtkTime >= atkTime)
        {
            if (Input.GetKeyDown(KeyCode.X) && !isMove && !isGuard && parameter.currentSp > 0 && canAttack)
            {
                isAttack = true;
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
        isAttack = false;

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
            if (Input.GetKeyDown(KeyCode.C) && !isMove && canAttack)
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
        Guard guard = guardPoint.GetComponent<Guard>();
        guard.currentParryTime = 0;
        playerAnim.SetBool("IdleBlock", false);
        guardPoint.gameObject.SetActive(false);
    }

    private void TryRoll()
    {
        if (currentRollTime >= rollTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (parameter.currentSp > 0)
                {
                    if (parameter.currentSp - 30 >= 0)
                        parameter.currentSp -= 30;
                    else
                        parameter.currentSp = 0;
                    isAttack = true;
                    StartCoroutine(Roll());
                    currentRollTime = 0f;   
                }
            }
        }
        else
            currentRollTime += Time.deltaTime;
    }

    private IEnumerator Roll()
    {
        playerAnim.SetTrigger("Roll");
        
        int _direction;
        if (playerSpriteRenderer.flipX)
            _direction = -1;
        else
        {
            _direction = 1;
        }
        Vector3 _moveHorizontal = transform.right * _direction;
        Vector3 _vel = 3f * _moveHorizontal;
        isRoll = true;
        yield return frameTime;
        for (int i = 0; i < 30; i++)
        {
            yield return frameTime;
            playerRigidbody.MovePosition(transform.position + _vel * Time.deltaTime);
            yield return frameTime;
        }
        isRoll = false;
        isAttack = false;
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

    private void Dead()
    {
        if(Parameter.instance.currentHp <= 0)
        {
            Parameter.instance.currentHp = 0;
            playerAnim.SetTrigger("Death");
            isDead = true;
        }
    }
    #endregion

    #region Extra

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "NPC")
        {
            gui.image.gameObject.SetActive(true);
            gui.text.text = "Press X to Talk.";
        }
        else if(collision.tag == "Gate")
        {
            gui.image.gameObject.SetActive(true);
            gui.text.text = "Press X to leave this map.";
        }
        else if(collision.tag == "Tool")
        {
            gui.image.gameObject.SetActive(true);
            gui.text.text = "Press X to Use.";
        }
        else if(collision.tag == "Campfire")
        {
            gui.image.gameObject.SetActive(true);
            gui.text.text = "Press X to Rest.";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gui.text.text = "";
        gui.image.gameObject.SetActive(false);
    }

    #endregion
}
