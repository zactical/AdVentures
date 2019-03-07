public abstract class UpgradeBase
{
    public float? ExpiresInSeconds { get; set; }

    public bool IsExpired()
    {
        if (ExpiresInSeconds.HasValue == false)
            return false;

        return ExpiresInSeconds.Value > 0;
    }

    public void UpdateTime(float amount)
    {
        if (ExpiresInSeconds.HasValue == false)
            return;

        ExpiresInSeconds -= amount;
    }
}
