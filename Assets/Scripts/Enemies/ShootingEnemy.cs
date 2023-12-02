using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class ShootingEnemy : MonoBehaviour
{
   [SerializeField] private float reloadTime;
   [SerializeField] private float bulletForce;
   [SerializeField] private GameObject bullet;
   [SerializeField] private float agroDistance;

    private bool playerVisible = false;
    private bool playerLeft = false;
    private bool isReloading = false;

    private GameObject player;

    private Rigidbody2D bulletRB;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("Shoot", 0f, reloadTime);
    }

    void Update()
    {
        float currentDistance = Vector2.Distance(transform.position, player.transform.position);

        if(currentDistance <= agroDistance){
            Vector2 raycastDir = player.transform.position - transform.position;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDir, agroDistance);
            Debug.DrawRay(transform.position, raycastDir, Color.red);

            if(hit == true){

                if(hit.collider.gameObject.tag == "Player"){
                    playerVisible = true;
                    transform.right = player.transform.position - transform.position;
                    if(!isReloading){
                        StartCoroutine(Shoot());
                        
                    }
                }
            }
            playerLeftCheck();
        }else{
            playerVisible = false;
        }
    }

    IEnumerator Shoot(){
        isReloading = true;
        GameObject bulletCopy = Instantiate(bullet,transform.position,transform.rotation);
        bulletRB = bulletCopy.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(transform.right*bulletForce);

        yield return new WaitForSeconds(reloadTime);

        isReloading = false;
    }

    void playerLeftCheck(){
        float playerX = player.GetComponent<Transform>().position.x;
        float enemyX = transform.position.x;

        if(enemyX > playerX){
            playerLeft = true;
        }else{
            playerLeft = false;
        }

        if(playerLeft){
            transform.localScale = new Vector2(1,-1);
        }else{
            transform.localScale = new Vector2(1,1);
        }
    }

}
