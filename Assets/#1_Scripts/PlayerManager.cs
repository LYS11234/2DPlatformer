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
    public SpriteRenderer playerSpriteRenderer;

    [SerializeField]
    public BoxCollider2D playerCol;

    [SerializeField]
    public Rigidbody2D playerRigidbody;

    [SerializeField]
    public Transform playerTransform;

    [SerializeField]
    public Animator playerAnim;

    [SerializeField]
    public BoxCollider2D attackPoint;
    [SerializeField]
    public BoxCollider2D attackPoint2;
    [SerializeField]
    public GameObject guardPoint;

    [SerializeField]
    public Parameter parameter;

    [SerializeField]
    public DieMessage dieMessage;

    [SerializeField]
    public ButtonGUI gui;

    public Camera camera;
    #endregion
    [Space(10)]

    

    #region Variables
    [Header("# Variables")]

    #region Player Movement
    [Header("Player Movement")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    public float jumpForce;
    public bool isMove;
    public Vector3 jumpDirectionR;
    public Vector3 jumpDirectionL;
    #endregion
    [Space(5)]
    #region Player State
    [Header("Player State")]
    public bool isGround;
    public bool canMove = true;
    public bool isDead;
    public bool isKnockBack;
    [SerializeField]
    private float knockBackTime;
    [SerializeField]
    private float currentKnockBackTime;
    #endregion
    [Space(5)]
    #region Attack
    [Header("Attack")]

    public bool isAttack;
    [SerializeField]
    private float atkSpeed;
    [SerializeField]
    public float atkTime;
    [SerializeField]
    public float currentAtkTime;
    [SerializeField]
    public float comboTime;
    [SerializeField]
    public float currentComboTime;
    public bool isCombo;
    public bool canAttack = true;
    #endregion
    #region Guard
    public bool isGuard;
    [SerializeField]
    public float guardTime;
    [SerializeField]
    public float currentGuardTime;
    [SerializeField]
    public bool isParry;
    [SerializeField]
    public float currentParryTime;
    [SerializeField]
    public float parryTime;
    #endregion
    [Space(5)]
    #region Roll
    [Header("Roll")]
    [SerializeField]
    public float currentRollTime;
    [SerializeField]
    public float rollTime;
    public bool isRoll;
    #endregion
    #region etc
    public WaitForSeconds waitTime = new WaitForSeconds(0.1f);
    public WaitForEndOfFrame frameTime = new WaitForEndOfFrame();
    [SerializeField]
    Vector2 knockBackForce;
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
    void FixedUpdate()
    {
        if (!isDead)
        {
            //CheckGround();
            CheckAirSpeed();
            PlayerMove();
            KnockBack();
            Dead();
        }
    }

    #region Player Movement
    private void PlayerMove()
    {
        if (Input.GetButton("Horizontal"))
        {
            Debug.Log($"Player Can Move: {canMove}");
            if (isGround && !isGuard && !isAttack && canMove)
            {
                if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    playerSpriteRenderer.flipX = true;
                    attackPoint.transform.localPosition = new Vector3(-0.249f, 0.028f, 0);
                    attackPoint2.transform.localPosition = new Vector3(-0.249f, 0.028f, 0);
                }
                else
                {
                    playerSpriteRenderer.flipX = false;
                    attackPoint.transform.localPosition = new Vector3(0.248f, 0.028f, 0);
                    attackPoint2.transform.localPosition = new Vector3(0.248f, 0.028f, 0);
                }
                isMove = true;
                playerAnim.SetInteger("AnimState", 1);

                Vector3 _moveHorizontal = playerTransform.right * Input.GetAxisRaw("Horizontal");
                Vector3 _vel = moveSpeed * _moveHorizontal;
                playerRigidbody.MovePosition(playerTransform.position + _vel * Time.smoothDeltaTime);
            }
        }
        else
        {
            playerAnim.SetInteger("AnimState", 0);
            isMove = false;
        }
    }

    



    #endregion

    #region Player Action
   

   

    //private void TryRoll()
    //{
    //    if (currentRollTime >= rollTime)
    //    {
    //        if (Input.GetKeyDown(KeyCode.Space))
    //        {
    //            if (parameter.currentSp > 0)
    //            {
    //                if (parameter.currentSp - 30 >= 0)
    //                    parameter.currentSp -= 30;
    //                else
    //                    parameter.currentSp = 0;
    //                isAttack = true;
    //                StartCoroutine(Roll());
    //                currentRollTime = 0f;   
    //            }
    //        }
    //    }
    //    else
    //        currentRollTime += Time.deltaTime;
    //}

    //private IEnumerator Roll()
    //{
    //    playerAnim.SetTrigger("Roll");
        
    //    int _direction;
    //    if (playerSpriteRenderer.flipX)
    //        _direction = -1;
    //    else
    //    {
    //        _direction = 1;
    //    }
    //    Vector3 _moveHorizontal = transform.right * _direction;
    //    Vector3 _vel = 3f * _moveHorizontal;
    //    isRoll = true;
    //    yield return frameTime;
    //    for (int i = 0; i < 30; i++)
    //    {
    //        yield return frameTime;
    //        playerRigidbody.MovePosition(transform.position + _vel * Time.smoothDeltaTime);
    //        yield return frameTime;
    //    }
    //    isRoll = false;
    //    isAttack = false;
    //}
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
            dieMessage.gameObject.SetActive(true);
        }
    }

    public void KnockBack()
    {
        if(isKnockBack)
        {
            if (currentKnockBackTime <= knockBackTime)
            {
                playerRigidbody.AddForce(knockBackForce);
                if(isParry)
                    currentKnockBackTime = knockBackTime;
                else
                    currentKnockBackTime += Time.deltaTime;
                canMove = false;
                canAttack = false;
            }
            else
            {
                currentKnockBackTime = 0;
                isKnockBack = false;
                canAttack = true;
                canMove = true;
            }
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
