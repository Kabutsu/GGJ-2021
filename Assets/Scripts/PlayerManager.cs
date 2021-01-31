using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float Health = 50f;
    public Text HealthText;

    void Start()
    {
        HealthText.text = Health.ToString();
    }

    void Update()
    {
        
    }

    public void WasHit(float Damage)
    {
        Health -= Damage;
        HealthText.text = Health.ToString();
    }
}
