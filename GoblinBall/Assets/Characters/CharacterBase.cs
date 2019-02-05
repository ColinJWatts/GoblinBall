using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    //public things
    public Rigidbody2D _rb;
    public float _moveForce = 5;
    public float _jumpForce = 500;
    public GameObject _bloodSplatter;
   
    public Transform UpperLeft { get { return _upperLeft; } }
    public Transform LowerRight { get { return _lowerRight; } }
    public bool IsInitiallized { get { return _initiallized; } }

    public bool MovementEnabled { get; set; }

    //Relevent game stats
    public int Health { get { return _health; } }

    //private things
    protected SpriteRenderer _spriteRenderer;
    private Transform _upperLeft;
    private Transform _lowerRight;
    private bool _initiallized = false;
    protected int _health;
    
    private IInputInterface _input;

    public void Init(IInputInterface inputInterface)
    {
        _health = 250;
        _input = inputInterface;
        _initiallized = true;
        MovementEnabled = true;
    }

    //TODO: Refactor
    void Start ()
    {
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

    void UpdateMove()
    {
        var hAxis = _input.GetHorizontal();
        var jump = _input.GetJump(); 
        _rb.AddForce(hAxis * Vector2.right * _moveForce);

        if (jump && Physics2D.OverlapArea(_upperLeft.position, _lowerRight.position, LayerMask.GetMask("Terrain")) != null)
        {
            Jump();
        }
    }

    void Jump()
    {
        _rb.AddForce(Vector2.up * _jumpForce);
    }

    void CheckHealth()
    {
        if(_health <= 0)
        {
            var bloodSplatter = GameObject.Instantiate(_bloodSplatter);
            bloodSplatter.transform.position = this.transform.position;
            Destroy(this.transform.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Contains("Goblin") && !GameManager.instance.GoblinManager.IsGrabbed)
        {
            GameManager.instance.GoblinManager.GrabGoblin(this);
        }
    }

}
