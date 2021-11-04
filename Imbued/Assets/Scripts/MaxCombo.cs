using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxCombo : MonoBehaviour
{
    public int max;
    public int current;
    public ShotAction shotAction;
    public bool reset;

    public Text comboText;
    // Start is called before the first frame update
    void Start()
    {
        max=0;
        current=0;
    }

    // Update is called once per frame
    void Update()
    {
        comboText.text= "Max Combo: "+max.ToString();

        if(GameObject.FindGameObjectWithTag("RedShot")!=null){
            shotAction=GameObject.FindGameObjectWithTag("RedShot").GetComponent<ShotAction>();
            reset=true;
        }
        else if(GameObject.FindGameObjectWithTag("BlueShot")!=null){
            shotAction=GameObject.FindGameObjectWithTag("BlueShot").GetComponent<ShotAction>();
            reset=true;
            
        }
        else if(GameObject.FindGameObjectWithTag("GreenShot")!=null){
            shotAction=GameObject.FindGameObjectWithTag("GreenShot").GetComponent<ShotAction>();
            reset=true;
        }
        if(shotAction!=null){
            current=(int)(shotAction.hits);
        }
        if(shotAction==null && reset){
            reset=false;
            if(current>max){
                max=current;
            }
            current=0;
        }

    }
}
