using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scrolling : MonoBehaviour
{
    public float scrollSpeed;
    public GameObject[] objects;

    private Vector3 randomMove, constMove;

    void Update()
    {
        if (gameObject.tag == "Cloud" || gameObject.tag == "Hills")
        {
            RandomMove();
        }
        else if (gameObject.tag == "Stars")
        {
            ConstantMove();
        }

        // set the conditions on when to respawn //
        if (gameObject.tag == "Cloud" && transform.position.x < -15)
        {
            Instantiate(objects[Random.Range(0, objects.Length)], new Vector3(15, Random.Range(0, 4)), Quaternion.identity);
            Destroy(gameObject);
        }
        else if (gameObject.tag == "Hills" && transform.position.x < -18)
        {
            Instantiate(objects[Random.Range(0, objects.Length)], new Vector3(18, -6), Quaternion.identity);
            Destroy(gameObject);
        }
        else if (gameObject.tag == "Stars" && transform.position.x < -18)
        {
            Instantiate(objects[Random.Range(0, objects.Length)], new Vector3(18, 0), Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void RandomMove()
    {
        Vector3 randomMove = new Vector3(1, 0) * -scrollSpeed * Time.deltaTime * Random.Range(1, 5);
        transform.position += randomMove;
    }

    void ConstantMove()
    {
        Vector3 constMove = new Vector3(1, 0) * -scrollSpeed * Time.deltaTime;
        transform.position += constMove;
    }
}

