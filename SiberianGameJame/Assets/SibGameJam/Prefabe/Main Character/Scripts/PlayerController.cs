using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb;
    public float jumpForce;
    public float playerSpeed;
    public float positionRadius;
    public LayerMask ground;
    public Transform playerPos;
    public Camera mainCamera;
    public List<GameObject> moduls;
    public BreakController breakContriller;
    
    [SerializeField]
    private Balance bodyBalance;

    private bool isOnGround;
    private List<ModuleBreak> breaks => breakContriller.breakers;

    void Start()
    {
        breakContriller = GetComponent<BreakController>();
    }

    void Update()
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

        if (Input.GetKeyDown(KeyCode.W))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 0; // Устанавливаем z координату, чтобы соответствовать плоскости камеры
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            transform.position = worldPosition;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            moduls.ForEach(module =>
            {
                if (!module.active)
                {
                    module.SetActive(true);
                    breaks.Add(module.GetComponentInChildren<ModuleBreak>());
                }
            });
        }
    }
}
