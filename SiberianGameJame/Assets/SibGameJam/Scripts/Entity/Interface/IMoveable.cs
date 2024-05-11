using UnityEngine;

public interface IMoveable
{
    Vector3 Position { get; }
    Vector2 Velocity { get; }
        
    void MoveTo(Vector3 position);
    void MoveBy(Vector3 delta);
    void Move();
    
    void Stop() => MoveBy(Vector3.zero);

    bool Reached(Vector3 position);
}