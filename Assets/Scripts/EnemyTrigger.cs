using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TriggerElement
{
    public Vector3 start;
    public Vector3 end;
    public GameObject enemy;

}
public class EnemyTrigger : MonoBehaviour
{
    public Color triggerColor = Color.green;
    public float triggerLine = 1.5f;
    public TriggerElement[] elements;
    public List<GameObject> createdEnemies = new List<GameObject>();
    //  public EnemyMove enemyMove;
    public bool isTriggered = false;
    // Start is called before the first frame update
    public UnityEvent onTrigger;
    void Start()
    {
        GameManager._access.onRestartLevel += ResetTrigger;

        //enemyMove = enemy.transform.GetComponent<EnemyMove>();

        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].start = transform.TransformPoint(elements[i].start);
            elements[i].end = transform.TransformPoint(elements[i].end);

        }

        isTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager._access.isDead || GameManager._access.isPaused)
            return;
        if (isTriggered)
        {
          //  enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, endPos.position, moveSpeed * Time.deltaTime);
        }

        TestTrigger();

        
    }
    private void TestTrigger()
    {

        if (GameManager._access.player.transform.position.z > transform.TransformPoint(Vector3.forward * triggerLine).z)
        {
            if (!isTriggered)
            {
                for (int i = 0; i < elements.Length; i++)
                {
                    GameObject e = Instantiate(elements[i].enemy);
                    e.transform.position = elements[i].start;
                    e.transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
                    createdEnemies.Add(e);


                    e.transform.GetComponent<EnemyMove>().SetTargetPosition(elements[i].end);


                }
                onTrigger.Invoke();
               
                isTriggered = true;
            }
        }
       

    }
    private void ResetTrigger()
    {
        for (int i = 0; i < createdEnemies.Count; i++)
            Destroy(createdEnemies[i]);
        createdEnemies.Clear();
        isTriggered = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = triggerColor;
        Vector3 tl = transform.TransformPoint(Vector3.forward * triggerLine);

        Gizmos.DrawLine(tl + Vector3.right * -20f, tl + Vector3.right * 20f);


        Vector3 manSize = new Vector3(1f, 2f, 1f);

        
        for (int i = 0; i < elements.Length; i++)
        {


            Vector3 s = transform.TransformPoint(elements[i].start);
            Vector3 e = transform.TransformPoint(elements[i].end);



            Gizmos.DrawWireCube( s + Vector3.up, manSize);
            Gizmos.DrawCube(e + Vector3.up, manSize);


            Gizmos.DrawLine(e + Vector3.up, s + Vector3.up);

        }
        
    }
}
