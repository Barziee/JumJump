
using UnityEngine;

public class JumpTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerManager.Instance.PlayerMovement.IsGround = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerManager.Instance.PlayerMovement.IsGround = false;
    }
}
