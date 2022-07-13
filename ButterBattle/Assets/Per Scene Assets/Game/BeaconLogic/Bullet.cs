using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 shootDirection;
    public float instantiateTime;
    public GameObject target;
    public GameObject damagePopupPrefab;
    public int damage;

    public void Setup(Vector3 shootDirection)
    {
        this.shootDirection = shootDirection;
        instantiateTime = Time.time;
    }

    public void Update()
    {

        //Move
        float aliveTime = Time.time - instantiateTime;
        float moveSpeed = 10f;
        transform.position += shootDirection * Time.deltaTime * moveSpeed;

        //Destroy protocol
        Color tempColor = gameObject.GetComponentInChildren<SpriteRenderer>().color;
        tempColor.a = 1 - (aliveTime * 0.7f);
        gameObject.GetComponentInChildren<SpriteRenderer>().color = tempColor;

        if (gameObject.GetComponentInChildren<SpriteRenderer>().color.a <= 0.001f)
        {
            Destroy(gameObject);
        }

        // Hit detection
        if (Vector3.Distance(target.transform.position, gameObject.transform.position) <= 0.5f)
        {
            //Popup damage
            GameObject damagePopupObject = Instantiate(damagePopupPrefab, target.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            DamagePopup damagePopup = damagePopupObject.GetComponent<DamagePopup>();
            damagePopup.Setup(damage);

            //Actual hit 
            target.GetComponent<EnemyAI>().hp = target.GetComponent<EnemyAI>().hp - damage;
            
            Debug.Log("BEACON Hit enemy for " + damage);
            
            //Destroy bullet on hit
            Destroy(gameObject);
        }

    }
}
