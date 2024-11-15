using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D collider2D;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetPos;
    [SerializeField] private float crouchHeight = 0.5f;
    [SerializeField] private float groundDistance = 0.25f;
    [SerializeField] private float crouchHeightFactor = 0.3f;
    [SerializeField] private float jumpTime = 0.3f;
    [SerializeField] private Animator animator;

    private bool isGrounded = false;
    private bool isJumping = false;
    private float jumpTimer;
    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;



    void Start()
    {
        originalColliderSize = collider2D.size;
        originalColliderOffset = collider2D.offset;
    }


    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);

        animator.SetBool("isGrounded", isGrounded);



        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimer = 0;
            rb.linearVelocity = Vector2.up * jumpForce;
            animator.SetTrigger("Jump");
        }
        if (isJumping && Input.GetButton("Jump"))
        {
            if (jumpTimer < jumpTime)
            {
                rb.linearVelocity = Vector2.up * jumpForce;

                jumpTimer += Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            jumpTimer = 0;
        }


        if (isGrounded && Input.GetButton("Crouch"))
        {
            collider2D.size = new Vector2(originalColliderSize.x, originalColliderSize.y * crouchHeightFactor);
            collider2D.offset = new Vector2(originalColliderOffset.x, originalColliderOffset.y * crouchHeightFactor);
            animator.SetBool("isCrouching", true);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            collider2D.size = originalColliderSize;
            collider2D.offset = originalColliderOffset;
            animator.SetBool("isCrouching", false);
        }

        if (!isJumping && !Input.GetButton("Crouch"))
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

    }
}