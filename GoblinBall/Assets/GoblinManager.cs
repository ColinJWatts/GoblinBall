using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinManager : MonoBehaviour
{
    public GameObject _goblinGameObject;

    public Goblin _goblin;

    private uint _goblinSpawnDelay = 100;
    private uint _spawnCounter;

	// Use this for initialization
	void Start ()
    {
        _spawnCounter = 1;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(_goblin == null)
        {
            _spawnCounter--;
            if(_spawnCounter == 0)
            {
                _goblin = CreateGoblin();
                _spawnCounter = _goblinSpawnDelay;
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
}
