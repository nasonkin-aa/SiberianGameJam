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
    public InventoryHUD inventory;
    
    [SerializeField]
    private ArmsController armsController;
    
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

        var inputRawAxis = Input.GetAxisRaw("Horizontal");
        if(inputRawAxis != 0)
        {
            if (Mathf.Abs(rb.velocity.x) < 10)
                rb.AddForce(new Vector2(Mathf.Sign(inputRawAxis), 0.5f) * playerSpeed * Time.deltaTime);
            
            if(inputRawAxis > 0 && !armsController.Flip || inputRawAxis < 0 && armsController.Flip)
            {
                anim.Play("Walk");
            }
            else
            {
                anim.Play("WalkBack");
            }
        }
        else
        {
            
            anim.Play("Idle");
        }

        if (isOnGround && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("JUMPING");
            rb.AddForce(Vector2.up * jumpForce * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 0;
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            transform.position = worldPosition;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            moduls.ForEach(module =>
            {
                if (!module.activeSelf)
                {
                    module.SetActive(true);
                    breaks.Add(module.GetComponentInChildren<ModuleBreak>());
                }
            });
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventory.GetStatus())
                inventory.CloseInventory();
            else
                inventory.OpenInventory();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            ReturnPart(PartName.Parts.LeftHand, Color.red);
        }
    }

    public void ReturnPart (PartName.Parts part, Color color)
    {
        GameObject correctPart = null;
        foreach (var el in moduls)
        {
            if (el.GetComponent<PartName>().name == part)
            {
                if (el.active)
                    return;
                correctPart = el;
            }
        }
        if (correctPart == null)
            return;

        correctPart.SetActive(true);
        var sprites = correctPart.GetComponentsInChildren<SpriteRenderer>();
        foreach (var sprite in sprites)
            sprite.color = color;
    }
}
