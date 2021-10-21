using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;
    public Text life;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    

    private int scoreValue = 0;
    private int lifeValue = 3;
    private bool facingRight = true;
    private bool isJumping = true;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); 
        score.text = scoreValue.ToString();
        life.text = lifeValue.ToString();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (Input.GetKeyDown(KeyCode.W))
        {
        
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
        
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
        anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
        anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
        anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
        anim.SetInteger("State", 0);
        }
        if (facingRight == false && hozMovement > 0)
        {
        Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
        Flip();
        }
        if (isJumping == true && vertMovement < 0 )
        {
        Jump();
        }
        else if (isJumping == true && vertMovement > 0)
        {
        Jump();
        }

        if (Input.GetKey("escape"))
{
Application.Quit();
}
    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }
    void Jump()
   {
       isJumping = !isJumping;

       anim.SetInteger("State", 2);
   }
    }
      private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);

        }
         if (collision.collider.tag == "Enemy")
        {
            lifeValue -= 1;
            life.text = lifeValue.ToString();
            Destroy(collision.collider.gameObject);

        }
        if (collision.collider.tag == "Door")
        {
        transform.position = new Vector3(100.0f, 1.0f, 0.0f);
        lifeValue = 3;
        life.text = lifeValue.ToString();
        }
        if (collision.collider.tag == "Door 2")
        {
        transform.position = new Vector3(0.0f, 1.0f, 0.0f);
        lifeValue = 3;
        life.text = lifeValue.ToString();
        }
        if (scoreValue >= 8)
        {
            winTextObject.SetActive(true);
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            musicSource.loop = true;
            rd2d.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        if (lifeValue <= 0)
        {
            loseTextObject.SetActive(true);
            rd2d.constraints = RigidbodyConstraints2D.FreezeAll;
        }


    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
}