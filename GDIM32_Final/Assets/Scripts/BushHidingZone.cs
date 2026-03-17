using UnityEngine;

public class BushHidingZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameController.Instance?.Player != null)
            GameController.Instance.Player.SetHidden(true);
            Debug.Log("Player is hidden in a bush");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && GameController.Instance?.Player != null)
            GameController.Instance.Player.SetHidden(false);
            Debug.Log("Player is out of a bush");
    }
}
