using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public uint countdown = 500;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        countdown--;
        if(countdown <= 0)
        {
            Destroy(this.transform.gameObject);
        }
	}
}
