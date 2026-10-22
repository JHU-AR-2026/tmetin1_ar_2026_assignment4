using UnityEngine;
public class GoalZone : MonoBehaviour
{
    void Start()
    {
        // Transparency is required for the goal, so create this material at runtime.
        var material = new Material(Shader.Find("Standard"));
        material.color = new Color(0.4f, 0.7f, 1f, 0.25f);
        material.SetFloat("_Mode", 3);
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.EnableKeyword("_ALPHABLEND_ON");
        material.renderQueue = 3000;
        GetComponent<Renderer>().material = material;
    }
    void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player) player.game.Win();
    }
}
