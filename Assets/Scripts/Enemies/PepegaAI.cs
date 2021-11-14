using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PepegaAI : Enemy
{
    public Rigidbody2D rb;
    public Collider2D objectCollider;
    public float speed;
    public GameObject sprite;
    public ParticleSystem deadParticles;
    public ParticleSystem walkParticles;
    bool dead;

    void Update()
    {
        if(!dead) rb.velocity = transform.right * speed;
    }

    IEnumerator Dead()
    {
        dead = true;
        objectCollider.enabled = false;
        walkParticles.Stop();
        sprite.SetActive(false);
        deadParticles.Play();
        yield return new WaitForSeconds(deadParticles.main.duration);
        Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if(collision2D.GetContact(0).normal.x == 1) transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
        if(collision2D.GetContact(0).normal.x == -1) transform.rotation = Quaternion.Euler(transform.rotation.x, -180, transform.rotation.z);

        if(collision2D.gameObject.tag == "Player")
        {
            if(collision2D.GetContact(0).normal.y == -1) StartCoroutine("Dead");
        }
    }
}
