using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScore : MonoBehaviour
{
    public static GameScore _access;

    public bool isHitStreak = false;
    public bool isKillStreak = false;

    public int currentHitStreak = 0;
    public int currentKillStreak = 0;

    public List<int> hitStreaks = new List<int>();
    public List<int> killStreaks = new List<int>();

    // Start is called before the first frame update
    void Awake()
    {
        _access = this;
    }
    private void Start()
    {
        GameManager._access.onStartLevel += ClearStreaks;
    }
    private void ClearStreaks()
    {
        hitStreaks.Clear();
    }
    public void IncrementHitStreak()
    {
        //called when an enemy gets hit by a bullet
        currentHitStreak += 1;
        if (currentHitStreak > 2)
            isHitStreak = true;
    }
    public void EndHitStreak()
    {
        //ended by missing an enemy
        if (isHitStreak)
            hitStreaks.Add(currentHitStreak);

        currentHitStreak = 0;
        isHitStreak = false;
    }

    public void IncrementKillStreak()
    {
        currentKillStreak += 1;
        if (currentKillStreak > 2)
            isKillStreak = true;
    }
    public void EndKillStreak()
    {
        if (isKillStreak)
            killStreaks.Add(currentKillStreak);
        currentKillStreak = 0;
        isKillStreak = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
