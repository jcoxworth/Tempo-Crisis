using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    
    [SerializeField]private Cover.Covertype covertype = Cover.Covertype.none;
    public Square upSquare, rightSquare, leftSquare, backSquare;

    public Cover cover;

    public LayerMask floor;
    // Start is called before the first frame update
    void Start()
    {
       // GameManager._access.onStartLevel += GetAllSquares;
    }
    /*
    private IEnumerator DelayGetSquares()
    {
        yield return new WaitForEndOfFrame();
        upSquare = GetSquareInDirection(Vector3.forward);
        rightSquare = GetSquareInDirection(Vector3.right);
        leftSquare = GetSquareInDirection(Vector3.left);
        backSquare = GetSquareInDirection(Vector3.back);

        cover = GetComponentInChildren<Cover>();
        if (cover)
        {
            covertype = cover.covertype;
        }
        else
            covertype = Cover.Covertype.none;
    }*/
    public void GetAllSquares()
    {
        // StartCoroutine(DelayGetSquares());
        upSquare = GetSquareInDirection(Vector3.forward);
        rightSquare = GetSquareInDirection(Vector3.right);
        leftSquare = GetSquareInDirection(Vector3.left);
        backSquare = GetSquareInDirection(Vector3.back);

        cover = GetComponentInChildren<Cover>();
        if (cover)
        {
           // Debug.Log("cover type " + cover.covertype);

            covertype = cover.covertype;
        }
        else
            covertype = Cover.Covertype.none;
    }
    private Square GetSquareInDirection(Vector3 direction)
    {
        RaycastHit hit;
        
        if (Physics.CapsuleCast(transform.position + (Vector3.up * 2f), transform.position + (Vector3.down * 2f), 0.5f, direction * 1f, out hit, 1f, floor))
        {
            //Debug.Log("got this square" + hit.transform.gameObject.name);
            return hit.collider.transform.GetComponentInParent<Square>();
        }
        return null;

    }
    public void SetCoverType(Cover.Covertype newCoverType)
    {
        covertype = newCoverType;
    }
    public Cover.Covertype GetCoverType()
    {
        return covertype;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            GetAllSquares();
           // Debug.Log("getting saq");
        }
    }
}
