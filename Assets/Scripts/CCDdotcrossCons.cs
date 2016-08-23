using UnityEngine;
using System.Collections;

public class CCDdotcrossCons : MonoBehaviour {
    public float angleG0weight = 0.9f;
    public Transform G1;
    public float angleG1weight = 0.5f;
    public Transform G2;
    public float angleG2weight = 0.2f;
    public Transform G3;
    public Transform target;

    private Transform G0Transform;
    private Transform G1Transform;
    private Transform G2Transform;
    private Transform G3Transform;
    private Transform targetTransform;

    private Vector3 PePc = Vector3.zero;
    private Vector3 PtPc = Vector3.zero;
    private Vector3 tempvector = Vector3.zero;

    private float teta = 0f;
    private float transition = 1;

    //  Vector3 PePc = vector

    // Use this for initialization
    void Start()
    {
        G0Transform = transform;
        G0Transform.parent = transform;

        G1Transform = G1.transform;
        G1Transform.parent = G0Transform;

        G2Transform = G2.transform;
        G2Transform.parent = G1Transform;

        G3Transform = G3.transform;
        G3Transform.parent = G2Transform;

        targetTransform = target.transform;


    }

    void Update()
    {
    }
    void LateUpdate()
    {

        targetTransform.position = target.position;     //Pt
        G3Transform.position = G3.position;             //Pe

        float errorCCD = Vector3.Distance(targetTransform.position, G3Transform.position);
        //        Debug.Log(errorCCD);
        if (errorCCD > 0.05)
        {

   //         Quaternion storeG0 = transform.rotation;
     //       Quaternion storeG1 = G1.rotation;
       //     Quaternion storeG2 = G2.rotation;
         //   Quaternion storeG3 = G3.rotation;

            G0Transform.position = transform.position;

            PePc = G3Transform.position - G0Transform.position;
            PtPc = targetTransform.position - G0Transform.position;

            teta = Mathf.Acos(Vector3.Dot((PePc / Vector3.Magnitude(PePc)), (PtPc / Vector3.Magnitude(PtPc)))) * Mathf.Rad2Deg;
            tempvector = Vector3.Cross((PePc / Vector3.Magnitude(PePc)), (PtPc / Vector3.Magnitude(PtPc)));

            transform.RotateAround(transform.position, tempvector, teta * angleG0weight);

            //     Vector3 eulerAngles = transform.rotation.eulerAngles;
            //   eulerAngles = new Vector3(0, eulerAngles.y, eulerAngles.z);
            // transform.rotation = Quaternion.Euler(eulerAngles);

            //--------------Going forward
            G1Transform.position = G1.position;

            PePc = G3Transform.position - G1Transform.position;
            PtPc = targetTransform.position - G1Transform.position;

            teta = Mathf.Acos(Vector3.Dot((PePc / Vector3.Magnitude(PePc)), (PtPc / Vector3.Magnitude(PtPc)))) * Mathf.Rad2Deg;
            tempvector = Vector3.Cross((PePc / Vector3.Magnitude(PePc)), (PtPc / Vector3.Magnitude(PtPc)));

            G1Transform.RotateAround(G1Transform.position, tempvector, teta * angleG1weight);

            //--------------Going forward
            G2Transform.position = G2.position;

            PePc = G3Transform.position - G2Transform.position;
            PtPc = targetTransform.position - G2Transform.position;

            teta = Mathf.Acos(Vector3.Dot((PePc / Vector3.Magnitude(PePc)), (PtPc / Vector3.Magnitude(PtPc)))) * Mathf.Rad2Deg;
            tempvector = Vector3.Cross((PePc / Vector3.Magnitude(PePc)), (PtPc / Vector3.Magnitude(PtPc)));

            //   if (tempvector.z > 0 && teta>45) teta = 45;

            G2Transform.RotateAround(G2Transform.position, tempvector, angleG2weight * teta);


            //-------------------end

            //     transition = Mathf.Clamp01(transition);
            //     transform.rotation = Quaternion.Slerp(storeG0, transform.rotation, transition);
            //     G1.rotation = Quaternion.Slerp(storeG1, G1Transform.rotation, transition);
            //     G2.rotation = Quaternion.Slerp(storeG2, G2Transform.rotation, transition);

            //  errorCCD = Vector3.Distance(targetTransform.position, G3Transform.position);
        }
        //   Debug.Log(tempvector);

        // tempTest




    }
}

