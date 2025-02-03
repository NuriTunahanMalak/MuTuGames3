using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5f,speedsabit,basmaYonuX,basmaYonuY,saldiridensonrabekle=0.5f;
    public Rigidbody2D rbPlayer;
    public Transform transformPlayer;
    public Animator animatorPlayer;
    public bool runTF;
    public GameObject MainCamera;
    public float can = 100f, giveDamage = 20f;

    void Start()
    {
        rbPlayer=GetComponent<Rigidbody2D>();
        transformPlayer=GetComponent<Transform>();
        animatorPlayer = GetComponent<Animator>();
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        animatorPlayer.SetBool("Hit", false);
        animatorPlayer.SetBool("Attack", false);
        speedsabit = speed;

    }

    void Update()
    {
        HareketHorizantal();
        HareketVertical();
        AnimasyonChancgeRun();
        CameraFollowPlayer();
        AttackAnim();
    }
    public void HareketHorizantal()
    {
        basmaYonuX = Input.GetAxis("Horizontal");//yan hareketin yönünü buluyor negatif ve pozitif olarak
        rbPlayer.velocity = new Vector2(basmaYonuX * speed, rbPlayer.velocity.y);//hareket etmesini saðlýyor
        FlipFace(basmaYonuX);// Bunu eðer sola giderse yüzü deðiþöesi için yaptýk 

    }
    public void HareketVertical()
    {
        basmaYonuY = Input.GetAxis("Vertical");//yatay hareketin yönünü buluyor negatif ve pozitif olarak
        rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, basmaYonuY*speed);//hareket etmesini saðlýyor

    }
    public void FlipFace(float BasmaYonu)
    {
        if (BasmaYonu > 0)
        {
            transformPlayer.localScale = new Vector3(1, 1, 1);//saða
        }
        else if(BasmaYonu<0)
        {
            transformPlayer.localScale = new Vector3(-1, 1, 1);//sola dönsün
        }
    }
    public void AnimasyonChancgeRun()
    {
        //We make to set Animation.
        if (basmaYonuX == 0 && basmaYonuY == 0)
        {
            runTF = false;
        }
        else 
        { 
            runTF = true;
        }  
        animatorPlayer.SetBool("yuruyormu", runTF);
    }
    public void CameraFollowPlayer()
    {
        MainCamera.transform.position = new Vector3(transformPlayer.position.x,transformPlayer.position.y,-5);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Skeleton"))
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage(float damage) 
    {
        animatorPlayer.SetBool("Hit", true);
        can -= damage;
        if (can <= 0)
        {
            Destroy(this.gameObject.transform.parent);
            Destroy(this.gameObject);
        }
        else 
        {
            StartCoroutine(StopHitAnimation());
        }

    }
    public void AttackAnim()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            rbPlayer.velocity = Vector2.zero;
            animatorPlayer.SetBool("Attack", true);
            StartCoroutine(StopAttackAnimation());

        }

    }
    private IEnumerator StopHitAnimation()
    {
        yield return new WaitForSeconds(0.15f);
        animatorPlayer.SetBool("Hit", false);
    }
    private IEnumerator StopAttackAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        animatorPlayer.SetBool("Attack", false);
        StartCoroutine(Saldiridensonrabekle());
    }
    public void Yavaslama(float yavaslamasuresi,float kacbolum)
    {
        speed=speed/kacbolum;
        StartCoroutine(YavaslamaSuresi(yavaslamasuresi));
    }
    public IEnumerator YavaslamaSuresi(float yavaslamasuresi)
    {
        yield return new WaitForSeconds(yavaslamasuresi);
        speed = speedsabit;
    }
    public IEnumerator Saldiridensonrabekle()
    {
        yield return new WaitForSeconds(saldiridensonrabekle);
    }
}
