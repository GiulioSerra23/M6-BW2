using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : GenericSingleton<LeaderboardManager>
{
    [SerializeField] private int _maxRunTimes = 3;

    private List<float> _bestTimes;

    protected override void Awake()
    {
        base.Awake();
        _bestTimes = new List<float>(_maxRunTimes);
    }

    public void RegisterRun(float time)
    {
        _bestTimes.Add(time);
        _bestTimes.Sort();

        if (_bestTimes.Count > _maxRunTimes) _bestTimes.RemoveAt(_bestTimes.Count - 1);
    }

    public void SetTimes(List<float> times)
    {
        _bestTimes = new List<float>(times);
    }

    public List<float> GetAllBestTimes()
    {
        return new List<float>(_bestTimes);
    }
}
