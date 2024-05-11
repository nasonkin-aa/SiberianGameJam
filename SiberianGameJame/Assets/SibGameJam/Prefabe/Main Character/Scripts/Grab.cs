using System;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public KeyCode mouseButton;
    public KeyCode dropItemButton;

    [SerializeField]
    private LayerMask grabMask;
    [SerializeField]
    private LayerMask pickUpMask;

    [SerializeField]
    private float grabRadius = 0.1f;

    [SerializeField]
    private Transform itemPivot;

    private bool _hold;
    private readonly Collider2D[] _hits = new Collider2D[1];
    private Collider2D _target;

    public bool Grabbed => _target != null;
    public bool Hold => _isHoldItem;

    private float _holdDelay = 0.2f;
    private float _holdTimer;

    [SerializeField]
    private Rigidbody2D lowerArmBody;

    [SerializeField] CharacterSlot characterSlotR;
    [SerializeField] CharacterSlot characterSlotL;

    private void Update()
    {
        if (_isHoldItem)
        {
            if (Input.GetKeyDown(dropItemButton))
            {
                _isHoldItem = false;
                if (_attachedItem)
                {
                    _attachedItem.DetachFromHand();
                    ItemDetached?.Invoke(_attachedItem);
                }
                Detach();
            }

            return;
        }

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
            Detach();
        }
    }

    private void Detach()
    {
        _holdTimer = 0;
        _hold = false;
        _target = null;
        Destroy(GetComponent<FixedJoint2D>());

        if (lowerArmBody)
            lowerArmBody.mass = 1;
    }

    private void FixedUpdate()
    {
        if (_hold)
        {
            var count = Physics2D.OverlapCircleNonAlloc(transform.position, grabRadius, _hits, grabMask);
            Collider2D[] pickUpItems = new Collider2D[1];

            var itemsCount = Physics2D.OverlapCircleNonAlloc(transform.position, grabRadius, pickUpItems, pickUpMask);
            if (itemsCount > 0 && pickUpItems[0] != null)
                CollisionEnt(pickUpItems[0]);

            if (count > 0 && _target != _hits[0])
            {
                _target = _hits[0];
                var rb = _target.GetComponentInParent<Rigidbody2D>();
                if (rb != null)
                {
                    var fj = transform.gameObject.AddComponent<FixedJoint2D>();
                    fj.connectedBody = rb;

                    if (rb.CompareTag("Item"))
                    {
                        _isHoldItem = true;
                        var item = rb.GetComponent<Item>();
                        if (item)
                        {
                            item.AttachToHand(itemPivot);
                            _attachedItem = item;
                            ItemAttached?.Invoke(_attachedItem);
                        }
                    }
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, grabRadius);
    }

    public void CollisionEnt(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            var icon = collision.transform.GetComponent<PickableItem>().prefabForInventory.GetComponent<ItemIcon>();
            if (characterSlotR != null)
            {
                characterSlotR.EquipItem(icon);
                Destroy(collision.gameObject);
            } else if (characterSlotL != null)
            {
                characterSlotL.EquipItem(icon);
                Destroy(collision.gameObject);
            }
        }
    }
}