using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Tower : MonoBehaviour {

    public float timeBetweenShoots = 2f;

    public float damage = 10;

    public GameObject bullet;

    public Enemies enemies;

    public AudioClip shootSound;

    private DateTime lastTime;
    
    void Start() {
        lastTime = DateTime.Now;
        Shoot();
    }

    private void FixedUpdate() {
        if (lastTime.AddSeconds(timeBetweenShoots).CompareTo(DateTime.Now) < 0) {
            Shoot();
            lastTime = DateTime.Now;
        }
    }

    private void Shoot() {
        GameObject bul = Instantiate(bullet, transform);
        if (GameManager.instance.choosenEnemy != null) {
            bul.GetComponent<Bullet>().enemy = GameManager.instance.choosenEnemy.transform;
        } else {
            bul.GetComponent<Bullet>().enemy = enemies.GetEnemy().transform;
        }
        AudioSource.PlayClipAtPoint(shootSound, transform.position);

        bul.GetComponent<Bullet>().damage = damage;
    }

    public void IncreaseSpeed() {
        //Speed will never be faster than 4 shoots per second
        //elsewise there can be to much enemies on the field and the game can crash
        timeBetweenShoots = ((timeBetweenShoots - 0.25f) * 0.75f) + 0.25f;
        UIManager.instance.SetTowerSpeedText((int)(60 / timeBetweenShoots));
    }

    public void IncreaseDamage() {
        //damage *= 1.1f;
        damage *= 1.2f;
        UIManager.instance.SetTowerDamageText(damage);
    }
}
