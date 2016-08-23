using UnityEngine;
using System.Collections;

public class DummyStats : MonoBehaviour {

    public int health = 10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider C)
    {
        if (C.tag == "Bullet")
        {
            health -= 1;
            Debug.Log(health);
        }
    }
   

}
