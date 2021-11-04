using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    public string ShotTag;
    public GameObject Replicant;
    private bool ready;
    public Rigidbody2D rb;
    public float minSpeed;
    public float enemySpeed;

    public SpriteRenderer spriteRenderer;
    public Sprite Count1;
    public Sprite Count2;
    public Sprite Count3;
    public Sprite NoCount;
    public bool blast;


    // Start is called before the first frame update
    void Start()
    {
        ready=false;
        StartCoroutine(Respond());
        blast=true;
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.magnitude<=minSpeed && blast){
            blast=false;
            StartCoroutine(BlastOff1());
        }
        else if(rb.velocity.magnitude<=minSpeed && !blast){

        }
        else{
            spriteRenderer.sprite=NoCount;
            blast=true;
        }
    }
    private void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.tag=="Player" && !coll.gameObject.GetComponent<PlayerController>().canShoot() && ready){
            coll.gameObject.GetComponent<PlayerController>().lives--;
        }
        if(coll.gameObject.tag==ShotTag && ready){
            Rigidbody2D shotRb=coll.gameObject.GetComponent<Rigidbody2D>();
            Vector3 vel=rb.velocity;
            GameObject clone;
            if(gameObject.transform.localScale.x>2.5){
                clone=Instantiate(Replicant, transform.position, transform.rotation);
                Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), clone.GetComponent<CircleCollider2D>());
                clone.transform.localScale= (clone.transform.localScale)*0.66f;
                Rigidbody2D newRb=clone.GetComponent<Rigidbody2D>();
                newRb.velocity=vel;
            }
            ShotAction shotAction = coll.gameObject.GetComponent<ShotAction>();
            shotAction.hits++;
            if(shotAction.switched){
                if(shotAction.scoreMultiplier<1){
                    shotAction.scoreMultiplier=1;
                }
                shotAction.scoreMultiplier++;
                shotAction.switched=false;
            }
            shotRb.drag=1.6f*(1f/((shotAction.hits+7f)*.125f));
            Destroy(gameObject);
        }
    }
    IEnumerator Respond(){
        yield return new WaitForSeconds(0.5f);
        ready=true;
    }
    IEnumerator BlastOff1(){
        yield return new WaitForSeconds(1f);
        spriteRenderer.sprite=Count3;
        yield return new WaitForSeconds(1f);
        spriteRenderer.sprite=Count2;
        yield return new WaitForSeconds(1f);
        spriteRenderer.sprite=Count1;
        yield return new WaitForSeconds(1f);
        if(rb.velocity.magnitude<=minSpeed){
            Vector3 dir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            dir=dir/dir.magnitude;
            Vector3 vel = enemySpeed * dir;
            gameObject.GetComponent<Rigidbody2D>().velocity = vel;
        }
    }
}
