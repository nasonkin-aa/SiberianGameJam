using System.Collections.Generic;
using System.Linq;
using Tools;
using UnityEngine;

[System.Serializable]
public class ColliderDetector<T> : IDetector<T>
{
    [SerializeField] private Collider2D collider;
    [SerializeField] private ContactFilter2D contactFilter;
    
    private readonly List<Collider2D> _colliders;

    public ColliderDetector()
    {
        _colliders = new List<Collider2D>();
    }

    public ColliderDetector(Collider2D collider, ContactFilter2D contactFilter) : this()
    {
        this.collider = collider;
        this.contactFilter = contactFilter;
    }
    
    public Option<T> Detected { get; set; }
    public Option<IEnumerable<T>> DetectedEnumerable { get; set; }

    public void Handle()
    {
        Physics2D.OverlapCollider(collider, contactFilter, _colliders);
        var components = _colliders.Select(x => x.GetComponent<T>()).Where(x => x != null);
        DetectedEnumerable = Option<IEnumerable<T>>.Some(components);
        Detected = DetectedEnumerable.Map(x => x.FirstOrDefault());
    }
}