using UnityEngine;
using System.Collections;

public class Shell : MonoBehaviour {

    private float lifeTime = 3;
    private float deathTime;

    public AudioClip ShellInGround;
    private AudioSource source;

    private float volLowRange = .3f;
    private float volHighRange = .8f;


    // Use this for initialization
    void Start () {

        source = GetComponent<AudioSource>();
        deathTime = Time.time + lifeTime;
        StartCoroutine("SelfDestruct");
	}

    IEnumerator SelfDestruct()
    {
        while (true)
        {
            yield return new WaitForSeconds(.2f);
            if(Time.time > deathTime)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision C)
    {


        if (C.gameObject.name == "Ground")
        {
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(ShellInGround, vol);
           // GetComponent<Rigidbody>().Sleep();
            
        }
        

    }

}
