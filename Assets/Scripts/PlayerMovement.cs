using UnityEngine;
using Photon.Pun;
public class PlayerMovement : MonoBehaviourPun
{
    private Rigidbody2D rb;
    public LayerMask groundMask;

    [SerializeField] private bool isGrounded;
    public float moveSpeed;
    public float moveInput;
    public bool canJump = true;
    public float jumpValue = 0f;
    public float jumpHeight = 10f;
    public bool isGameStarted;

  
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

        isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f),
        new Vector2(0.9f, 0.4f), 0f, groundMask);

        JumpLogic();
      //  Debug.Log(rb.velocity);
    }

    private void JumpLogic()
    {
        if (Input.GetKey("space") && isGrounded && canJump)
        {
            jumpValue += 0.1f;
        }

        if (jumpValue >= jumpHeight && isGrounded)
        {
            float tempX = moveInput * moveSpeed;
            float tempY = jumpValue;
            rb.velocity = new Vector2(tempX, tempY);
            Invoke("JumpReset", 0.2f);
        }

        if (Input.GetKeyUp("space"))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(moveInput * moveSpeed, jumpValue);
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
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        }
    }

    private void JumpReset()
    {
        canJump = false;
        jumpValue = 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.2f), new Vector2(0.9f, 0.2f));
    }
}
