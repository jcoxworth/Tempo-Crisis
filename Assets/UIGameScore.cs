using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIGameScore : MonoBehaviour
{
    public TMP_Text hitStreakTXT, killStreakTXT;
    public GameObject hitStreakObject, killStreakObject;

    GameScore gameScore;

    // Start is called before the first frame update
    void Start()
    {
        gameScore = GetComponent<GameScore>();    
    }

    // Update is called once per frame
    void Update()
    {
        hitStreakObject.SetActive(gameScore.isHitStreak);
        if (gameScore.isHitStreak)
            hitStreakTXT.text = gameScore.currentHitStreak.ToString();
        killStreakObject.SetActive(gameScore.isKillStreak);
        if (gameScore.isKillStreak)
            killStreakTXT.text = gameScore.currentKillStreak.ToString();
    }
}
