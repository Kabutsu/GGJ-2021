using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float MovementSpeed = 1000f;
    public float Lifetime = 2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Lifetime -= Time.deltaTime;

        //if (Lifetime <= 0f) Destroy(gameObject);

        transform.position += transform.forward * Time.deltaTime * MovementSpeed;
    }
}
