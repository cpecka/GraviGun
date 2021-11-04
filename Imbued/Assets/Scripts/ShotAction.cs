using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotAction : MonoBehaviour
{
    public float moveSpeed;
    public float hits;
    public GameObject player;
    public Rigidbody2D rb;
    public float attractMult;
    public bool active;
    public bool fired;
    public bool switched;
    public float scoreMultiplier;
    public int lives;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if(fired)
        {
            active=false;
            Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            dir= dir/(dir.magnitude);
            rb= gameObject.GetComponent<Rigidbody2D>();
            Physics2D.IgnoreCollision(gameObject.GetComponent<PolygonCollider2D>(), player.GetComponent<PolygonCollider2D>());
            rb.AddForce((dir)*moveSpeed);
        }
        StartCoroutine(Respond());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space)&& active){
            Vector2 attract= player.transform.position - transform.position;
            attract=attract/attract.magnitude;
            rb.AddForce(attract*attractMult);
        }
    }
    IEnumerator Respond()
    {
        yield return new WaitForSeconds(0.1f);
        Physics2D.IgnoreCollision(gameObject.GetComponent<PolygonCollider2D>(), player.GetComponent<PolygonCollider2D>(), false);
        active=true;
    }
}
