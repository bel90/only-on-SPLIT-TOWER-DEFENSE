using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    
    public float health = 0;

    public int coins = 40;
    
    public GameObject deathEffect;
    public AudioClip deathSound;

    //movement stuff
    public float speed = .5f;

    public int wayPointIndex = 0;
    public Transform nextGoal;

    public Transform enemies;
    public Enemies enemiesScript;
    public GameObject enemiesPrefab;

    void Start() {
        if (health == 0) health = GameManager.instance.currentEnemyHealth > 0 ? GameManager.instance.currentEnemyHealth : 20;
        else health = GameManager.instance.currentEnemyHealth;

        if (wayPointIndex == 0) {
            nextGoal = WayPoints.wayPoints[wayPointIndex];
        }
        
        enemies = FindObjectOfType<Enemies>().gameObject.transform;
        enemiesScript = enemies.GetComponent<Enemies>();
        gameObject.name = "Enemy" + enemies.transform.childCount;
        
        transform.SetParent(enemies);
    }
	
    void Update() {
        Vector3 dir = nextGoal.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, nextGoal.position) <= 0.1f) {
            GetNextWaypoint();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Bullet") {
            //Is this the choosen Enemy?
            if (GameManager.instance.choosenEnemy != null &&
                gameObject.name != GameManager.instance.choosenEnemy.name) {
                //If this is not the choosen one, do nothing
                return;
            }

            HitEnemy(collision);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Bullet") {
            if (GameManager.instance.choosenEnemy != null &&
                gameObject.name != other.gameObject.GetComponent<Bullet>().enemy.gameObject.name) {
                //If this is not the choosen one, do nothing
                return;
            }

            HitEnemy(other);
        }
    }

    private void HitEnemy(Collider2D collision) {
        Bullet bul = collision.gameObject.GetComponent<Bullet>();

        Vector3 dir = -1 * (collision.gameObject.transform.position - transform.position);
        transform.Translate(dir.normalized * speed * 2 * Time.deltaTime, Space.World);

        //decrease live, and if life < 0 split enemy
        health -= bul.damage;
        bul.DestroyBullet();
        if (health < 0) {
            SplitEnemy();
        }
    }

    private void OnMouseUp() {
        GameManager.instance.SetChoosenEnemy(gameObject);
    }

    public void SetNextGoal(int index) {
        wayPointIndex = index;
        nextGoal = WayPoints.wayPoints[wayPointIndex];
    }

    void SplitEnemy() {
        Vector3 tmpVec = transform.position;
        Instantiate(deathEffect, tmpVec, Quaternion.identity);
        AudioSource.PlayClipAtPoint(deathSound, tmpVec);

        enemiesScript.CreateNewEnemies();
        //Give coins to the Player
        GameManager.instance.AddCoins(coins);

        health = GameManager.instance.currentEnemyHealth; //reset the health
        transform.position = enemiesScript.startPosition.position;//reset the position
        
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
        if (GameManager.instance.choosenEnemy != null &&
            GameManager.instance.choosenEnemy.name == gameObject.name) {
            GameManager.instance.SetChoosenEnemy(null);
        }
        
        SetNextGoal(0);
    }

    void GetNextWaypoint() {
        if (wayPointIndex >= WayPoints.wayPoints.Length - 1) {
            //finish the game
            PanelManager.instance.ShowGameOverPanel();
            Destroy(gameObject);
            return;
        }

        wayPointIndex++;
        nextGoal = WayPoints.wayPoints[wayPointIndex];
    }
    
}
