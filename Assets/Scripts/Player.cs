using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float speed, jumpBoost;
    public GameObject bubble, oldsScore, input;
    public static bool bounced;
    public ParticleSystem bubblePop;
    public AudioClip[] audioClip;
    public float[] spawnInt, objInt;
    public GameObject[] objects;

    private bool dead = false;
    private Animator anim;
    private float elaspedTime, currentInt, objTimer, moving;
    private Vector3 spawnPos;
    private AudioSource mus;

    void Start()
    {
        //Get Animation
        anim = GetComponent<Animator>();

        //Get AudioSource
        mus = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        //Creates random int for currentInt
        currentInt = Random.Range(spawnInt[0], spawnInt[1]);

        //Object timer
        objTimer = Random.Range(objInt[0], objInt[1]);

        //Sets spawnPos
        spawnPos = new Vector3(13, Random.Range(-5, -3));
    }

    void Update()
    {
        if (transform.position.y < -8.20)
        {
            if (!GameObject.FindGameObjectWithTag("GameController").GetComponent<GameScore>().isDead)
            {
                Death();
            }
        }

        //Adds elapsedTime
        elaspedTime += Time.deltaTime;

        #region Player Stuff
        if (!dead)
        {
            //Player movement
            transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0) * speed * Time.deltaTime;

            moving = Input.GetAxis("Horizontal");

            if (Input.GetAxis("Horizontal") < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            if (Input.GetAxis("Horizontal") > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }

            if (GetComponent<Rigidbody2D>().velocity.y != 0)
            {
                anim.SetBool("isJumping", true);
                anim.SetBool("Grounded", false);
            }
            else
            {
                anim.SetBool("isJumping", false);
                anim.SetBool("Grounded", true);
            }

            if (moving != 0)
            {
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }

            //Getting player position
            Vector3 clampPos = transform.position;

            //Clamping X position
            clampPos.x = Mathf.Clamp(transform.position.x, -9.84f, 9.84f);

            //assigning clamp position to player
            transform.position = clampPos;
        }

        #endregion

        #region Spawner
        if (bounced == true)
        {
            if (elaspedTime >= currentInt)
            {
                currentInt = Random.Range(spawnInt[0] + Time.deltaTime * 0.01f, spawnInt[1]);

                elaspedTime = 0;

                Instantiate(bubble, spawnPos, Quaternion.identity);
            }
            else if (elaspedTime >= objTimer)
            {
                objTimer = Random.Range(objInt[0] - Time.deltaTime * 0.01f, objInt[1]);

                elaspedTime = 0;

                Instantiate(objects[Random.Range(0, objects.Length)], new Vector3(13, Random.Range(1, 5)), Quaternion.identity);
            }
        }
        #endregion

    }

    #region Col
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Bubble")
        {
            if (!bounced)

            {
                StartCoroutine(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameScore>().AddScore());
            }

            bounced = true;

            mus.PlayOneShot(audioClip[0]);

            Pop(transform.position);

            //Adding force when hitting bubble
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpBoost;

            Destroy(collision.gameObject);

        }
    }

    private void OnTriggerEnter2D(Collider2D Collider)
    {
        if (Collider.gameObject.tag == "Apple")
        {
            GameScore.score += 5;

            mus.PlayOneShot(audioClip[1]);
            Destroy(Collider.gameObject);
        }
        else if (Collider.gameObject.tag == "Cherry")
        {
            GameScore.score += 10;

            mus.PlayOneShot(audioClip[1]);
            Destroy(Collider.gameObject);
        }
        else if (Collider.gameObject.tag == "Banana")
        {
            GameScore.score += 15;

            mus.PlayOneShot(audioClip[1]);
            Destroy(Collider.gameObject);
        }
        else if (Collider.gameObject.tag == "Bomb")
        {
            mus.PlayOneShot(audioClip[2]);
            Death();
        }
    }
    #endregion

    //On Death Function-
    public void Death()
    {
        mus.clip = audioClip[3];
        GameObject.FindGameObjectWithTag("GameController").GetComponent<AudioSource>().Stop();
        mus.Play();

        gameObject.GetComponent<PolygonCollider2D>().enabled = false;

        oldsScore.SetActive(false);
        input.SetActive(true);

        bounced = false;

        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameScore>().isDead = true;

        dead = true;
    }

    //Creating Particle System
    public void Pop(Vector3 position)
    {
        Instantiate(bubblePop, position, Quaternion.identity);
    }

    //Menu Button
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("Menu");
    }

    //Retry Button
    public void Retry()
    {
        SceneManager.LoadSceneAsync("Game");
    }
}
