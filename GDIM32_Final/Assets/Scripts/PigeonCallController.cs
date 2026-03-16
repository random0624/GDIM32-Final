using UnityEngine;

public class PigeonCallController : MonoBehaviour
{
    private void Start()
    {
        if (GameController.Instance != null && GameController.Instance.Player != null)
            GameController.Instance.Player.OnPigeonCallRequested += OnPigeonCallRequested;
    }

    private void OnDestroy()
    {
        if (GameController.Instance != null && GameController.Instance.Player != null)
            GameController.Instance.Player.OnPigeonCallRequested -= OnPigeonCallRequested;
    }

    private void OnPigeonCallRequested()
    {
        if (GameController.Instance?.CurrentPigeon == null)
        {
            Debug.Log("No pigeon found");
            return;
        }

        Pigeon pigeon = GameController.Instance.CurrentPigeon;
        if (pigeon.gameObject.activeSelf)
            pigeon.Disable();
        else
            pigeon.Enable();
    }
}

