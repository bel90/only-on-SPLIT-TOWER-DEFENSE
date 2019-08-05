using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour {

    public GameObject enemyPrefab;
    public Transform startPosition;

    public GameObject GetEnemy() {
        return GetLatestEnemy();
        /*
        if (transform.childCount > 0) {
            int ranIndex = Random.Range(0, transform.childCount);
            return transform.GetChild(ranIndex).gameObject;
        }
        return null;
        */
    }

    public GameObject GetLatestEnemy() {
        GameObject latestEnemy;
        if (transform.childCount > 0) {
            latestEnemy = transform.GetChild(0).gameObject;
            int currentWayPoint = latestEnemy.GetComponent<Enemy>().wayPointIndex;

            for (int i = 1; i < transform.childCount; i++) {
                int otherWayPoint = transform.GetChild(i).gameObject.GetComponent<Enemy>().wayPointIndex;
                if (currentWayPoint < otherWayPoint) {
                    latestEnemy = transform.GetChild(i).gameObject;
                    currentWayPoint = otherWayPoint;
                }
            }
            GameManager.instance.SetChoosenEnemy(latestEnemy);
            return latestEnemy;
        }

        return null;
    }

    public void CreateNewEnemies() {
        StartCoroutine(CreateEnemyCo());
    }

    IEnumerator CreateEnemyCo() {
        yield return new WaitForSeconds(.15f);

        GameObject enemy1 = Instantiate(enemyPrefab, startPosition.position, Quaternion.identity);
        Enemy enemyScript = enemy1.GetComponent<Enemy>();
        GameManager.instance.IncreaseEnemies();
    }
    
}
