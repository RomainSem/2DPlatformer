using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    #region Expose

    [SerializeField]
    private float _moveSpeed;

    [SerializeField]
    private float _jumpForce = 300f;

    [SerializeField]
    private int _maxJump = 2;

    [SerializeField]
    private float _fallMultiplier = 3f;

    [Header("Related GameObjects")]
    [SerializeField]
    private GameObject _graphics;

    [SerializeField]
    GameObject _weapons;

    [SerializeField]
    [Range(0, 2f)]
    float _radius;

    [SerializeField]
    [Range(-5, 5f)]
    float _offsetYGroundChecker;

    [SerializeField]
    LayerMask _layer;


    #endregion

    #region Unity Lyfecycle
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = _graphics.GetComponent<Animator>();
    }
    void Start()
    {

    }

    void Update()
    {
        ReloadScene();

        // Récupération des boutons pour le déplacement
        _direction.x = Input.GetAxisRaw("Horizontal") * _moveSpeed;

        // Récupération des boutons pour le saut
        if (Input.GetButtonDown("Jump") && _nbJump < _maxJump)
        {
            _isJumping = true;
        }

        GroundCheck();

        ActivateAnims();

    }

    private void FixedUpdate()
    {
        // Appliquer une gravité permanente
        _direction.y = _rigidbody2D.velocity.y;

        // Application de la force pour le déplacement
        _rigidbody2D.velocity = _direction;

        // Application de la force pour le saut
        if (_isJumping == true && _nbJump < _maxJump)
        {
            _nbJump++;
            //Vector2 jumpVector = new Vector2(_direction.x, _direction.y = _jumpForce);
            //_rigidbody2D.AddForce(jumpVector);
            _isJumping = false;

            _direction.y = _jumpForce;
            _rigidbody2D.velocity = _direction * Time.fixedDeltaTime;
        }

        TurnCharacter();

        // Changement de la gravité quand le player retombe pour qu'il retombe plus vite
        if (_rigidbody2D.velocity.y < -0.1f)
        {
            _rigidbody2D.gravityScale = _fallMultiplier;
        }
        else
        {
            _rigidbody2D.gravityScale = 1f;
        }


    }
    #endregion

    #region Methods


    private void ActivateAnims()
    {
        _animator.SetFloat("moveSpeedX", Mathf.Abs(_direction.x));
        _animator.SetFloat("moveSpeedY", _rigidbody2D.velocity.y);

        if (Input.GetAxisRaw("Fire1") == 1)
        {
            _weapons.GetComponent<Collider2D>().enabled = true;
            _animator.SetBool("attackNormal", true);
        }
        else
        {
            _weapons.GetComponent<Collider2D>().enabled = false;
            _animator.SetBool("attackNormal", false);
        }
    }
    private void ReloadScene()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 positionTransformOffset = new Vector3(transform.position.x, transform.position.y - _offsetYGroundChecker, transform.position.z);
        Gizmos.DrawWireSphere(positionTransformOffset, _radius);
    }

    //private void DoMove()
    //{
    //    Vector2 velocity = _direction * _moveSpeed;
    //    velocity.y = _rigidbody2D.velocity.y;
    //    _rigidbody2D.velocity = velocity;
    //}

    private void TurnCharacter()
    {
        // Tourner le personnage dans la bonne direction
        if (_direction.x < 0f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (_direction.x > 0f)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        //if (_direction.x < 0.1f || _direction.x > 0.1f)
        //{
        //    transform.right = new Vector2(_direction.x, 0);
        //}
    }

    private void GroundCheck()
    {
        Vector3 positionTransformOffset = new Vector3(transform.position.x, transform.position.y - _offsetYGroundChecker, transform.position.z);
        // GroundChecker
        Collider2D floorCollider = Physics2D.OverlapCircle(positionTransformOffset, _radius, _layer);

        if (floorCollider != null)
        {
            // Si le player touche une plateforme mobile, il devient enfant de la plateforme pour ne pas tomber
            if (floorCollider.CompareTag("Platform"))
            {
                transform.SetParent(floorCollider.transform);
            }
            // _animator.SetTrigger("grounded");
            _nbJump = 0;
        }
        else
        {
            transform.SetParent(null);
        }

    }
   

    #endregion

    #region Private & Protected

    private Rigidbody2D _rigidbody2D;
    private Vector2 _direction;
    private bool _isJumping;
    private int _nbJump;
    private Animator _animator;

    #endregion
}
