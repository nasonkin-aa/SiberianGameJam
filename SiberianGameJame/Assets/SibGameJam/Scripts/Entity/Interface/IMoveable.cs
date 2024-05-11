using UnityEngine;

public interface IMoveable
{
    Vector3 Position { get; }
    Vector2 Direction { get; }
    Vector2 Velocity { get; }

    void MoveTo(Vector3 position) => MoveBy((position - Position).normalized);
    void MoveBy(Vector3 delta);
    
    void Handle();
    void LookAt(Vector3 position);
    
    void Stop() => MoveBy(Vector3.zero);

    bool Reached(Vector3 position);
}