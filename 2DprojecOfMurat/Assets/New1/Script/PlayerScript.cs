using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5f,basmaYonuX,basmaYonuY;
    public Rigidbody2D rbPlayer;
    public Transform transformPlayer;
    public Animator animatorPlayer;
    public bool runTF;
    public GameObject MainCamera;

    void Start()
    {
        rbPlayer=GetComponent<Rigidbody2D>();
        transformPlayer=GetComponent<Transform>();
        animatorPlayer = GetComponent<Animator>();
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        HareketHorizantal();
        HareketVertical();
        AnimasyonChancgeRun();
        CameraFollowPlayer();
        Debug.Log(runTF);
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
        if (BasmaYonu >= 0)
        {
            transformPlayer.localScale = new Vector3(1, 1, 1);//saða
        }
        else 
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

}
