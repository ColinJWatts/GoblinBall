using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinManager : MonoBehaviour
{
    public GameObject _goblinGameObject;

    private Goblin _goblin;
	// Use this for initialization
	void Start ()
    {
        var characterGameObject = GameObject.Instantiate(_goblinGameObject);
        _goblin = characterGameObject.GetComponent<Goblin>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
