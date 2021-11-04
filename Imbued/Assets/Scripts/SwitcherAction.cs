using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwitcherAction : MonoBehaviour
{
    private Rigidbody2D oldRb;
    public List<string> ColorTags;
    public GameObject newShot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.tag=="Player" && coll.gameObject.GetComponent<PlayerController>().Colors.Count>0 && !(coll.gameObject.GetComponent<PlayerController>().Colors[0]==newShot.tag)){
            coll.gameObject.GetComponent<PlayerController>().Colors[0]=newShot.tag;
            Destroy(gameObject);
        }
        if(coll.gameObject.tag==ColorTags[0] || coll.gameObject.tag==ColorTags[1]){
            GameObject oldShot=coll.gameObject;
            Quaternion rot=oldShot.transform.rotation;
            Vector3 pos= oldShot.transform.position;
            oldRb=oldShot.GetComponent<Rigidbody2D>();
            Vector3 vel=oldRb.velocity;
            float dragNum = oldRb.drag;
            float spin = oldRb.rotation;
            float hit = oldRb.gameObject.GetComponent<ShotAction>().hits;
            float mult = oldRb.gameObject.GetComponent<ShotAction>().scoreMultiplier;
            int lives = oldRb.gameObject.GetComponent<ShotAction>().lives;
            Destroy(oldShot);
            GameObject clone;
            clone = Instantiate(newShot, transform.position, rot);
            Rigidbody2D newRb=clone.GetComponent<Rigidbody2D>();
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), clone.GetComponent<PolygonCollider2D>());
            newRb.velocity=vel;
            newRb.rotation=spin;
            newRb.gameObject.GetComponent<ShotAction>().hits=hit;
            newRb.gameObject.GetComponent<ShotAction>().scoreMultiplier=mult;
            newRb.gameObject.GetComponent<ShotAction>().lives=lives;
            if(hit>0&& !newRb.gameObject.GetComponent<ShotAction>().switched){
                newRb.gameObject.GetComponent<ShotAction>().switched=true;
            }
            newRb.drag=dragNum;
            Destroy(gameObject);
        }
    }
}
