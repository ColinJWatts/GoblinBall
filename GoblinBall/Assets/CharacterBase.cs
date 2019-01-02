using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public Rigidbody2D _rb;
    public float _moveForce = 5;
    public float _jumpForce = 500;
    public Transform _upperLeft;
    public Transform _lowerRight;

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
        Init(new BasicInput()); //This line is a test, should be initiallized by the player object later
    }
	
    void FixedUpdate ()
    {
        if (_initiallized)
        {
            //state checks

            //movement
            UpdateMove();

            //animation

            //actions
        }
        else
        {
            Debug.Log("CharacterController not initiallized");
        }
    }

    void UpdateMove()
    {
        var hAxis = _input.GetHorizontal();
        var jump = _input.GetJump(); 
        _rb.AddForce(hAxis * Vector2.right * _moveForce);

        if (jump && Physics2D.OverlapArea(_upperLeft.position, _lowerRight.position, LayerMask.GetMask("Terrain")) != null)
        {
            Debug.Log("Jump!");
            Jump();
        }
    }

    void Jump()
    {
        _rb.AddForce(Vector2.up * _jumpForce);
    }
}
