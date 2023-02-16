using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    public bool isHiding = true;
    private SquareMove squareMove;
    public Cover.Covertype currentCoverType; 
    // Start is called before the first frame update
    void Start()
    {
        squareMove = GetComponent<SquareMove>();
        isHiding = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager._access.isDead)
        {
            return;
        }

        if (GameManager._access.isPaused || !squareMove)
            return;


        if (!squareMove)
            return;

        isHiding = 
            !Input.GetKey(KeyCode.Space) && 
            !squareMove.isMovingSquare && 
            squareMove.currentSquare.GetCoverType() != Cover.Covertype.none;

        if (squareMove.currentSquare)
        {
            currentCoverType = squareMove.currentSquare.GetCoverType();
        }
    }
}
