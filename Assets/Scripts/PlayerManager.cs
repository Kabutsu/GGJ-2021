using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Helpers;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public float MaxHealth = 50f;

    private Slider healthSlider;
    private Image healthSliderImage;
    private float health;
    private int keys = 0;

    void Start()
    {
        healthSlider = FindObjectsOfType<Slider>().FirstOrDefault(x => x.gameObject.name == UserInterface.PlayerHealth);

        healthSliderImage = healthSlider
            .GetComponentsInChildren<Image>()
            .Where(x => x.name.Contains("Fill"))
            .FirstOrDefault();

        health = MaxHealth;
        healthSlider.value = health / MaxHealth;
    }

    void Update()
    {
        
    }

    public void PickUp(PickUpType pickup)
    {
        switch(pickup)
        {
            case PickUpType.Key:
                keys++;
                break;
            default:
                break;
        }
    }

    public void WasHit(float Damage)
    {
        health -= Damage;

        StartCoroutine(ReduceHealth(healthSlider.value, health / MaxHealth));
    }

    public IEnumerator ReduceHealth(float from, float to)
    {
        for (float i = from; i >= to; i -= ((from - to) / 12f))
        {
            healthSlider.value = i;
            healthSliderImage.LerpColor3(Color.green, Color.yellow, Color.red, 0.5f, i);

            yield return null;
        }
    }
}
