using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    //public things
    public Rigidbody2D _rb;
    public float _moveForce = 5;
    public float _jumpForce = 500;
    public Transform _upperLeft;
    public Transform _lowerRight;

    //Relevent game stats
    public uint _health = 500;

    //private things
    private SpriteRenderer _spriteRenderer;

    private bool _initiallized = false;
    public bool IsInitiallized { get { return _initiallized; } }
    
    private IInputInterface _input;

    public void Init(IInputInterface inputInterface)
    {
        _input = inputInterface;
        _initiallized = true;
    }

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
    }
	
    void FixedUpdate ()
    {
        if (_initiallized)
        {
            //state checks
            CheckHealth();

            //movement
            UpdateMove();

            //animation

            //actions
            CharacterUpdate();
        }
        else
        {
            Debug.Log("CharacterController not initiallized");
        }
    }

    public virtual void CharacterUpdate() { }

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
        if(_health == 0)
        {
            Destroy(transform.gameObject);
        }
    }


}
