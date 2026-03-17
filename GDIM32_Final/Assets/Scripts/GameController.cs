using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public PlayerMovement Player {get; private set;}

    public Door CurrentDoor {get; private set;}
    public Pigeon CurrentPigeon { get; private set; }

    public PuzzleLogic CurrentPuzzle { get; private set;}

    [SerializeField] GameObject _gameOverUI;

    public delegate void GameOver();
    public event GameOver _gameOver;   
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
        if (doorObj == null)
        {
            Debug.LogError("GameController: No GameObject with tag 'Door' found in the scene.");
            return;
        }
        CurrentDoor = doorObj.GetComponent<Door>();

        GameObject puzObj = GameObject.FindGameObjectWithTag("Puzzle");
        if (doorObj == null)
        {
            Debug.LogError("GameController: No GameObject with tag 'Door' found in the scene.");
            return;
        }
        CurrentPuzzle = puzObj.GetComponent<PuzzleLogic>();

        Pigeon pigeon = FindObjectOfType<Pigeon>(true);
        if (pigeon != null)
            CurrentPigeon = pigeon;
    }

    void OnEnable()
    {
       // PlayerMovement player = FindObjectOfType<PlayerMovement>();
        Player.OnLoseLife += CheckGameOver;
    }

    void OnDisable()
    {
       // PlayerMovement player = FindObjectOfType<PlayerMovement>();
        Player.OnLoseLife -= CheckGameOver;
    }
    private void CheckGameOver()
    {
        if (Player._lifeCount <= 0)
        {

            Debug.Log("Game Over");
            _gameOver?.Invoke();
            _gameOverUI.SetActive(true);
            Time.timeScale = 0.0f;
        }

        
       
    }
}
