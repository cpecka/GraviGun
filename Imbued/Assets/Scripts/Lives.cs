using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    public PlayerController playerController;
    public Text livesText;
    // Update is called once per frame
    void Update()
    {
        if(playerController.lives==4){
            livesText.text="Lives: "+playerController.lives.ToString()+"  MAX";
        }
        else{
            livesText.text="Lives: "+playerController.lives.ToString();
        }
    }
}
