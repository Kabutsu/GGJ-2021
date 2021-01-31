using Assets.Scripts.Helpers;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float Health = 50f;

    private Text healthText;

    void Start()
    {
        healthText = FindObjectsOfType<Text>().FirstOrDefault(x => x.gameObject.name == UserInterface.PlayerHealth);
        healthText.text = Health.ToString();
    }

    void Update()
    {
        
    }

    public void WasHit(float Damage)
    {
        Health -= Damage;
        healthText.text = Health.ToString();
    }
}
