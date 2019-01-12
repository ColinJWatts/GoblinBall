using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject _playerGameObject;
    public List<Player> Players;
	// Use this for initialization
	void Start ()
    {
        var defaultPlayer = GameObject.Instantiate(_playerGameObject);
        Players.Add(defaultPlayer.GetComponent<Player>());

        StartGame();
	}
	
    public void StartGame()
    {
        foreach(Player p in Players)
        {
            p.CreateCharacter();
        }
    }

	// Update is called once per frame
	void Update ()
    {
		
	}
}
