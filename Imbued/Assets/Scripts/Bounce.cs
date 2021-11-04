using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public List<string> AdditionalBounces;
    private Rigidbody2D rb;
    public float BounceRedux;
    public float DragShift;

    Vector3 lastVelocity;
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity=rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D coll){
        if((coll.gameObject.tag=="Wall")){
            bounce(coll);
            if((gameObject.tag=="RedShot" || gameObject.tag=="BlueShot" || gameObject.tag=="GreenShot")&&rb.drag==0){
                rb.drag=DragShift;
            }
        }
        for(int i=0; i<AdditionalBounces.Count; i++){
            if(coll.gameObject.tag==AdditionalBounces[i]){
                bounce(coll);
            }
        }
    }
    private void bounce(Collision2D coll){
        var speed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, coll.contacts[0].normal);
        rb.velocity = direction * Mathf.Max(speed/BounceRedux, 0f);
    }
}
