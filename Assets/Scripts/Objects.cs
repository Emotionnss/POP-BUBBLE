using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{
    public float speed;

    void Update()
    {
        if (Player.bounced == true)
        {
            transform.position += new Vector3(-1, 0) * speed * Time.deltaTime;
            if(gameObject.name == "Starting Platform")
            {
                Destroy(gameObject);
            }
        }
        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
