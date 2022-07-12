using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{

    private TextMeshPro textMesh;
    private float disappearTimer = 0.3f;
    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount)
    {
        textMesh.text = damageAmount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        float moveYSpeed = 2f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            //start disappeare
            Color color = textMesh.color;
            float disappearSpeed = 2f;
            color.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = color;
            if (color.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
