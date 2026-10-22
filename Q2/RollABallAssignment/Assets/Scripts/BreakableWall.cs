using UnityEngine;
public class BreakableWall : MonoBehaviour
{
    void OnCollisionEnter(Collision collision) { TryBreak(collision); }
    void OnCollisionStay(Collision collision) { TryBreak(collision); }
    void TryBreak(Collision collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player) player.game.BreakWall(gameObject);
    }
}
