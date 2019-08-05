using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 20f;
    public float damage = 0;
    public Transform enemy;

    public GameObject bulletParticleSystem;
    public AudioClip bulletSound;
    
    void Update() {
        if (enemy != null) {
            Vector3 dir = enemy.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        } else {
            if (GameManager.instance.choosenEnemy != null) {
                enemy = GameManager.instance.choosenEnemy.transform;
            } else {
                GameObject en = FindObjectOfType<Enemies>().GetEnemy();
                if (en != null) enemy = en.transform;
            }
        }
    }

    public void DestroyBullet() {
        Vector3 tmpVec = transform.position;
        AudioSource.PlayClipAtPoint(bulletSound, tmpVec);
        Instantiate(bulletParticleSystem, tmpVec, Quaternion.identity);
        Destroy(gameObject);
    }
}
