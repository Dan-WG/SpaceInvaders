using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5;
    public Transform ShootPoint;
    public GameObject bullet;
    float FireRate = .5f;
    float nextFire = 0;
    public AudioSource shootSound;


    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x < 8.39f)
            {
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }
                
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x > -8.53f)
            {
                transform.position += Vector3.right * -moveSpeed * Time.deltaTime;
            }
                
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            //Shoot
            if(Time.time > nextFire)
            {
                shootSound.Play();
                nextFire = Time.time + FireRate;
               Shoot(); 
            }
        }

    }

    void Shoot()
    {
            GameObject bulletInstance = Instantiate(bullet, ShootPoint.position, ShootPoint.rotation);
            bulletInstance.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 200);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            SceneManager.LoadScene("GameOver");
        }
    }
}
