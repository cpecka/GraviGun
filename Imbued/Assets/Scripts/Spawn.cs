using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
using UnityEngine.SceneManagement;

public class Spawn : MonoBehaviour
{
    //import UnityEngine.SceneManagement; 
    private GameObject[] RedEnemies;
    private GameObject[] RedSwitchers;
    private GameObject[] BlueEnemies;
    private GameObject[] BlueSwitchers;
    private GameObject[] GreenEnemies;
    private GameObject[] GreenSwitchers;
    public GameObject RedSwitcher;
    public GameObject BlueSwitcher;
    public GameObject RedEnemy;
    public GameObject BlueEnemy;
    public GameObject GreenSwitcher;
    public GameObject GreenEnemy;
    public float enemySpeed;
    private bool RedBalance;
    private bool BlueBalance;
    private bool GreenBalance;
    private bool EnemyBalance;
    // Start is called before the first frame update
    void Start()
    {
        RedBalance=false;
        BlueBalance=false;
        GreenBalance=false;
        EnemyBalance=false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R)) { //If you press R
            SceneManager.LoadScene("Game");
        }
        RedEnemies=GameObject.FindGameObjectsWithTag("RedEnemy");
        BlueEnemies=GameObject.FindGameObjectsWithTag("BlueEnemy");
        BlueSwitchers=GameObject.FindGameObjectsWithTag("BlueSwitcher");
        RedSwitchers=GameObject.FindGameObjectsWithTag("RedSwitcher");
        GreenEnemies=GameObject.FindGameObjectsWithTag("GreenEnemy");
        GreenSwitchers=GameObject.FindGameObjectsWithTag("GreenSwitcher");

        if(RedEnemies.Length+BlueEnemies.Length+GreenEnemies.Length<3 && !(EnemyBalance)){
            EnemyBalance=true;
            StartCoroutine(SpawnEnemy());
        }

        if(RedEnemies.Length>=1 && RedSwitchers.Length==0 && !(RedBalance)){
            RedBalance=true;
            StartCoroutine(SpawnRedSwitcher());
        }
        if(BlueEnemies.Length>=1 && BlueSwitchers.Length==0 && !(BlueBalance)){
            BlueBalance=true;
            StartCoroutine(SpawnBlueSwitcher());
        }
        if(GreenEnemies.Length>=1 && GreenSwitchers.Length==0 && !(GreenBalance)){
            GreenBalance=true;
            StartCoroutine(SpawnGreenSwitcher());
        }
    }
    IEnumerator SpawnRedSwitcher(){
        yield return new WaitForSeconds(3.0f);
        GameObject clone;
        clone=Instantiate(RedSwitcher, new Vector3(Random.Range(-21.5f, 21.5f), Random.Range(-8, 8), 1), Quaternion.identity);
        RedBalance=false;
    }
    IEnumerator SpawnBlueSwitcher(){
        yield return new WaitForSeconds(3.0f);
        GameObject clone;
        clone=Instantiate(BlueSwitcher, new Vector3(Random.Range(-21.5f, 21.5f), Random.Range(-8, 8), 1), Quaternion.identity);
        BlueBalance=false;
    }
    IEnumerator SpawnGreenSwitcher(){
        yield return new WaitForSeconds(3.0f);
        GameObject clone;
        clone=Instantiate(GreenSwitcher, new Vector3(Random.Range(-21.5f, 21.5f), Random.Range(-8, 8), 1), Quaternion.identity);
        GreenBalance=false;
    }
    IEnumerator SpawnEnemy(){
        yield return new WaitForSeconds(5.0f);
        GameObject clone;
        float option= Random.Range(1f, 10f);
        if(option<4f){
            clone=Instantiate(BlueEnemy, new Vector3(Random.Range(-21.5f, 21.5f), Random.Range(-8, 8), 1), Quaternion.identity);
        }
        else if(option<7f){
            clone=Instantiate(GreenEnemy, new Vector3(Random.Range(-21.5f, 21.5f), Random.Range(-8, 8), 1), Quaternion.identity);
        }
        else{
            clone=Instantiate(RedEnemy, new Vector3(Random.Range(-21.5f, 21.5f), Random.Range(-8, 8), 1), Quaternion.identity);
        }
        /*
        Vector3 dir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        dir=dir/dir.magnitude;
        Vector3 vel = enemySpeed * dir;
        clone.GetComponent<Rigidbody2D>().velocity = vel;
        */
        EnemyBalance=false;
    }
}
