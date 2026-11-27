using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0), 50 * Time.deltaTime);
    }
}
