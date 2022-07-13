using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    /*/
     * DIT IS DE GROOTSTE KUT DUCKTAPE CODE OOIT
     * DIT FF EEN ANDERE KEER HELEMAAL OPNIEUW DOEN
     * BUGS NU ZIJN HEEL VEEL EN GROOT
     * SJIT MET ANGULAR DRAG ENZO, EN SPEED * 10000 ZONDER REDEN
     * Na een kill lijkt een deel van de AI die niet gevochten heeft maar wel vast zat lijkt niet door te lopen GEFIXT
     * Soms lijkt men niet terug te vechten
     * FUCK DIT, alleen HIT werkt perfect (maar niet op beacons)
    /*/

    public Transform target;
    public float nextWaypointDistance = 5f;
    public GameObject parentObject;
    public Transform enemyGFX;
    public Animator animator;
    public GameObject damagePopupPrefab;
    public Beacon beacon;
    public GameController controller;
    public int troopIndex;

    //Properties that get set based on TroopProperties.cs
    public int cost;
    public int hp;
    public float speed;
    public float attackRate;
    public int ADamageMin;
    public int ADamageMax;
    public int BDamageMin;
    public int BDamageMax;
    public string armorClass;

    //Hit timer thingy
    float nextAttackTime = 0f;

    //Pathseeking bollocks
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

        if (gameObject.tag =="AI L")
        {
            target = GameObject.Find("TargetR").transform;
            GameObject controllerGameObj = GameObject.Find("GameController");
            controller = controllerGameObj.GetComponent<GameController>();
            GameObject beaconGameObj = GameObject.Find("TargetR");
            beacon = beaconGameObj.GetComponent<Beacon>();
        }
        if (gameObject.tag == "AI R")
        {
            target = GameObject.Find("TargetL").transform;
            GameObject controllerGameObj = GameObject.Find("GameController");
            controller = controllerGameObj.GetComponent<GameController>();
            GameObject beaconGameObj = GameObject.Find("TargetL");
            beacon = beaconGameObj.GetComponent<Beacon>();
        }

        InvokeProperties(troopIndex);

    }

    public void InvokeProperties(int troopIndex)
    {

        cost = TroopProperties.costOf[troopIndex];
        hp = TroopProperties.hpOf[troopIndex];
        speed = TroopProperties.speedOf[troopIndex];
        attackRate = TroopProperties.attackRateOf[troopIndex];
        ADamageMin = TroopProperties.ADamageMinOf[troopIndex];
        ADamageMax = TroopProperties.ADamageMaxOf[troopIndex];
        BDamageMin = TroopProperties.BDamageMinOf[troopIndex];
        BDamageMax = TroopProperties.BDamageMaxOf[troopIndex];

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


        if (hp <= 0)
        {
            Destroy(gameObject);
            //Death
        }

        //Hit beacon
        if (Vector3.Distance(parentObject.transform.position, target.position) <= 3f)
        {
            HitEnemy(target.gameObject);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Someone entered trigger
        if (collision.tag == "AI R" && parentObject.tag == "AI L")
        {
            distracted = true;
            path = null;
        }
        // Someone entered trigger
        else if (collision.tag == "AI L" && parentObject.tag == "AI R")
        {
            distracted = true;
            path = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (seeker.IsDone())
        {
            if ((collision.tag == "AI R"  && parentObject.tag == "AI L") | (collision.tag == "AI L" && parentObject.tag == "AI R"))
            {
                distracted = true;
                path = null;
                GameObject enemy = collision.gameObject;
                seeker.StartPath(rb.position, enemy.transform.position, OnPathComplete);
                Vector2 direction = ((Vector2) enemy.transform.position - rb.position).normalized;
                Vector2 force = direction * speed * 10000 * Time.deltaTime;

                rb.AddForce(force);

                if (Vector3.Distance(parentObject.transform.position, enemy.transform.position) <= 2f)
                {
                    rb.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeAll;
                    HitEnemy(collision.gameObject);

                }  
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
        rb.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        rb.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    void HitEnemy(GameObject collision)
    {
        //Hit logic
        //Play animation
        //animator.SetTrigger("Hit"); See brackeys video
        if (Time.time >= nextAttackTime)
        {   
            
            //Randomise hit damage
            int damagePerHit = Random.Range(ADamageMin, ADamageMax);

            //Popup damage
            GameObject damagePopupObject = Instantiate(damagePopupPrefab, collision.transform.position + new Vector3(0,1,0), Quaternion.identity);
            DamagePopup damagePopup = damagePopupObject.GetComponent<DamagePopup>();
            damagePopup.Setup(damagePerHit);

            //Actual hit damage
            
            if (collision.tag == "AI R" | collision.tag == "AI L") {
                collision.GetComponent<EnemyAI>().hp = collision.GetComponent<EnemyAI>().hp - damagePerHit;
            }

            if (collision.tag == "BEACON L" | collision.tag == "BEACON R")
            {
                collision.GetComponent<Beacon>().HP = collision.GetComponent<Beacon>().HP - damagePerHit;
                beacon.updateHPDisplay();
                controller.WinLossCheck();
                  
            }
            Debug.Log("Hit enemy for " + damagePerHit);

            //Reset hit timer
            nextAttackTime = Time.time + 1f / attackRate;

            //If the enemy died, no longer distracted, if enemy is still alive, he will be in the trigger and this will be reverted instantly
            //Seems to work good, sometimes people slow down after defeating somethings, but that ought to be a bug somewhere else
            distracted = false;
            seeker.StartPath(rb.position, target.position, OnPathComplete);
            rb.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            rb.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        }

    }
}
