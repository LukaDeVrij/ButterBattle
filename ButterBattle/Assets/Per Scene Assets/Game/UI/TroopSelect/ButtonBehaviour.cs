using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour
{

    public Button button;
    public int troopIndex;
    public GameObject previewImageGameObject;
    public Image backgroundImage;

    void Start()
    {
        Image previewImage = previewImageGameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TroopProperties.troopSelected == troopIndex)
        {
            backgroundImage.color = Color.grey;
        }
        else
        {
            backgroundImage.color = Color.white;
        }
    }

    public void onClick()
    {
        TroopProperties.troopSelected = troopIndex;
        


    }

    public void onHoverEnter()
    {
       
    }

    public void onHoverQuit()
    {

    }

}
