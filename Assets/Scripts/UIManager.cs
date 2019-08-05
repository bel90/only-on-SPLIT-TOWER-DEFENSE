using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    public Text coinText;
    public Text enemyText;

    public int speedCost;
    public int damageCost;
    public int speedLevel;
    public int damageLevel;

    public Text speedLevelText;
    public Text damageLevelText;

    public Text speedCostText;
    public Text damageCostText;

    public Text towerSpeed;
    public Text towerDamage;
    
    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Start() {
        speedCost = 30;
        damageCost = 30;
        speedLevel = 0;
        damageLevel = 0;
    }

    public void SetCoinText(int coins) {
        coinText.text = coins.ToString();
    }

    public void SetEnemyText(int enemies) {
        enemyText.text = enemies.ToString();
    }

    public void BuySpeed() {
        if (speedCost <= GameManager.instance.GetCoins()) {
            GameManager.instance.AddCoins(-speedCost);
            IncreaseSpeedCosts();
            speedLevel++;
            GameManager.instance.tower.IncreaseSpeed();
            speedLevelText.text = "Level " + speedLevel;
            speedCostText.text = speedCost + " Coins";
        }
    }

    private void IncreaseSpeedCosts() {
        speedCost += (int)(speedCost * 0.3f);
    }

    public void BuyDamage() {
        if (damageCost <= GameManager.instance.GetCoins()) {
            GameManager.instance.AddCoins(-damageCost);
            IncreaseDamageCosts();
            damageLevel++;
            GameManager.instance.tower.IncreaseDamage();
            damageLevelText.text = "Level " + damageLevel;
            damageCostText.text = damageCost + " Coins";
        }
    }

    private void IncreaseDamageCosts() {
        damageCost += (int)(damageCost * 0.3f);
    }

    public void SetTowerSpeedText(int perMinute) {
        towerSpeed.text = "Speed: " + perMinute + " shoots per minute";
    }

    public void SetTowerDamageText(float damage) {
        towerDamage.text = "Damage: " + damage;
    }
}
