using UnityEngine;

public class ShootController : MonoBehaviour
{
    public GameObject Bullet;
    public float RateOfFire = 2f;

    private float Cooldown;

    void Start()
    {
        Cooldown = 0f;
    }

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

        var eulerAngles = transform.rotation.eulerAngles;
        GameObject go = Instantiate(Bullet, transform.position, Quaternion.Euler(eulerAngles.x, eulerAngles.y - 90, eulerAngles.z));

        Destroy(go, go.GetComponent<BulletController>().Lifetime);
    }
}
