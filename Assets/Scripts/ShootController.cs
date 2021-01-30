using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public GameObject Bullet;
    public float RateOfFire = 2f;

    private float Cooldown;

    // Start is called before the first frame update
    void Start()
    {
        Cooldown = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Cooldown -= Time.deltaTime;

        if (Input.GetButton("Fire1") && Cooldown <= 0f)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Cooldown = 1 / RateOfFire;

        //var bullet = Instantiate(Bullet, transform.position, transform.rotation);

        //var bulletController = bullet.GetComponent<BulletController>();
        //var bulletBody = bullet.GetComponent<Rigidbody>();

        //bulletBody.velocity = transform.TransformDirection(Vector3.up * bulletController.MovementSpeed);

        //
        var eulerAngles = transform.rotation.eulerAngles;
        GameObject go = Instantiate(Bullet, transform.position, Quaternion.Euler(eulerAngles.x, eulerAngles.y - 90, eulerAngles.z));
        Destroy(go, 3f);
        //
    }
}
