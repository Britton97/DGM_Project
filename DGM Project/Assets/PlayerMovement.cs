using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public GameObject playerObject;
    [SerializeField] public GameObject mapObject;
    [SerializeField] private string layerName;
    [SerializeField] private ColorManager colorManager;
    public Color colorHit;
    public float speed;
    public float rotationSpeed;
    Rigidbody rb;
    //[SerializeField] public GravityAttractor planet;
    //float counter = 0;

    [SerializeField] GameObject th;

    void Awake()
    {
        //planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
        rb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -9.81f, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CastRayToGround();
        }
    }

    private void FixedUpdate()
    {
        float xRot = Input.GetAxisRaw("Vertical");
        float zRot = Input.GetAxisRaw("Horizontal");

        Vector3 pos = new Vector3(xRot, 0, 0);
        transform.RotateAround(mapObject.transform.position, transform.forward * zRot * Time.deltaTime, speed * Time.deltaTime);
        transform.RotateAround(mapObject.transform.position, transform.right * xRot * Time.deltaTime, speed * Time.deltaTime);

        th.transform.Rotate(0, 10, 0);
    }

    private void CastRayToGround()
    {
        RaycastHit hit;

        Physics.Raycast(transform.position,-transform.up, 10f);

        if(Physics.Raycast(transform.position, -transform.up, out hit, 10f) && hit.transform.gameObject.layer == LayerMask.NameToLayer(layerName))
        {
            colorManager.HandleColorChoice(hit);
        }
        else
        {
            Debug.Log("error");
        }
    }
}
