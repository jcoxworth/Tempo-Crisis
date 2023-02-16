using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    public Square square;
    public bool destructable = true;
    public int originalCoverHealth = 100;
    public int coverHealth = 100;
    private bool isReactingToDamage = false;
    private Vector3 originalPosition;

    public MeshFilter meshFilter;
    private Mesh activeCover;
    public Mesh destroyedCover;
    public Collider coverCollider;

    public delegate void OnCoverDestroy();
    public OnCoverDestroy onCoverDestroy;

    public delegate void OnCoverDamage();
    public OnCoverDamage onCoverDamage;

    public delegate void OnCoverReset();
    public OnCoverReset onCoverReset;


    public enum Covertype { none, crouch, standR, standL }
    public Covertype covertype = Covertype.crouch;

    // Start is called before the first frame update
    void Start()
    {
        square = GetComponentInParent<Square>();
        meshFilter = GetComponent<MeshFilter>();
        coverCollider = GetComponent<Collider>();
        activeCover = meshFilter.mesh;
        originalPosition = transform.localPosition;
        coverHealth = originalCoverHealth;
        GameManager._access.onRestartLevel += ResetCover;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (!destructable)
            return;
        DamageShake();
    }
    private void DamageShake()
    {

        if (isReactingToDamage)
            transform.localPosition = originalPosition + Random.onUnitSphere * 0.01f;
        else
            transform.localPosition = originalPosition;
    }
    public void AddDamage(int amount)
    {
        if (!destructable)
            return;

        onCoverDamage?.Invoke();
        coverHealth -= amount;
        StartCoroutine(ReactToDamage());
        
    }
    private IEnumerator ReactToDamage()
    {
        isReactingToDamage = true;
        yield return new WaitForSeconds(0.2f);

        isReactingToDamage = false;
        if (coverHealth < 1)
            CoverDestroy();
    }
    private void CoverDestroy()
    {
        onCoverDestroy?.Invoke();
        square.SetCoverType( Covertype.none);
        coverCollider.enabled = false;
        meshFilter.mesh = destroyedCover;
    }
    public void ResetCover()
    {
        onCoverReset?.Invoke();
        square.SetCoverType(covertype);
        coverCollider.enabled = true;
        coverHealth = originalCoverHealth;
        meshFilter.mesh = activeCover;
    }
}
