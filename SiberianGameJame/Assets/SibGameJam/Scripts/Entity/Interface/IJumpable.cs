public interface IJumpable
{
    bool CanJump { get; }
    void Jump();
 
    bool TryJump(int amount = 1)
    {
        if (CanJump)
            Jump();
        
        return CanJump;
    }
    
    void Handle();
}