internal interface IOverSizeParameters : IParameters
{
    bool Oversize { get; }
}

internal class OverSizeParameters : IOverSizeParameters
{
    public bool Oversize { get; set; } = false;
}