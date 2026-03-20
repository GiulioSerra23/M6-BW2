using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public List<float> BestTimes;
    public int TotalCoins;
    public Dictionary<ObjectID, int> PowerUps;
}
