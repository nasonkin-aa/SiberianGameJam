using UnityEngine;

public class Balance : MonoBehaviour
{
    [SerializeField]
    private bool active = true;
    
    [SerializeField]
    private ArmsController armsController;
    
    public float targetRotation;
    public Rigidbody2D rb;
    public float force;
    
    public bool Active { get => active; set => active = value; }

    private float _targetRotation;
    
    private void Start()
    {
        _targetRotation = targetRotation;
        if (armsController)
        {
            armsController.Flipped += OnFlipped;
        }
    }

    private void OnFlipped(bool flip)
    {
        _targetRotation = flip ? -targetRotation : targetRotation;
    }

    public void FixedUpdate()
    {
        if (!active) return;
        rb.MoveRotation(Mathf.MoveTowardsAngle(rb.rotation, _targetRotation, force * Time.fixedDeltaTime));
    }

    private void OnJointBreak2D(Joint2D brokenJoint)
    {
        Debug.Log("Сломало!");
    }
}
