using UnityEngine;
public class Pickup : MonoBehaviour
{
    public bool isPowerUp;
    bool collected;
    void Update() { transform.Rotate(new Vector3(15, 40, 20) * Time.deltaTime); }
    void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (!player || !player.game.Started || player.game.Won || collected) return;
        collected = true;
        if (isPowerUp) player.game.ActivatePower();
        else { player.game.Collect(); gameObject.SetActive(false); }
    }
}
