using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public PlayerMovement Player {get; private set;}

    public Door CurrentDoor {get; private set;}

    [SerializeField] GameObject _gameOverUI;
 
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

        GameObject doorObj = GameObject.FindGameObjectWithTag("Door");
        if (playerObj == null)
        {
            Debug.LogError("GameController: No GameObject with tag 'Player' found in the scene. Assign the Player tag to your player.");
            return;
        }
        CurrentDoor = playerObj.GetComponent<Door>();
    }

    void OnEnable()
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        player.OnLoseLife += CheckGameOver;
    }

    void OnDisable()
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        player.OnLoseLife -= CheckGameOver;
    }
    private void CheckGameOver()
    {
        if (Player._lifeCount <= 0)
        {
            Debug.Log("Game Over");
            _gameOverUI.SetActive(true);
            Time.timeScale = 0.0f;
        }

        
       
    }
}
