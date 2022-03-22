using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public GameObject playerObject;
    [SerializeField] public GameObject mapObject;
    public float speed;
    private Transform playerTransform;
    private Rigidbody playerRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = playerObject.GetComponent<Transform>();
        playerRigidbody = playerObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        playerTransform.LookAt(mapObject.transform);
        ApplyCustomGravity();
        if (Input.GetKey(KeyCode.W))
        {
            //playerObject.transform.Translate(-transform.up * speed *Time.deltaTime);
            playerRigidbody.AddForce(playerObject.transform.up * speed * Time.deltaTime);
        }
    }

    private void ApplyCustomGravity()
    {
        Vector3 downForce = mapObject.transform.position - playerTransform.position;
        downForce = downForce.normalized;
        //playerObject.GetComponent<Rigidbody>().AddForce(downForce * 9.81f);
        playerRigidbody.MovePosition(playerTransform.position + (downForce * Time.deltaTime));
        //playerObject.transform.Translate(downForce * Time.deltaTime);
    }
}
