using UnityEngine;

public class Gun : Item
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Collider2D collider;

    private Vector3 _initialLocalScale;

    private void Start()
    {
        _initialLocalScale = transform.localScale;
    }


    void Update()
    {
        if (Attached && Input.GetKeyDown(KeyCode.LeftShift))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    public override void AttachToHand(Transform hand)
    {
        base.AttachToHand(hand);

        GetComponent<Rigidbody2D>().simulated = false;
        collider.enabled = false;
        transform.SetParent(hand, false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = new Vector3(_initialLocalScale.x / Mathf.Abs(hand.lossyScale.x),
            _initialLocalScale.y / Mathf.Abs(hand.lossyScale.y), _initialLocalScale.z / Mathf.Abs(hand.lossyScale.z));

        Debug.Log(hand.lossyScale);
    }

    public override void DetachFromHand()
    {
        base.DetachFromHand();
        GetComponent<Rigidbody2D>().simulated = true;
        collider.enabled = true;
        transform.SetParent(null, true);
        transform.localScale = new Vector3(Mathf.Sign(transform.localScale.x) * _initialLocalScale.x, Mathf.Sign(transform.localScale.y) * _initialLocalScale.y, Mathf.Sign(transform.localScale.z) * _initialLocalScale.z);
    }
}
