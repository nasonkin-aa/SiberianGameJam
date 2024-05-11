using System.Collections.Generic;
using System.Linq;
using Tools;
using UnityEngine;

public class ColliderDetector<T> : IDetector<T>
{
    private readonly Collider2D _collider;
    private readonly ContactFilter2D _contactFilter;
    private readonly List<Collider2D> _colliders;

    public ColliderDetector(Collider2D collider, ContactFilter2D contactFilter)
    {
        _collider = collider;
        _contactFilter = contactFilter;
        _colliders = new List<Collider2D>();
    }
    
    public Option<T> Detected { get; set; }
    public Option<IEnumerable<T>> DetectedEnumerable { get; set; }

    public void Handle()
    {
        Physics2D.OverlapCollider(_collider, _contactFilter, _colliders);
        var components = _colliders.Select(x => x.GetComponent<T>()).Where(x => x != null);
        DetectedEnumerable = Option<IEnumerable<T>>.Some(components);
        Detected = DetectedEnumerable.Map(x => x.FirstOrDefault());
    }
}