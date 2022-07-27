using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float RotateSpeed, SecondsToMove;

    public Sprite[] EnemiesSprites;
    private SpriteRenderer spriteRenderer;

    public AudioClip[] Sounds;
    public AudioSource SoundEffects;

    int spriteSelector;
    int HP = 1;
    int scoreAdd = 1;

    //Flash Damage Animation
    [SerializeField]
    Material flashMaterial;

    [SerializeField]
    float duration;

    private Material originalMaterial;

    private Coroutine flashRoutine;


    private void Start()
    {
        StartCoroutine(MoveEnemy());
        SelectSprite();
        SoundEffects = GameObject.FindGameObjectWithTag("EnemySounds").GetComponent<AudioSource>();

        originalMaterial = spriteRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 1 * RotateSpeed);
        if(transform.position.y <= -2.392f)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    IEnumerator MoveEnemy()
    {
        if(HP > 0)
        {
            yield return new WaitForSeconds(SecondsToMove);
            if (GameManager.levelcount == 1)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
            }
            else 
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);
            }

            StartCoroutine(MoveEnemy());
        }
      
    }
    public void Die()
    {
        SoundEffects.PlayOneShot(Sounds[1]);
        Destroy(gameObject);
        GameManager.score += scoreAdd;
        GameManager.enemycount--;
        if(GameManager.levelcount == 0)
        {
            if (GameManager.enemycount == 0)
            {
                GameManager.levelcount++;
                SceneManager.LoadScene("Game");
            }
        }
        else if (GameManager.levelcount == 1)
        {
            if (GameManager.enemycount == 0)
            {     
                 SceneManager.LoadScene("Win");        
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" )
        {
            SoundEffects.PlayOneShot(Sounds[0]);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Bullet")
        {
            Flash();
            HP--;
            SoundEffects.PlayOneShot(Sounds[0]);
            Destroy(collision.gameObject);
            if(HP == 0)
            {
                Die();
            }
        }
    }

    void SelectSprite()
    {
        spriteSelector = Random.Range(0, 3);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = EnemiesSprites[spriteSelector];
        

        switch (spriteSelector)
        {
            case 0:
                HP = 1;
                scoreAdd = 1;
                break;
            case 1:
                HP = 2;
                scoreAdd = 2;
                break;
            case 2:
                HP = 3;
                scoreAdd = 3;
                break;
        }

    }

    public void Flash()
    {

        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {

        spriteRenderer.material = flashMaterial;

        yield return new WaitForSeconds(duration);

        spriteRenderer.material = originalMaterial;

        flashRoutine = null;
    }

}

