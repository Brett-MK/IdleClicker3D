using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using Manager;
using UnityEngine;

public static class SaveSystem
{
    public const string FILENAME_SAVEDATA = "/savedata.json";
    public static void SaveGamestate()
    {
        string filePathSaveData = Application.persistentDataPath + FILENAME_SAVEDATA;
        Debug.Log(filePathSaveData);
        PlayerSaveData playerSaveData = new PlayerSaveData(PlayerManager.Instance);
        UpgradeSaveData upgradeSaveData = new UpgradeSaveData(UpgradeManager.Instance);
        IdleSaveData idleSaveData = new IdleSaveData(IdleManager.Instance);

        SaveData saveData = new SaveData(playerSaveData, upgradeSaveData, idleSaveData);
        string txt = JsonUtility.ToJson(saveData);
        File.WriteAllText(filePathSaveData, txt);
    }

}

public class SaveData
{
    [SerializeField]
    public PlayerSaveData playerSaveData;

    [SerializeField]
    public UpgradeSaveData upgradeSaveData;

    [SerializeField]
    public IdleSaveData idleSaveData;

    [SerializeField]
    public long currentTimeStamp;

    public SaveData(PlayerSaveData playerSaveData, UpgradeSaveData upgradeSaveData, IdleSaveData idleSaveData)
    {
        this.playerSaveData = playerSaveData;
        this.upgradeSaveData = upgradeSaveData;
        this.idleSaveData = idleSaveData;
        this.currentTimeStamp = DateTime.Now.Ticks;
    }
}

[Serializable]
public class PlayerSaveData
{
    [SerializeField]
    public double currentCash;

    [SerializeField]
    public double currentPremiumCurrency;

    public PlayerSaveData(PlayerManager playerManager)
    {
        currentCash = playerManager.CurrentCash;
        currentPremiumCurrency = playerManager.CurrentPremiumCurrency;
    }
}

[Serializable]
public class UpgradeSaveData
{
    [SerializeField]
    private List<UpgradeLevel> upgradeLevels = new List<UpgradeLevel>();

    public UpgradeSaveData(UpgradeManager upgradeManager)
    {
        foreach (KeyValuePair<ShopItemSO, int> entry in upgradeManager.UpgradeLevels)
        {
            upgradeLevels.Add(new UpgradeLevel(entry.Value, entry.Key.id));
        }
    }
}

[Serializable]
public class UpgradeLevel
{
    [SerializeField]
    public int level;

    [SerializeField]
    public int id;

    public UpgradeLevel(int level, int id)
    {
        this.level = level;
        this.id = id;
    }
}

[Serializable]
public class IdleSaveData
{
    [SerializeField]
    public double cashPerSecond;

    public IdleSaveData(IdleManager idleManager)
    {
        cashPerSecond = idleManager.CashPerSecond;
    }
}