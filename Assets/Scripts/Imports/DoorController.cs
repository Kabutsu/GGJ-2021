using UnityEngine;
using Assets.Scripts.Helpers;

public class DoorController : MonoBehaviour
{
	[SerializeField]
	private const string managerName = GameTag.Door;

	private GameManager gameManager;

    private void Start()
    {
		gameManager = GameObject.FindGameObjectWithTag(GameTag.GameController).GetComponent<GameManager>();
		gameManager.Register(managerName, gameObject);
    }

	public void Open()
    {
		gameObject.GetComponent<Animation>().Play("open");
    }
}