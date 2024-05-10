public interface IJumpable
{
    public float JumpHeight { get; }
    
    bool CanJump { get; }
    
    void Jump();

    void TryJump()
    {
        if (CanJump)
            Jump();
    }
}