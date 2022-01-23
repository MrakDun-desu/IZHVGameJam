using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("Dependencies")]
    [SerializeField] private AudioClip _deathSound;
    
    [Header("Movement")]
    [SerializeField] private float _movementSpeed = 10;
    [SerializeField] private float _jumpForce = 1600;
    [SerializeField] private float _maxJumpTime = .2f;

    private BoxCollider2D _topCollider;
    private float _movement;
    private bool _jumping;
    private float _jumpTime;
    private Rigidbody2D _rigidbody;
    private GameController _gameController;
    private Animator _animator;
    private Coroutine _rain = null;
    private SFXPlayer _sfxPlayer;

    private void Awake()
    {
        _gameController = FindObjectOfType<GameController>();
        _sfxPlayer = FindObjectOfType<SFXPlayer>();
        _topCollider = GetComponent<BoxCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        HandleJump();
        HandleMovement();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.gameObject.layer != LayerMask.NameToLayer("Heat")) return;
        
        _animator.SetBool("Dead", true);
        _sfxPlayer.PlayClip(_deathSound);
        _gameController.HandleDeath();
    }

    private void HandleMovement() => _rigidbody.velocity = new Vector2(_movement * _movementSpeed, _rigidbody.velocity.y);

    private void HandleJump()
    {
        _animator.SetBool("Jumping", _jumping);
        if (_jumping && _jumpTime < _maxJumpTime)
        {
            _jumpTime += Time.fixedDeltaTime;
            _rigidbody.velocity = Vector2.down * _jumpForce;
        }
        else
        {
            _jumping = false;
            _jumpTime = 0;
        }
    }

    private bool OnGround() => _topCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));

    public void Revive() => _animator.SetBool("Dead", false);

    // input handlers
    public void OnMovement(InputAction.CallbackContext ctx) => _movement = ctx.ReadValue<float>();

    public void OnJump(InputAction.CallbackContext ctx) => _jumping = ctx.performed && OnGround();

    public void OnRain(InputAction.CallbackContext ctx) => Debug.Log("It would rain now.");

    public void OnPause(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) _gameController.TogglePause();
    }
}
