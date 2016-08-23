using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }

    void OnTriggerEnter(Collider Bul)
    {


        if (Bul.tag == "Ground" || Bul.tag == "Terrain" || Bul.tag == "Enemy")
        {
            Destroy(gameObject);
        }


    }

}