namespace EnemyAI
{
    public static class Policies 
    {
        public static readonly IPolicy RunForever = new RunForeverPolicy();
        public static readonly IPolicy RunUntilSuccess = new RunUntilSuccessPolicy();
        public static readonly IPolicy RunUntilFailure = new RunUntilFailurePolicy();
        
        private class RunForeverPolicy : IPolicy 
        {
            public bool ShouldReturn(Node.Status status) => false;
        }
        
        private class RunUntilSuccessPolicy : IPolicy 
        {
            public bool ShouldReturn(Node.Status status) => status == Node.Status.Success;
        }

        private class RunUntilFailurePolicy : IPolicy 
        {
            public bool ShouldReturn(Node.Status status) => status == Node.Status.Failure;
        }
    }
}