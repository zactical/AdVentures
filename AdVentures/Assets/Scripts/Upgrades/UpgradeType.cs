public class UpgradeType : UpgradeBase
{
    public GenericUpgradeEnum Upgrade;

    public UpgradeType(GenericUpgradeEnum upgrade, float? expiresInSeconds)
    {
        Upgrade = upgrade;
        ExpiresInSeconds = expiresInSeconds;
    }
}