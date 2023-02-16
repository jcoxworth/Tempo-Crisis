using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UILevelFinished : MonoBehaviour
{
    public TMP_Text finalTime;
    // Start is called before the first frame update
    void Start()
    {
        GameManager._access.onFinishLevel += DisplayFinalTime;
    }
    private void DisplayFinalTime()
    {
        finalTime.text = GameManager._access.currentLevelTime_Formatted;
    }
    // Update is called once per frame
    void Update()
    {
        DisplayFinalTime();
    }
}
