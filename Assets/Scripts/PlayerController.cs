using System;
using System.Collections.Generic;
using System.Numerics;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.U2D.Animation;
using Vector2 = UnityEngine.Vector2;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")] 
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _moveSpeedCap = 10f;
    [SerializeField] private float _groundFriction = 0.5f;
    [SerializeField] private float _airFriction = 0.95f;

    [Header("Jump")]
    [SerializeField] private float _jumpForce = 10f;

    [Header("Ground Check")] [SerializeField]
    private float _groundCheckDistance;

    [SerializeField] private Vector2 _groundCheckSize;
    [SerializeField] private LayerMask _groundLayer;
    
    [Header("Animator")]
    [SerializeField] private Animator _leftAnimators;
    [SerializeField] private Animator _rightAnimators;
    [SerializeField] private List<SpriteSkin> _leftSpriteSkins;
    [SerializeField] private List<SpriteSkin> _rightSpriteSkins;

    [Header("Animator Param")] 
    [SerializeField, AnimatorParam("_leftAnimators")] private string _grounded;
    [SerializeField, AnimatorParam("_leftAnimators")] private string _yVelocity;
    [SerializeField, AnimatorParam("_leftAnimators")] private string _isMoving;
    

    [Header("Read Only")] [ReadOnly] public bool isGrounded;

    private Vector2 _moveInput;
    private float _lastGroundTouch;
    private Rigidbody2D _rb;
    private int _currentSpriteDirection = 1;
    private List<Animator> _animators = new List<Animator>();

    public void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animators.Add(_leftAnimators);
        _animators.Add(_rightAnimators);
    }

    public void FixedUpdate()
    {
        isGrounded = IsGrounded();

        _rb.linearVelocityX = _moveInput.x * _moveSpeed;

        if (_currentSpriteDirection != (int)_moveInput.x && (int)_moveInput.x != 0)
        {
            print("swap sides");
            foreach (SpriteSkin skin in (_currentSpriteDirection > 0 ? _rightSpriteSkins : _leftSpriteSkins))
            {
                skin.gameObject.SetActive(false);
            }
            foreach (SpriteSkin skin in (_moveInput.x > 0 ? _rightSpriteSkins : _leftSpriteSkins))
            {
                skin.gameObject.SetActive(true);
            }

            _currentSpriteDirection = (int)_moveInput.x;
        }
        /*
        if (_moveInput.x != 0)
        {
            if (_rb.linearVelocityX + _moveInput.x * _moveSpeed > _moveSpeedCap)
            {

            }
        }*/ 
        
        
        //animator setup
        foreach (Animator animator in _animators)
        {
            animator.SetBool(_isMoving, Mathf.Abs(_moveInput.x) > 0);
            animator.SetBool(_grounded, isGrounded);
            animator.SetFloat(_yVelocity, _rb.linearVelocityY);
        }
    }





public void OnMove(InputAction.CallbackContext inputAction)
    {
        _moveInput = inputAction.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext inputAction)
    {
        
        Jump();
    }

    public void Jump(float jumpMultiplier = 1)
    {
        if(isGrounded)
        {
            _rb.AddForce(_jumpForce * Vector2.up, ForceMode2D.Impulse);
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, _groundCheckSize, 0,
            Vector2.down, _groundCheckDistance,_groundLayer);
        
        return hit;
    }
}
