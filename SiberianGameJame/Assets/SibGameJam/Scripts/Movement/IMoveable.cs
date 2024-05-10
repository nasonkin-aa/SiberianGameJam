using UnityEngine;

public interface IMoveable
{
    Vector3 Position { get; }
        
    void MoveTo(Vector3 position);
    void MoveBy(Vector3 delta);
    void Move();

    bool Reached(Vector3 position);
}