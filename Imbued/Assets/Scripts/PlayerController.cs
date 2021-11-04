using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int lives;
    public GameObject RedShot;
    public GameObject BlueShot;
    public GameObject GreenShot;
    public List<string> Colors;
    public bool Respond;
    public int Score;
    public SpriteRenderer spriteRenderer;
    public Sprite RedShip;
    public Sprite BlueShip;
    public Sprite GreenShip;
    public Sprite EmptyShip;
    public Text reText;

    void Start()
    {
        lives=3;
        Colors.Add("RedShot");
        Score=0;
    }
    
    void Update()
    {
        if(lives==0){
            reText.text="PRESS R TO RESTART";
            gameObject.SetActive(false);
        }
        if(!canShoot()){
            spriteRenderer.sprite=EmptyShip;
        }
        else if(Colors[0]=="RedShot"){
            spriteRenderer.sprite=RedShip;
        }
        else if(Colors[0]=="GreenShot"){
            spriteRenderer.sprite=GreenShip;
        }
        else if(Colors[0]=="BlueShot"){
            spriteRenderer.sprite=BlueShip;
        }
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
         if (Input.GetKey(KeyCode.A))
             rb.AddForce(Vector3.left);
         if (Input.GetKey(KeyCode.D))
             rb.AddForce(Vector3.right);
         if (Input.GetKey(KeyCode.W))
             rb.AddForce(Vector3.up);
         if (Input.GetKey(KeyCode.S))
             rb.AddForce(Vector3.down);
        if(Input.GetMouseButtonDown(0) && canShoot()){
            Shoot();
        }
    }
    void Shoot(){
        Respond=false;
        GameObject clone;
        if(Colors[0]=="RedShot"){
            clone = Instantiate(RedShot, transform.position, transform.rotation);
        }
        else if(Colors[0]=="GreenShot"){
            clone = Instantiate(GreenShot, transform.position, transform.rotation);
        }
        else{
            clone = Instantiate(BlueShot, transform.position, transform.rotation);
        }
        Colors.RemoveAt(0);
        ShotAction shotAction= clone.GetComponent<ShotAction>();
        shotAction.fired=true;
        shotAction.switched=false;
        StartCoroutine(setRespond());
    }
    IEnumerator setRespond()
    {
        yield return new WaitForSeconds(0.1f);
        Respond=true;
    }
    private void OnCollisionEnter2D(Collision2D coll){
        if((coll.gameObject.tag=="RedShot" || coll.gameObject.tag=="BlueShot" || coll.gameObject.tag=="GreenShot")&&Respond){
            ShotAction shotAction = coll.gameObject.GetComponent<ShotAction>();
            int addScore= (int)(shotAction.hits);
            if(shotAction.scoreMultiplier>0){
                addScore*=(int)(shotAction.scoreMultiplier);
            }
            Score+=addScore;
            if(lives<4){
                if(lives+shotAction.lives>4){
                    lives=4;
                }
                else{
                    lives+=shotAction.lives;
                }
            }
            Destroy(coll.gameObject);
            Colors.Add(coll.gameObject.tag);
        }
    }
    public bool canShoot(){
        return (Colors.Count!=0);
    }
}
