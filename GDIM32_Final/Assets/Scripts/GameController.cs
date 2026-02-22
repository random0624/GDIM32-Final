using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public PlayerMovement Player {get; private set;}

    public delegate void LionTrigger();
    public static event LionTrigger _lionTriggered;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        GameObject playerObj = GameObject.FindWithTag("Player");
        Player = playerObj.GetComponent<PlayerMovement>();
    }
}
