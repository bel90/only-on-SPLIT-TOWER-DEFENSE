using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public GameObject choosenEnemy;

    private int coins;
    private int enemyCount;

    public int currentEnemyHealth = 20;
    public int increaseHealthAt;

    public Tower tower;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    void Start() {
        coins = 20;
        enemyCount = 1;
        currentEnemyHealth = 20;
        increaseHealthAt = 10;
        UIManager.instance.SetCoinText(coins);
        UIManager.instance.SetEnemyText(enemyCount);
    }

    public void SetChoosenEnemy(GameObject enemy) {
        if (choosenEnemy != null) {
            choosenEnemy.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
        }
        
        choosenEnemy = enemy;
        if (choosenEnemy != null) {
            choosenEnemy.GetComponent<SpriteRenderer>().color = new Color(.5f, .5f, .5f);
        }
    }

    public void IncreaseEnemies() {
        enemyCount++;
        UIManager.instance.SetEnemyText(enemyCount);
        //Debug.Log(enemyCount + " " + increaseHealthAt);
        if (enemyCount >= increaseHealthAt) {
            increaseHealthAt += 10;
            //currentEnemyHealth = (int)(currentEnemyHealth * 1.2f);
            currentEnemyHealth = (int)(currentEnemyHealth * 1.06f);
            //Debug.Log(currentEnemyHealth);
        }
    }

    public void AddCoins(int coins) {
        this.coins += coins;
        UIManager.instance.SetCoinText(this.coins);
    }

    public int GetCoins() {
        return coins;
    }

    public int GetEnemyCount() {
        return enemyCount;
    }
}
