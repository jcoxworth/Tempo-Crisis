using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSquare : MonoBehaviour
{
    public GameObject door;

    public bool isLevelStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        GameManager._access.onRestartLevel += ResetStartSquare;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLevelStarted)
            door.transform.localPosition = Vector3.Lerp(door.transform.localPosition, new Vector3(-1.95f, 1f, 1.125f), Time.deltaTime * 10f);
        else
            door.transform.localPosition = new Vector3(0f, 1f, 1.125f);

        if (isLevelStarted)
            return;
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("StartGame");
            if (!isLevelStarted)
            {
                isLevelStarted = true;
                GameManager._access.StartLevel();
            }
        }
    }
    private void ResetStartSquare()
    {
        isLevelStarted = false;
    }
}
