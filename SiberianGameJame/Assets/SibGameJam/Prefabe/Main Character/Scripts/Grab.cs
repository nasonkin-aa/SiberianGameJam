using System;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public KeyCode mouseButton;
    public KeyCode dropItemButton;

    [SerializeField]
    private LayerMask grabMask;

    [SerializeField]
    private float grabRadius = 0.1f;

    private bool _hold;
    private readonly Collider2D[] _hits = new Collider2D[1];
    private Collider2D _target;

    public bool Grabbed => _target != null;

    private float _holdDelay = 0.2f;
    private float _holdTimer;

    [SerializeField]
    private Rigidbody2D lowerArmBody;

    private void Update()
    {
        if (Input.GetKeyDown(mouseButton))
        {
            _holdTimer = 0;
        }
        else if (Input.GetKey(mouseButton))
        {
            _holdTimer += Time.deltaTime;
            if (_holdTimer > _holdDelay)
            {
                _hold = true;
            }
        }
        else
        {
            _hold = false;
            _target = null;
            Destroy(GetComponent<FixedJoint2D>());
            
            if (lowerArmBody)
                lowerArmBody.mass = 1;
        }
    }

    private void FixedUpdate()
    {
        if (_hold)
        {
            var count = Physics2D.OverlapCircleNonAlloc(transform.position, grabRadius, _hits, grabMask);
            if (count > 0 && _target != _hits[0])
            {
                _target = _hits[0];
                var rb = _target.GetComponentInParent<Rigidbody2D>();
                if (rb != null)
                {
                    var fj = transform.gameObject.AddComponent<FixedJoint2D>();
                    fj.connectedBody = rb;
                }
                else
                {
                    if (lowerArmBody)
                        lowerArmBody.mass = 10f;
                    var fj = transform.gameObject.AddComponent<FixedJoint2D>();
                }
            }
        }
    }
    
    private void HandleGrab()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, grabRadius);
    }
}