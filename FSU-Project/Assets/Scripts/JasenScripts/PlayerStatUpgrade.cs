using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatUpgrade : MonoBehaviour
{
    public PlayerClass playerClass;
    public List<Button> upgradeButtons;

    private List<Upgrade> availableUpgrades;

    void Start()
    {
        availableUpgrades = new List<Upgrade>
        {
            new Upgrade("Increase HP", UpgradeType.IncreaseHP),
            new Upgrade("Increase Speed", UpgradeType.IncreaseSpeed),
            new Upgrade("Increase Damage", UpgradeType.IncreaseDamage),
            new Upgrade("Increase Jump Speed", UpgradeType.IncreaseJumpSpeed),
            new Upgrade("Increase Max Ammo", UpgradeType.IncreaseMaxAmmo),
            
        };
    }

    public void GenerateRandomUpgrades()
    {
        List<Upgrade> randomUpgrades = new List<Upgrade>();

        while (randomUpgrades.Count < 3)
        {
            int index = UnityEngine.Random.Range(0, availableUpgrades.Count);
            Upgrade selectedUpgrade = availableUpgrades[index];

            if (!randomUpgrades.Contains(selectedUpgrade))
            {
                randomUpgrades.Add(selectedUpgrade);
            }
        }

        DisplayUpgrades(randomUpgrades);
    }

    void DisplayUpgrades(List<Upgrade> upgrades)
    {
        for (int i = 0; i < upgrades.Count; i++)
        {
            int index = i; 
            upgradeButtons[i].GetComponentInChildren<TMP_Text>().text = upgrades[i].upgradeName;
            upgradeButtons[i].onClick.RemoveAllListeners(); 
            upgradeButtons[i].onClick.AddListener(() => ApplyUpgrade(upgrades[index]));
        }
    }

    void ApplyUpgrade(Upgrade upgrade)
    {
        switch (upgrade.upgradeType)
        {
            case UpgradeType.IncreaseHP:
                playerClass.playerHP += 10;
                break;
            case UpgradeType.IncreaseSpeed:
                playerClass.speed += 1;
                break;
            case UpgradeType.IncreaseDamage:
                playerClass.damage += 5;
                break;
            case UpgradeType.IncreaseJumpSpeed:
                playerClass.jumpSpeed += 2;
                break;
            case UpgradeType.IncreaseMaxAmmo:
                playerClass.maxAmmo += 10;
                break;
                
        }

        Debug.Log("Applied Upgrade: " + upgrade.upgradeName);

       
        
    }
}



