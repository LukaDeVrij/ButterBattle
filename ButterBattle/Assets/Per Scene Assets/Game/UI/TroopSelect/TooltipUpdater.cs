using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipUpdater : MonoBehaviour
{
    public Image previewImage;
    public TextMeshProUGUI troopName;
    public TextMeshProUGUI troopHP;
    public TextMeshProUGUI troopSpeed;
    public TextMeshProUGUI troopAttackRate;
    public TextMeshProUGUI troopCost;
    public TextMeshProUGUI troopArmorClass;
    public TextMeshProUGUI troopADamage;
    public TextMeshProUGUI troopBDamage;


    // Start is called before the first frame update
    void Start()
    {
        previewImage = gameObject.GetComponentInChildren<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeTooltip(int troopIndex)
    {   
        // Ok here we go, property change fest
        //Img
        string imageFindString = "Enemy" + troopIndex + "Img";
        GameObject imageGO = GameObject.Find(imageFindString);
        SpriteRenderer previewImageFromButton = imageGO.GetComponent<SpriteRenderer>();
        previewImage.sprite = previewImageFromButton.sprite;
        previewImage.color = previewImageFromButton.color;
        
        troopName.text = TroopProperties.nameOf[troopIndex];

        troopHP.text = TroopProperties.hpOf[troopIndex].ToString();

        troopSpeed.text = TroopProperties.speedOf[troopIndex].ToString();

        troopAttackRate.text = TroopProperties.attackRateOf[troopIndex].ToString();

        troopCost.text = TroopProperties.costOf[troopIndex].ToString();

        troopArmorClass.text = TroopProperties.armorClassOf[troopIndex].ToString();

        troopADamage.text = TroopProperties.ADamageMinOf[troopIndex].ToString() + "-" + TroopProperties.ADamageMaxOf[troopIndex].ToString();

        troopBDamage.text = TroopProperties.BDamageMinOf[troopIndex].ToString() + "-" + TroopProperties.BDamageMaxOf[troopIndex].ToString();


    }
}
