using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareEvent : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent onPlayerEnterSquare;
    public bool hasTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if (GameManager._access.player.transform.position == transform.position)
        {
            if (!hasTriggered)
            {
                hasTriggered = true;
                onPlayerEnterSquare.Invoke();
            }
        }
        else
        {
            hasTriggered = false;
        }
            


    }
    public void TriggerTest()
    {
        Debug.Log("trigger test" + Time.time);
    }
}
