using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinManager : MonoBehaviour
{
    public GameObject _goblinGameObject;

    public Goblin _goblin;

    private uint _goblinSpawnDelay = 500;


	// Use this for initialization
	void Start ()
    {
        _goblin = CreateGoblin();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(_goblin == null)
        {
            _goblinSpawnDelay--;
            if(_goblinSpawnDelay == 0)
            {
                _goblin = CreateGoblin();
                _goblinSpawnDelay = 500;
            }
        }
	}

    public Goblin CreateGoblin()
    {
        var characterGameObject = GameObject.Instantiate(_goblinGameObject);
        var gob = characterGameObject.GetComponent<Goblin>();
        gob.Init(new GoblinInput());
        return gob;
    }
}
