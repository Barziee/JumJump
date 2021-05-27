using UnityEngine;
using Photon.Pun;
public class PlayerMovement : MonoBehaviourPun
{
    private Rigidbody2D rb;


    [SerializeField] private bool isGrounded;
    [SerializeField] PlayerSettings playerSettings;


    public float moveInput;
    public bool canJump = true;
    public float jumpValue = 0f;

    public bool isGameStarted;



    float tempX, tempY;


    public bool IsGround { get => isGrounded; set => isGrounded = value; }
    public void InitMovement()
    {

        rb = gameObject.GetComponent<Rigidbody2D>();

        if (!rb)
            Debug.LogError("PlayerMovement: Rigidbody not found!");
      
    }
    void Update()
    {
        if (isActiveAndEnabled == false || isGameStarted == false 
            || (photonView.IsMine == false && PhotonNetwork.IsConnected == true))
            return;

        if (rb == null)
            InitMovement();



        moveInput = Input.GetAxisRaw("Horizontal");

        JumpLogic();
      //  Debug.Log(rb.velocity);
    }

    private void JumpLogic()
    {
        if (Input.GetKey("space") && isGrounded && canJump)
        {
            jumpValue += 0.1f;
        }

        if (jumpValue >= playerSettings.JumpHeight && isGrounded)
        {
             tempX = moveInput * playerSettings.MoveSpeed;
             tempY = jumpValue;
            rb.velocity = new Vector2(tempX, tempY);
            Invoke("JumpReset", 0.1f);
        }

        if (Input.GetKeyUp("space"))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(moveInput * playerSettings.MoveSpeed, jumpValue);
                jumpValue = 0f;
            }
            canJump = true;
        }

        if (Input.GetKeyDown("space") && isGrounded && canJump)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        if (jumpValue == 0f && isGrounded)
        {
            rb.velocity = new Vector2(moveInput * playerSettings.MoveSpeed, rb.velocity.y);

        }
    }

    private void JumpReset()
    {
        canJump = false;
        jumpValue = 0;
    }

}
