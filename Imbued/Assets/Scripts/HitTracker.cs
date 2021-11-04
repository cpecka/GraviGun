using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitTracker : MonoBehaviour
{
    public ShotAction shotAction;
    public Text hitText;
    public int lastCount;
    private bool newLife;
    
    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("RedShot")!=null){
            shotAction=GameObject.FindGameObjectWithTag("RedShot").GetComponent<ShotAction>();
        }
        else if(GameObject.FindGameObjectWithTag("BlueShot")!=null){
            shotAction=GameObject.FindGameObjectWithTag("BlueShot").GetComponent<ShotAction>();
        }
        else if(GameObject.FindGameObjectWithTag("GreenShot")!=null){
            shotAction=GameObject.FindGameObjectWithTag("GreenShot").GetComponent<ShotAction>();
        }
        if(shotAction!=null && shotAction.hits!=0){
            if(shotAction.hits==1){
                hitText.text=shotAction.hits.ToString()+"  HIT";
                newLife=true;
                lastCount=(int)(shotAction.hits);
            }
            else if(shotAction.hits%5==0 && newLife && shotAction.hits!=lastCount){
                hitText.text=shotAction.hits.ToString()+"  HITS\n EXTRA LIFE!";
                shotAction.lives++;
                newLife=false;
                lastCount=(int)(shotAction.hits);
            }
            else if(shotAction.hits!=lastCount){
                hitText.text=shotAction.hits.ToString()+"  HITS";
                newLife=true;
                lastCount=(int)(shotAction.hits);
            }
        }
        if(shotAction==null){
            hitText.text=" ";
            lastCount=0;
        }
    }
    IEnumerator Hide(){
        yield return new WaitForSeconds(2f);
    
    }
}
