public enum UpgradeType
{
    IncreaseHP,
    IncreaseSpeed,
    IncreaseDamage,
    IncreaseJumpSpeed,
    IncreaseMaxAmmo,

}

public class Upgrade
{
    public string upgradeName;
    public UpgradeType upgradeType;

    public Upgrade(string name, UpgradeType type)
    {
        upgradeName = name;
        upgradeType = type;
    }
}