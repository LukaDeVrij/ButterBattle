using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconDefense : MonoBehaviour
{

    float nextAttackTime = 0f;
    float attackRate = 1f;
    public Transform pfBullet;
    public Transform spawnLocation;
    public string toBeTargeted;
    public int damage;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == toBeTargeted)
        {

            GameObject enemy = other.gameObject;
            if (Time.time >= nextAttackTime)
            {

                Transform bulletTransform = Instantiate(pfBullet, spawnLocation.position, Quaternion.identity);

                Vector3 shootDir = (other.transform.position - spawnLocation.position).normalized;
                bulletTransform.GetComponent<Bullet>().Setup(shootDir);
                bulletTransform.GetComponent<Bullet>().target = enemy;
                bulletTransform.GetComponent<Bullet>().damage = damage;


                nextAttackTime = Time.time + 1f / attackRate;


            }
        }

    }

}
