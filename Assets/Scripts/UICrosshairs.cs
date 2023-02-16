using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICrosshairs : MonoBehaviour
{
    public GameObject crosshairs;
    public PlayerHide hide;
    public SquareMove move;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        ShowHideCrosshairs();
    }

    private void ShowHideCrosshairs()
    {
        crosshairs.SetActive(!GameManager._access.isPaused && !GameManager._access.isDead && !hide.isHiding && !move.isMovingSquare);
    }
}
