using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public PlayerMovement Player {get; private set;}


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null)
        {
            Debug.LogError("GameController: No GameObject with tag 'Player' found in the scene. Assign the Player tag to your player.");
            return;
        }
        Player = playerObj.GetComponent<PlayerMovement>();
    }
}
