using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    public bool isCrouching = true;
    private SquareMove squareMove;
    // Start is called before the first frame update
    void Start()
    {
        squareMove = GetComponent<SquareMove>();
        isCrouching = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager._access.isDead)
        {
            return;
        }

        if (GameManager._access.isPaused || !squareMove )
            return;

        isCrouching = !Input.GetKey(KeyCode.Space) && !squareMove.isMovingSquare;
        
    }
}
