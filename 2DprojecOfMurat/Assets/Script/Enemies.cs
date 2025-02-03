using System;
using System.Collections;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemies : MonoBehaviour
{
    public bool follow;
    public float speed = 5f,health=60f;
    public int visionX = 7;
    public int visionY = 3;
    public GameObject player;
    public Rigidbody2D enemiesRb;
    public Animator enemiesAnim;
    public float yaklasmaiGerekenMesafeX = 1f, yaklasmaiGerekenMesafeYPOZ = 0.7f, yaklasmaiGerekenMesafeYNEG = -0.3f, zararaldigindebekelmesuresi = 0.2f;
    private void Start()
    {
        enemiesAnim.SetBool("Attack", false);
        player = GameObject.FindGameObjectWithTag("Player");
        enemiesRb = this.GetComponent<Rigidbody2D>();
        enemiesAnim = this.GetComponent<Animator>();
        enemiesAnim.SetBool("Hit", false);

    }
    private void Update()
    {
        FollowingDistance();
    }
    public void FollowingDistance()
    {
        float playerXdistance = player.transform.position.x - this.gameObject.transform.position.x;
        float playerYdistance = player.transform.position.y - this.gameObject.transform.position.y;
        //We should attend to distance for follow, this code for follow the player.
        if (MathF.Abs(playerXdistance) > visionX)
        {
            enemiesAnim.SetBool("Run", false);
            enemiesRb.velocity = new Vector2(0f, 0f);


        }
        else 
        {
            if (MathF.Abs(playerXdistance) <= visionX)
            {
                if (playerXdistance > yaklasmaiGerekenMesafeX || playerXdistance < -yaklasmaiGerekenMesafeX)
                {
                    enemiesAnim.SetBool("Run", true);
                    enemiesRb.velocity = new Vector2(playerXdistance * speed / MathF.Abs(playerXdistance), enemiesRb.velocity.y);
                }
                else
                {
                    enemiesAnim.SetBool("Run", false);
                    enemiesRb.velocity = new Vector2(0f, enemiesRb.velocity.y);
                }


            }
        }
        if (MathF.Abs(playerYdistance) > visionY)
        {
            enemiesAnim.SetBool("Run", false);
            enemiesRb.velocity = new Vector2(0f, 0f);
        }
        else 
        {
            // same things for Vertical position
            if (MathF.Abs(playerYdistance) <= visionY)
            {
                if (playerYdistance > yaklasmaiGerekenMesafeYPOZ || playerYdistance < -yaklasmaiGerekenMesafeYNEG)
                {
                    enemiesAnim.SetBool("Run", true);

                    enemiesRb.velocity = new Vector2(enemiesRb.velocity.x, playerYdistance * speed / MathF.Abs(playerYdistance));
                }
                else
                {
                    enemiesAnim.SetBool("Run", false);
                    enemiesRb.velocity = new Vector2(enemiesRb.velocity.x, 0f);
                }


            }
        }
        FlipFaceEnemies(playerXdistance);
        Attack(playerXdistance, playerYdistance);

    }
    public void FlipFaceEnemies(float playerXdistance)
    {
        if (playerXdistance >= 0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else 
        { 
            this.transform.localScale=new Vector3(-1, 1, 1);
        }  
    }
    public void Attack(float playerXdistance,float playerYdistance)
    {
        if (playerYdistance<yaklasmaiGerekenMesafeYPOZ&& playerYdistance > yaklasmaiGerekenMesafeYNEG && Mathf.Abs(playerXdistance) < yaklasmaiGerekenMesafeX && MathF.Abs(playerXdistance)>0.2f)
        {
            enemiesAnim.SetBool("Attack", true);
            enemiesRb.velocity = new Vector2(0f,0f);
        }
        else
        {
            enemiesAnim.SetBool("Attack", false);

        }
        if (MathF.Abs(playerXdistance) <= 0.15)
        {
            enemiesRb.velocity = new Vector2(speed/2, 0f);
        }
    }
    public void TakeHit(float hitCount)
    {
        enemiesAnim.SetBool("Hit", true);
        enemiesAnim.SetBool("Attack", false);
        enemiesRb.velocity = Vector2.zero;

        health -= hitCount;
        if (health <= 0)
        {
            Destroy(this.transform.parent);
            Destroy(this.gameObject);
        }
        else 
        {
            StartCoroutine(HitAnimStopEnemies());
        }
    }
    public IEnumerator HitAnimStopEnemies()
    {
        yield return new WaitForSeconds(zararaldigindebekelmesuresi);
        enemiesAnim.SetBool("Hit", false);


    }
}

