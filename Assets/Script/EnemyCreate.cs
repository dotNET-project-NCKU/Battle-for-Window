using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreate : MonoBehaviour
{
    public GameObject theEnemy;
    public float xPos;
    public float zPos;
    public int enemyCount;
    void Start()
    {
        StartCoroutine(EnemyDrop());

    }
    IEnumerator EnemyDrop()
    {
        while (enemyCount < 5)
        {
            //xPos = Random.Range(2, 80);
            //zPos = Random.Range(2, 80);
            Instantiate(theEnemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
            yield return new WaitForSeconds(3f);
            enemyCount += 1;

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
