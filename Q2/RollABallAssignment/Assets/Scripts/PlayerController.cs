using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public class PlayerController : MonoBehaviour
{
    public GameManager game;
    public float speed = 6f;
    public float poweredSpeed = 10f;
    public float jumpSpeed = 7.5f;
    public bool automatedInput;
    public Rigidbody Body { get; private set; }
    public bool Grounded { get; private set; }
    public float CurrentSpeed => game.Powered ? poweredSpeed : speed;
    Vector2 movement;
    bool jumpQueued;
    Vector3 spawn;
    Collider sphere;

    void Awake()
    {
        Body = GetComponent<Rigidbody>();
        sphere = GetComponent<Collider>();
        spawn = transform.position;
        Body.useGravity = false;
        Body.isKinematic = true;
    }
    public void Begin() { Body.isKinematic = false; Body.useGravity = true; }
    void Update()
    {
        if (automatedInput) return;
        if (Input.GetKeyDown(KeyCode.R)) GameManager.Restart();
        SetMovement(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
        if (Input.GetButtonDown("Jump")) RequestJump();
    }
    public void SetMovement(Vector2 value) { movement = Vector2.ClampMagnitude(value, 1); }
    public void RequestJump() { if (game.Started && !game.Won) jumpQueued = true; }
    void FixedUpdate()
    {
        if (!game.Started || game.Won) return;
        // Ignore triggers and require an upward-facing surface: walls cannot grant extra jumps.
        Grounded = false;
        foreach (var hit in Physics.SphereCastAll(transform.position, 0.43f, Vector3.down, 0.18f, ~0, QueryTriggerInteraction.Ignore))
            if (hit.collider != sphere && hit.normal.y > 0.65f) Grounded = true;
        var velocity = Body.linearVelocity;
        velocity.x = movement.x * CurrentSpeed;
        velocity.z = movement.y * CurrentSpeed;
        if (jumpQueued && Grounded) velocity.y = jumpSpeed;
        jumpQueued = false;
        Body.linearVelocity = velocity;
        if (transform.position.y < -8) Respawn();
    }
    public void Respawn()
    {
        Body.position = spawn;
        Body.linearVelocity = Vector3.zero;
        Body.angularVelocity = Vector3.zero;
        jumpQueued = false;
    }
    public void PowerUp() { GetComponent<Renderer>().material.color = new Color(1f, 0.25f, 0.8f); }
}
