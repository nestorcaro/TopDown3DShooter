using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]

public class PlayerController : MonoBehaviour {
    // Handling variables
    public float rotationSpeed = 450;
    public float walkSpeed = 5;
    public float runSpeed = 8;
    public int health = 10;

    // System
    private Quaternion targerRotation;

    // Components
    private CharacterController controller;
    private Camera cam;
    public Gun gun;

	void Start () {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;      // Reference the main camera for our mouse orientation
	}

    void Update () {
  //      ControlArrows();
        ControlMouse();
        if (Input.GetButtonDown("Shoot"))
        {
            gun.shoot();
        }
	}

    void ControlMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.transform.position.y - transform.position.y));
        targerRotation = Quaternion.LookRotation(mousePos- new Vector3(transform.position.x,0,transform.position.z));
        transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targerRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 motion = input;

        motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? .7f : 1;
        // if X axis is pressed   and Z axis is pressed    multuply by 0.7f   otherwise by 1
        motion *= (Input.GetButton("Run")) ? runSpeed : walkSpeed;
        // if RUN is pressed    multuply by runSpeed   otherwise by walkSpeed

        motion += Vector3.up * -8;       // Some kind of gravity
        controller.Move(motion * Time.deltaTime);
    }

        void ControlArrows()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        // Player facing the direction of the ARROWS.... this should be modyfied for tracking the pointer
        if (input != Vector3.zero)
        {
            targerRotation = Quaternion.LookRotation(input);
            transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targerRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
            //              .up is rotation along the Y axis                   player current rotation      target rotation          how much can we rotate
        }

        Vector3 motion = input;

        motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? .7f : 1;
        // if X axis is pressed   and Z axis is pressed    multuply by 0.7f   otherwise by 1
        motion *= (Input.GetButton("Run")) ? runSpeed : walkSpeed;
        // if RUN is pressed    multuply by runSpeed   otherwise by walkSpeed

        motion += Vector3.up * -8;       // Some kind of gravity
        controller.Move(motion * Time.deltaTime);
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
