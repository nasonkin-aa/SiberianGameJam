public interface IJumpable
{
    public float JumpHeight { get; }
    
    bool CanJump { get; }
    
    void Jump();

    bool TryJump()
    {
        if (CanJump) Jump();
        return CanJump;
    }

    void HandleLogic();
}