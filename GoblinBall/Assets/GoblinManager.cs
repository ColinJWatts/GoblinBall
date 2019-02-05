using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinManager : MonoBehaviour
{
    public GameObject _goblinGameObject;

    public Goblin Goblin;

    private uint _goblinSpawnDelay = 100;
    private uint _spawnCounter;
    private CharacterBase _grabber; 

    //_grabbed acts as a lock on grabbing the goblin. Any time the goblin is grabbed or gets away this lock must be taken
    private bool _grabbed;

    public bool IsGrabbed { get { return _grabbed; } }

	// Use this for initialization
	void Start ()
    {
        _spawnCounter = 1;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Goblin == null)
        {
            _spawnCounter--;
            if(_spawnCounter == 0)
            {
                Goblin = CreateGoblin();
                _grabbed = false;
                _grabber = null;
                _spawnCounter = _goblinSpawnDelay;
            }
        }
        else
        {
            if(IsGrabbed)
            {
                Goblin.transform.position = _grabber.transform.position;
            }
        }
	}

    public Goblin CreateGoblin()
    {
        var goblinGameObject = GameObject.Instantiate(_goblinGameObject);
        var gob = goblinGameObject.GetComponent<Goblin>();
        gob.Init(new GoblinInput());
        return gob;
    }

    //returns true if goblin is grabbed
    public void GrabGoblin(CharacterBase Grabber)
    {
        //Maybe set position?
        Goblin.transform.SetParent(Grabber.transform);
        _grabber = Grabber;
        Physics2D.IgnoreCollision(Goblin.GetComponent<Collider2D>(), _grabber.GetComponent<Collider2D>(),true);
        Rigidbody2D goblinBody = Goblin.GetComponent<Rigidbody2D>();
        goblinBody.Sleep();
        Goblin.MovementEnabled = false;
        _grabbed = true;
    }

    public void DropGoblin()
    {
        //Maybe add force?
        Rigidbody2D goblinBody = Goblin.GetComponent<Rigidbody2D>();
        goblinBody.WakeUp();
        Physics2D.IgnoreCollision(Goblin.GetComponent<Collider2D>(), _grabber.GetComponent<Collider2D>(), false);
        _grabber = null;
        Goblin.transform.SetParent(null);
        Goblin.MovementEnabled = true;
        _grabbed = false;
    }
}
