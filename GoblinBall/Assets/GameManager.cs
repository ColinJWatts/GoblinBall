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

    public Dictionary<int, Timer> TimerList;
    public List<int> ToRemove;

    private int _timerId = 0;

    void Awake()
    {
        TimerList = new Dictionary<int, Timer>();
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
        List<int> keys = new List<int>(TimerList.Keys);
        lock (TimerList)
        {
            foreach(int key in keys)
            {
                if (TimerList.ContainsKey(key))
                {
                    try
                    {
                        TimerList[key].DoUpdate();
                    }
                    catch
                    {
                        ToRemove.Add(key);
                    }
                }
            }
            foreach(int i in ToRemove)
            {
                if (TimerList.ContainsKey(i))
                {
                    Debug.Log("Removing timer " + i.ToString());
                    
                    TimerList.Remove(i);
                }
            }
            ToRemove.Clear();
        }
	}

    void CreateGoblinManager()
    {
        GoblinManager = GameObject.Instantiate(_goblinManagerGameObject).GetComponent<GoblinManager>();
    }

    public int GetNewTimerId()
    {
        _timerId++;
        return _timerId;
    }
}
