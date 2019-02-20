using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    //public things
    public Rigidbody2D _rb;
    public float _maxSpeed = 5;
    public float _jumpSpeed = 5;
    public GameObject _bloodSplatter;
   
    public Transform UpperLeft { get { return _upperLeft; } }
    public Transform LowerRight { get { return _lowerRight; } }
    public bool IsInitiallized { get { return _initiallized; } }

    public bool MovementEnabled { get; set; }
    public bool GrabEnabled { get; set; }

    //Relevent game stats
    public int Health { get { return _health; } }

    //private things
    protected SpriteRenderer _spriteRenderer;
    private Transform _upperLeft;
    private Transform _lowerRight;
    private bool _initiallized = false;
    private Rigidbody2D _body;
    
    private int _grabstrength = 100;
    private bool _hasGoblin = false;
    private Goblin _grabbedGoblin = null;
    protected int _health;
    
    private IInputInterface _input;

    public void Init(IInputInterface inputInterface)
    {
        _health = 250;
        _input = inputInterface;
        _initiallized = true;
        MovementEnabled = true;
        GrabEnabled = true;
       
    }

    //TODO: Refactor
    void Start ()
    {
        _body = this.GetComponent<Rigidbody2D>();

        GameObject upperLeft = new GameObject("upperLeft");
        upperLeft.transform.parent = this.transform;
        GameObject lowerRight = new GameObject("lowerRight");
        lowerRight.transform.parent = this.transform;

        _spriteRenderer = GetComponent<SpriteRenderer>();

        upperLeft.transform.position = new Vector3(this.transform.position.x - _spriteRenderer.bounds.extents.x, this.transform.position.y + _spriteRenderer.bounds.extents.y);
        lowerRight.transform.position = new Vector3(this.transform.position.x + _spriteRenderer.bounds.extents.x, this.transform.position.y - _spriteRenderer.bounds.extents.y);

        _upperLeft = upperLeft.transform;
        _lowerRight = lowerRight.transform;

        CharacterStart();
    }
	
    void FixedUpdate ()
    {
        if (_initiallized)
        {
            //state checks
            CheckHealth();

            //movement
            if (MovementEnabled)
            {
                UpdateMove();
            }
            //animation

            //actions
            CharacterUpdate();
        }
        else
        {
            Debug.Log("CharacterController not initiallized");
        }
    }

    protected virtual void CharacterUpdate() { }
    protected virtual void CharacterStart() { }
    protected virtual void OnDeath() { }

    void UpdateMove()
    {
        var hAxis = _input.GetHorizontal();
        var jump = _input.GetJump(); 
        _rb.velocity = new Vector2(hAxis * _maxSpeed, _rb.velocity.y);

        if (jump && Physics2D.OverlapArea(_upperLeft.position, _lowerRight.position, LayerMask.GetMask("Terrain")) != null)
        {
            Jump();
        }
    }

    void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpSpeed);
    }

    void CheckHealth()
    {
        if(_health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        var bloodSplatter = GameObject.Instantiate(_bloodSplatter);
        bloodSplatter.transform.position = this.transform.position;
        OnDeath();

        Destroy(this.transform.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(_input.GetGrab() && collision.gameObject.name.Contains("Goblin") && _grabbedGoblin == null && GrabEnabled)
        {
            _grabbedGoblin = collision.gameObject.GetComponent<Goblin>();
            if (!_grabbedGoblin.IsGrabbed)
            {
                _grabbedGoblin.GetGrabbed(this);
                _hasGoblin = true;
                GrabEnabled = false;
                new Timer(_grabstrength, DropGoblin);
            }
            else
            {
                _grabbedGoblin = null;
            }
        }
    }

    public void DropGoblin()
    {
        if (_hasGoblin && _grabbedGoblin != null)
        {
            _grabbedGoblin.Escape();
            _grabbedGoblin = null;
            _hasGoblin = false;
        }
        new Timer(100, ToggleGrabEnabled);
    }
    
    private void ToggleGrabEnabled()
    {
        GrabEnabled = !GrabEnabled;
    }

}
