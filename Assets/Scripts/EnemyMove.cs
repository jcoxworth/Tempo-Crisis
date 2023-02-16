using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Enemy move moves and rotates the parentTransform
public class EnemyMove : MonoBehaviour
{
    public bool isActive = true;
    Enemy enemy;

    public float moveSpeed = 10f;
    public Vector3 targetPosition;
    private Quaternion targetRotation;
    public bool isMoving = false;

    private bool gotCurrentSquareFlag = false;
    private bool gotCoverFlag = false;

    public Square currentSquare;
    public Square currentCoverSquare;
    public Cover.Covertype currentSquareCovertype;
    public LayerMask floor;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        enemy.onEnemyDie += DisableMove;
        // targetPosition = enemy.transform.position;
       // print("fLoor mask layer value" +floor.value) ;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemy.isAlive || !isActive)
        {
            isMoving = false;
            return;
        }
        
        if (enemy.transform.position != targetPosition)
        {
            gotCurrentSquareFlag = false;
            gotCoverFlag = false;
            isMoving = true;
           // RotateToMovement();
        }
        else
        {
            if (!gotCurrentSquareFlag)
            {
                currentSquare = GetCurrentSquare();
                currentCoverSquare = GetCoverSquare();
            }
            isMoving = false;

        }

        transform.position = Vector3.MoveTowards(enemy.transform.position, targetPosition, moveSpeed * Time.deltaTime);

    }
    public void EnableMove()
    {
        isActive = true;
    }
    public void DisableMove()
    {
        isActive = false;
    }
    public void SetTargetPosition(Vector3 newTarget)
    {
        targetPosition = newTarget;
    }
   
    private Square GetCurrentSquare()
    {
        RaycastHit hit;
        if (Physics.Raycast((transform.position + (Vector3.up * 0.2f)), Vector3.down, out hit, 3f, floor))
        {
            gotCurrentSquareFlag = true;
       //     Debug.Log("enemy " + gameObject.name + " hit the " + hit.transform.gameObject);
            return hit.collider.transform.GetComponentInParent<Square>();
        }

        return null;

    }

    //the Cover square is always behind the current Square
    private Square GetCoverSquare()
    {
        RaycastHit hit;
        Square sq = null;
        if (Physics.Raycast((transform.position + (Vector3.up * 0.2f)), Vector3.down, out hit, 3f,floor))
        {
            currentSquare.GetAllSquares();

            sq = hit.collider.transform.GetComponentInParent<Square>().backSquare;
        }
        gotCoverFlag = true;

        return sq;

    }
}
