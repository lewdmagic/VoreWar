public sealed class EmptyParameters : IParameters
{
}

public interface IOverSizeParameters : IParameters
{
    bool Oversize { get; }
}

public class OverSizeParameters : IOverSizeParameters
{
    public bool Oversize { get; set; } = false;
}