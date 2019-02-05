using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameObject _playerGameObject;
    public List<Player> Players;

    public GameObject _goblinManagerGameObject;
    public GoblinManager GoblinManager;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    // Use this for initialization
    void Start ()
    {
        var defaultPlayer = GameObject.Instantiate(_playerGameObject);
        Players.Add(defaultPlayer.GetComponent<Player>());

        StartGame();
	}
	
    public void StartGame()
    {
        CreateGoblinManager();
        foreach(Player p in Players)
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
        GoblinManager = GameObject.Instantiate(_goblinManagerGameObject).GetComponent<GoblinManager>();

    }
}
