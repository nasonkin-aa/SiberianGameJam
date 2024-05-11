using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb;
    public float jumpForce;
    public float playerSpeed;
    public float positionRadius;
    public LayerMask ground;
    public Transform playerPos;
    
    [SerializeField]
    private Balance bodyBalance;

    private bool isOnGround;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            // bodyBalance.Active = false;
            foreach (var balance in GetComponentsInChildren<Balance>())
            {
                balance.Active = false;
            }
        }
        
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            // bodyBalance.Active = true;
            foreach (var balance in GetComponentsInChildren<Balance>())
            {
                balance.Active = true;
            }
        }
        
        isOnGround = Physics2D.OverlapCircle(playerPos.position, positionRadius, ground);
        
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            if(Input.GetAxisRaw("Horizontal") > 0)
            {
                anim.Play("Walk");
                rb.AddForce(Vector2.right * playerSpeed * Time.deltaTime);
            }
            else
            {
                anim.Play("WalkBack");
                rb.AddForce(Vector2.left * playerSpeed * Time.deltaTime);
            }
        }
        else
        {
            
            anim.Play("Idle");
        }

        if (isOnGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("JUMPING");
            rb.AddForce(Vector2.up * jumpForce * Time.deltaTime);
        }

    }
}
