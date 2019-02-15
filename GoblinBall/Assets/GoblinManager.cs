using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinManager : MonoBehaviour
{
    public GameObject _goblinGameObject;

    public Goblin Goblin;

    private int _goblinSpawnTime = 100;
    private Timer _spawnTimer = null;

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Goblin == null && _spawnTimer == null)
        {
            _spawnTimer = new Timer(_goblinSpawnTime, CreateGoblin);
        }
	}

    public void CreateGoblin()
    {
        var goblinGameObject = GameObject.Instantiate(_goblinGameObject);
        var gob = goblinGameObject.GetComponent<Goblin>();
        gob.Init(new GoblinInput());
        gob.SetManager(this);
        Goblin = gob;
        _spawnTimer = null;
    }
}
