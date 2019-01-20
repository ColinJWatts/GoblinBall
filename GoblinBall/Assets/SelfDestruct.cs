using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public uint Countdown = 750; 
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Countdown--;
        if(Countdown == 0)
        {
            Destroy(this.transform.gameObject);
        }
	}
}
