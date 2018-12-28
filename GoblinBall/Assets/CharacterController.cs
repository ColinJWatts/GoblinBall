using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Rigidbody2D _rb;
    public float _moveForce = 5;
    public float _jumpForce = 500;
    public Transform _upperLeft;
    public Transform _lowerRight;

    // Use this for initialization
    void Start ()
    {
        
    }
	
    void FixedUpdate ()
    {
        //state checks
        //movement
        UpdateMove();
        

        //animation
        //actions
    }

    void UpdateMove()
    {
        var hAxis = Input.GetAxis("Horizontal"); // replace this with input interface
        var vAxis = Input.GetAxis("Jump"); // replace this with input interface
        _rb.AddForce(hAxis * Vector2.right * _moveForce);

        if (vAxis != 0 && Physics2D.OverlapArea(_upperLeft.position, _lowerRight.position, LayerMask.GetMask("Terrain")) != null)
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
