using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSquare : MonoBehaviour
{
    public bool isDoorOpen = false;
    public bool isLevelFinished = false;
    public GameObject door;
    
    public Player player;
    

    float distanceToPlayer;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        GameManager._access.onRestartLevel += ResetEndSquare;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player || isLevelFinished)
            return;


        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        isDoorOpen = distanceToPlayer < 5f && distanceToPlayer > 1f;

        

        if (isDoorOpen)
            door.transform.localPosition = Vector3.Lerp(door.transform.localPosition, new Vector3(2f, 1f, -1.125f), Time.deltaTime * 10f);
        else
            door.transform.localPosition = Vector3.Lerp(door.transform.localPosition, new Vector3(0f, 1f, -1.125f), Time.deltaTime * 10f);


        if (distanceToPlayer < 0.25f)
        {
            if (!isLevelFinished)
            {
                GameManager._access.FinishLevel();
                isLevelFinished = true;
            }
        }
    }
    private void ResetEndSquare()
    {
        isLevelFinished = false;
        isDoorOpen = false;
    }
}
