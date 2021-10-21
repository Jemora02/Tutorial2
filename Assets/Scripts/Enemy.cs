using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
 public Vector2 speed = new Vector2(10, 10); 
 public Vector2 direction = new Vector2(-1, 0);

    private Rigidbody2D rd2d;
    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {     {
     Vector3 movement = new Vector3(speed.x * direction.x, speed.y * direction.y, 0);
     movement *= Time.deltaTime;
     transform.Translate(movement);
     }
    }
 private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Wall")
        { 
        direction = Vector2.Scale(direction, new Vector2(-1, 0));
        }
}
}