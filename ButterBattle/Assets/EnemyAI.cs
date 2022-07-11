using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{

    public Transform target;
    public float nextWaypointDistance = 5f;
    public GameObject parentObject;
    public Transform enemyGFX;
    public Animator animator;
    public GameObject damagePopupPrefab;

    //Properties
    public int cost;
    public int HP;
    public float speed = 200f;
    public float attackRate;
    float nextAttackTime = 0f;
    public int damagePerHitMin;
    public int damagePerHitMax;
    


    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    bool distracted = false;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);

        
        
    }

    void UpdatePath()
    {
        if (distracted == false)
        {
            if (seeker.IsDone())
            {
                seeker.StartPath(rb.position, target.position, OnPathComplete);
            }
        }
        
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (path == null)
        {
            return;
        } 
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        } else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2) path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * 10000 * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }


        if (rb.velocity.x >= 0.01f && force.x > 0f)
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (rb.velocity.x <= -0.01 && force.x < 0f)
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }


        if (HP <= 0)
        {
            Destroy(gameObject);
            //Death
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Someone entered trigger
        if ((collision.tag == "AI R" | collision.tag == "BEACON R") && parentObject.tag == "AI L")
        {
            distracted = true;
            path = null;
            Debug.LogWarning("Combat");
        }
        // Someone entered trigger
        else if ((collision.tag == "AI L" | collision.tag == "BEACON L") && parentObject.tag == "AI R")
        {
            distracted = true;
            path = null;
            Debug.LogWarning("Combat");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (seeker.IsDone())
        {
            if (collision.tag == "AI R" && parentObject.tag == "AI L") {
                distracted = true;
                path = null;
                GameObject enemy = collision.gameObject;
                seeker.StartPath(rb.position, enemy.transform.position, OnPathComplete);
                Vector2 direction = ((Vector2) enemy.transform.position - rb.position).normalized;
                Vector2 force = direction * speed * 10000 * Time.deltaTime;

                rb.AddForce(force);
                if (Vector3.Distance(parentObject.transform.position, enemy.transform.position) <= 1f)
                {
                    rb.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeAll;
                    HitEnemy(collision);

                }
            }
            else if (collision.tag == "AI L" && parentObject.tag == "AI R")
            {
                distracted = true;
                path = null;
                GameObject enemy = collision.gameObject;
                seeker.StartPath(rb.position, enemy.transform.position, OnPathComplete);
                Vector2 direction = ((Vector2)enemy.transform.position - rb.position).normalized;
                Vector2 force = direction * speed * 10000 * Time.deltaTime;

                rb.AddForce(force);

                if (Vector3.Distance(parentObject.transform.position, enemy.transform.position) <= 1f)
                {
                    
                    rb.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeAll;
                    HitEnemy(collision);
                }
            }
        }
    }
    void HitEnemy(Collider2D collision)
    {
        //Hit logic
        //Play animation
        //animator.SetTrigger("Hit"); See brackeys video
        if (Time.time >= nextAttackTime)
        {   

            //Randomise hit damage
            int damagePerHit = Random.Range(damagePerHitMin, damagePerHitMax);

            //Popup damage
            GameObject damagePopupObject = Instantiate(damagePopupPrefab, collision.transform.position + new Vector3(0,1,0), Quaternion.identity);
            DamagePopup damagePopup = damagePopupObject.GetComponent<DamagePopup>();
            damagePopup.Setup(damagePerHit);

            //Actual hit damage
            collision.GetComponent<EnemyAI>().HP = collision.GetComponent<EnemyAI>().HP - damagePerHit;
            Debug.Log("Hit enemy for " + damagePerHit);
            //Reset hit timer
            nextAttackTime = Time.time + 1f / attackRate;

            //If the enemy died, no longer distracted, if enemy is still alive, he will be in the trigger and this will be reverted instantly
            //
            distracted = false;
            seeker.StartPath(rb.position, target.position, OnPathComplete);
            rb.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        }

        //Damage collision gameobject
    }
}
