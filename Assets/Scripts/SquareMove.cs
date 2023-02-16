using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareMove : MonoBehaviour
{
    public bool isMovingSquare = false;
    public Square currentSquare;
    public Square nextSquare;

    public Cover.Covertype currentSquareCovertype;

    public LayerMask floor;
    // Start is called before the first frame update
    void Start()
    {
        GameManager._access.onRestartLevel += ResetMovement;
        GameManager._access.onStartLevel += ResetMovement;
        GameManager._access.onNextLevel += ResetMovement;
        GameManager._access.onFinishLevel += ResetMovement;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager._access.isDead || GameManager._access.isPaused)
            return;

        if (isMovingSquare)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextSquare.transform.position, Time.deltaTime * 5f);
        }
        else
        {
            currentSquare = GetCurrentSquare();
            currentSquareCovertype = currentSquare.GetCoverType();
            ListenForInputs();
        }
    }
    private void ResetMovement()
    {
        isMovingSquare = false;
        currentSquare = GetCurrentSquare();
        currentSquareCovertype = currentSquare.GetCoverType();
    }
    private void ListenForInputs()
    {
        if (
            Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)||
            Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)||
            Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)||
            Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)
            )
            currentSquare = GetCurrentSquare();
        currentSquare.GetAllSquares();

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            MoveUp();
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            MoveRight();
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            MoveLeft();
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            MoveBack();
    }
    private void MoveUp()
    {
        if (currentSquare.upSquare && currentSquare.GetCoverType() == Cover.Covertype.none)
        {
            nextSquare = currentSquare.upSquare;
            StartCoroutine(MoveToNewSquare());
        }
    }
    private void MoveRight()
    {
        if (currentSquare.rightSquare)
        {
            nextSquare = currentSquare.rightSquare;
            StartCoroutine(MoveToNewSquare());
        }
    }
    private void MoveLeft()
    {
        if (currentSquare.leftSquare)
        {
            nextSquare = currentSquare.leftSquare;
            StartCoroutine(MoveToNewSquare());
        }
    }
    private void MoveBack()
    {
        if (currentSquare.backSquare)
        {
            nextSquare = currentSquare.backSquare;
            StartCoroutine(MoveToNewSquare());
        }
    }
    private bool MovingFinished()
    {
        bool b = true;
        if (currentSquare && nextSquare)
            b = transform.position == nextSquare.transform.position;
        return b;
    }
    private IEnumerator MoveToNewSquare()
    {
        isMovingSquare = true;
        yield return new WaitUntil(MovingFinished);
        isMovingSquare = false;
    }
    private Square GetCurrentSquare()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(
            (transform.position + (Vector3.up * 0.2f) ), 
            Vector3.down, out hit, 3f, floor) )
        {
            return hit.collider.transform.GetComponentInParent<Square>();
        }
        return null;

    }
}
