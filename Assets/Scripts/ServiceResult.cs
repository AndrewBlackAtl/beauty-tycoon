public interface IServiceResult 
{
    public float MoneyChange { get; }
    public int ReputationChange { get; }
}


public class SuccessfulResult : IServiceResult
{
    public float MoneyChange => 9.65f;
    public int ReputationChange => 1;
}

public class FailureResult : IServiceResult
{
    public float MoneyChange => 0f;
    public int ReputationChange => 0;
}