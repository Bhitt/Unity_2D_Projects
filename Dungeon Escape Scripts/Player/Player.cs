using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageable
{
    public int diamonds;

    private Rigidbody2D _rigid;
    [SerializeField] private float _jumpForce = 6.0f;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] private float _speed = 2.5f;
    private bool _resetJump = false;
    private bool _grounded = false;
    private bool _hasBoots = false;
    private bool _hasSword = false;
    private bool _hasKey = false;
    private bool _isDead = false;

    private PlayerAnimation _playerAnim;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;

    public int Health {get;set;}

    // Start is called before the first frame update
    void Start()
    {
        /* set handle of rigidbody */
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        Health = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDead) return;
        Movement();

        if(CrossPlatformInputManager.GetButtonDown("A_Button") && isGrounded())
        {
            _playerAnim.Attack();
        }
    }

    void Movement()
    {
        _grounded = isGrounded();

        /* works on android or pc */
        float move = CrossPlatformInputManager.GetAxis("Horizontal");

        /*sprite flipping */
        Flip(move);

        if (CrossPlatformInputManager.GetButtonDown("B_Button") && isGrounded())
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());
            _playerAnim.Jump(true);
        }

        _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);

        /*check for animation */
        _playerAnim.Move(move);
    }

    private void Flip(float move)
    {
        if (move > 0)
        {
            /* face sprite to the right */
            _playerSprite.flipX = false;
            /* face weapon effect to the right */ 
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;
            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
        else if (move < 0)
        {
            /*face sprite to the left */
            _playerSprite.flipX = true;
            /* face weapon effect to the left */
            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;
            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
    }

    bool isGrounded()
    {
        /* raycast downwards 
            1 << 8 chooses the 8th layer for the raycast to detect
        */
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1f, 1 << 8);

        if(hitInfo.collider != null)
        {
            if (!_resetJump)
            {
                _playerAnim.Jump(false);
                return true;
            }
        }
        return false;
    }

    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

    public void Damage()
    {
        if (Health < 1)
        {
            return;
        }

        Health--;
        UIManager.Instance.UpdateLives(Health);

        if (Health < 1)
        {
            _isDead = true;
            _playerAnim.Death();
            Die die = GetComponent<Die>();
            if(die != null)
            {
                die.LoadMainMenu(); 
            }
        }
    }

    public void ForceDeath()
    {
        _isDead = true;
        _playerAnim.Death();
        Die die = GetComponent<Die>();
        if (die != null)
        {
            die.LoadMainMenu();
        }
    }

    public void AddGems(int amount)
    {
        diamonds += amount;
        UIManager.Instance.UpdateGemCount(diamonds);
    }

    public void PurchaseBoots()
    {
        _hasBoots = true;
        _jumpForce = 12f;
    }

    public bool HasBoots()
    {
        return _hasBoots;
    }


    public void PurchaseSword()
    {
        _hasSword = true;
        _speed = 8f;
    }

    public bool HasSword()
    {
        return _hasSword;
    }

    public bool HasKey()
    {
        return _hasKey;
    }
}
