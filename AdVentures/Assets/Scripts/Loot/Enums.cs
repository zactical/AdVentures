

public enum WeaponUpgradeTypeEnum
{
    None,
    Normal,
    Left,
    Right,
    Double,
    AngleLeft,
    AngleRight
}

public enum ProjectileUpgradeTypeEnum
{
    None,
    Standard,
    Medium,
    Large,
    Massive
}

public enum GenericUpgradeEnum
{
    None,
    ShotSpeed,
    Projectile
}

public interface IExpiringUpgrade
{
    float? ExpiresInSeconds { get; }
    void UpdateTime(float amount);
}