using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Player class will hold metadata about the player, load saved player configurations and act as glue code for characters and menu inputs
public class Player : MonoBehaviour
{
    public GameObject _characterPrefab;

    private IInputInterface _input;
    private CharacterBase _character;

    public Player()
    {
        _input = new BasicInput();
    }

    public void CreateCharacter()
    {
        var characterGameObject = GameObject.Instantiate(_characterPrefab);
        _character = characterGameObject.GetComponent<CharacterBase>();
        _character.Init(_input);
    }


}
