using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyAI : MonoBehaviour
{

    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public String team;

    public Transform enemyGFX;

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

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Someone entered trigger
        if (collision.tag == "AI R" || collision.tag == "AI L")
        {
            distracted = true;
            path = null;
            //Debug.LogWarning("TRIGGER ENTERED");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (seeker.IsDone())
        {
            if (collision.tag == "AI R" || collision.tag == "AI L") {
                GameObject enemy = collision.gameObject;
                seeker.StartPath(rb.position, enemy.transform.position, OnPathComplete);
                Vector2 direction = ((Vector2) enemy.transform.position - rb.position).normalized;
                Vector2 force = direction * speed * 10000 * Time.deltaTime;

                rb.AddForce(force);

                if (Vector3.Distance(rb.position, enemy.transform.position) <= 0.5f)
                {
                    Debug.LogWarning("FIGHTING TIMNEEE");
                }
            }
        }
    }
}
