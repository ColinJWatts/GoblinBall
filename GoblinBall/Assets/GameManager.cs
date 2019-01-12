using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject _playerGameObject;
    public List<Player> _players;

    public GameObject _goblinManagerGameObject;
    private GoblinManager _goblinManager;
	// Use this for initialization
	void Start ()
    {
        var defaultPlayer = GameObject.Instantiate(_playerGameObject);
        _players.Add(defaultPlayer.GetComponent<Player>());

        StartGame();
	}
	
    public void StartGame()
    {
        CreateGoblinManager();
        foreach(Player p in _players)
        {
            p.CreateCharacter();
        }
    }

	// Update is called once per frame
	void Update ()
    {
		
	}

    void CreateGoblinManager()
    {
        _goblinManager = GameObject.Instantiate(_goblinManagerGameObject).GetComponent<GoblinManager>();

    }
}
