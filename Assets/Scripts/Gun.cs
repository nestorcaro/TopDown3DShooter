using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour {

    public enum GunTypeMain {Revolver,Sword};       // Sword can be Close Combat and deflect projectiles so it can give a different playstyle
    public enum GunTypeSecondary {ShotGun,MachineGun};

    public GunTypeMain gunTypeMain;
    public GunTypeSecondary gunTypeSecondary;
    public int gunLevel;

    public AudioClip revolverSound;
    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.1f;

    // Components
    public Transform bulletSpawn;
    public Transform bulletEjection;
    public Rigidbody Shell;
    public Rigidbody Bullet;
    private LineRenderer tracer;

    public float rpm;

    // System
    private float secondsBetweenShots;
    private float nextPossibleShootTime;



    void Start()
    {
        source = GetComponent<AudioSource>();
        secondsBetweenShots = 60 / rpm;
        if (GetComponent<LineRenderer>())
        {
            tracer = GetComponent<LineRenderer>();
        }
    }



    public void shoot()
    {
        if (CanShoot())
        {
            Ray ray = new Ray(bulletSpawn.position, bulletSpawn.forward);
            RaycastHit hit;

            float shotDistance = 10;

            if (Physics.Raycast(ray, out hit, shotDistance))
            {
                shotDistance = hit.distance;
            }
            //Debug.DrawRay(ray.origin, ray.direction * shotDistance, Color.red, 1);
            nextPossibleShootTime = Time.time + secondsBetweenShots;
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(revolverSound, vol);

            if (tracer)
            {
                StartCoroutine("RenderTracer",ray.direction*shotDistance);
            }
      //----------------------------------------------------------------------------------------------------DEFINE NUMBER OF BULLETS AND RANGE AS PART OF THE LEVEL OF GUN      
            Rigidbody newBullet = Instantiate(Bullet, bulletSpawn.position, bulletSpawn.rotation) as Rigidbody; // Creation of bullet
            newBullet.AddForce(bulletSpawn.forward * Random.Range(500f, 600f) + bulletSpawn.up * Random.Range(-100f, 100f));

            Rigidbody newBullet2 = Instantiate(Bullet, bulletSpawn.position, bulletSpawn.rotation) as Rigidbody; // Creation of bullet
            newBullet2.AddForce(bulletSpawn.forward * Random.Range(500f, 600f) + bulletSpawn.up * Random.Range(-100f, 100f));

            Rigidbody newBullet3 = Instantiate(Bullet, bulletSpawn.position, bulletSpawn.rotation) as Rigidbody; // Creation of bullet
            newBullet3.AddForce(bulletSpawn.forward * Random.Range(500f, 600f) + bulletSpawn.up * Random.Range(-100f, 100f));
            
      //----------------------------------------------------------------------------------------------------DEFINE NUMBER OF BULLETS AND RANGE AS PART OF THE LEVEL OF GUN      

            Rigidbody newShell = Instantiate(Shell, bulletEjection.position, bulletEjection.rotation) as Rigidbody; // Creation of Shell           
            newShell.AddForce(bulletEjection.forward * Random.Range(100f, 200f));
           
        }
    }

    private bool CanShoot()
    {
        bool canShoot = true;

        if (Time.time < nextPossibleShootTime)
        {
            canShoot = false;
        }
        return canShoot;
    }

    IEnumerator RenderTracer(Vector3 hitPoint)
    {
        tracer.enabled = true;
        tracer.SetPosition(0, bulletSpawn.position);
        tracer.SetPosition(1, bulletSpawn.position+hitPoint);
        //  yield return new WaitForSeconds(10);
        yield return null;
        tracer.enabled = false;
    }
}
