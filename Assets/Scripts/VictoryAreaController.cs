using UnityEngine;
using Assets.Scripts.Helpers;

public class VictoryAreaController : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag(GameTag.GameController).GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameTag.IsPlayer(other.gameObject))
        {
            gameManager.Victory();
        }
    }
}
