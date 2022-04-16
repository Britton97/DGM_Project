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

    [SerializeField] GameObject questionColorObj;
    [SerializeField] GameObject questionBottom;

    public int lifeCount;

    void Awake()
    {
        //planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
        lifeCount = colorManager.GetLifeCount();
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
        transform.RotateAround(mapObject.transform.position, transform.forward * -zRot * Time.deltaTime, speed * Time.deltaTime);
        transform.RotateAround(mapObject.transform.position, transform.right * xRot * Time.deltaTime, speed * Time.deltaTime);

        questionColorObj.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        questionBottom.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Rock"))
        {
            Debug.Log("Hit a rock");
            lifeCount = colorManager.LoseLife();
        }
    }
}
