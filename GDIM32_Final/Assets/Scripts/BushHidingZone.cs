using UnityEngine;

public class BushHidingZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameController.Instance?.Player != null)
            GameController.Instance.Player.SetHidden(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && GameController.Instance?.Player != null)
            GameController.Instance.Player.SetHidden(false);
    }
}
