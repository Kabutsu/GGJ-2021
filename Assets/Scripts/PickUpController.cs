using UnityEngine;
using Assets.Scripts.Helpers;

public enum PickUpType
{
    Key = 1
}

public class PickUpController : MonoBehaviour
{
    public PickUpType PickUpType;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag(GameTag.GameController).GetComponent<GameManager>();
        gameManager.AddWorldItem(PickUpType.GetName());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameTag.IsPlayer(other.gameObject))
        {
            other.gameObject.GetComponent<PlayerManager>().PickUp(PickUpType);
            Destroy(gameObject);
        }
    }
}
