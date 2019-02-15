using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : CharacterBase
{
    private CharacterBase _grabber;

    //_grabbed acts as a lock on grabbing the goblin. Any time the goblin is grabbed or gets away this lock must be taken
    private bool _grabbed;

    public bool IsGrabbed { get { return _grabbed; } }

    private GoblinManager _manager;

    protected override void CharacterStart()
    {
        _grabbed = false;
        _grabber = null;
        GrabEnabled = false;

        new Timer(500, Kill);
    }

    protected override void CharacterUpdate()
    {
        if (IsGrabbed)
        {
            transform.position = _grabber.transform.position;
        }
    }

    public void SetManager(GoblinManager manager)
    {
        _manager = manager;
    }

    protected override void OnDeath()
    {
        _manager.Goblin = null;
    }

    public void GetGrabbed(CharacterBase grabber)
    {
        _grabber = grabber;
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), _grabber.GetComponent<Collider2D>(), true);
        Rigidbody2D goblinBody = GetComponent<Rigidbody2D>();
        goblinBody.gravityScale = 0;
        MovementEnabled = false;
        _grabbed = true;
    }

    public void Escape()
    {
        Escape(new Vector2(10, 100));
    }

    public void Escape(Vector2 escapeForce)
    {
        Rigidbody2D goblinBody = GetComponent<Rigidbody2D>();
        goblinBody.gravityScale = 1;
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), _grabber.GetComponent<Collider2D>(), false);
        _grabber = null;
        MovementEnabled = true;
        _grabbed = false;
        new Timer(1, () => goblinBody.AddForce(escapeForce));
    }
}
