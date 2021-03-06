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
    [SerializeField] GameObject vfx;
    [SerializeField] GameObject hitVFX;
    [SerializeField] Animator UFOAnimator;

    [SerializeField] AudioSource laserAudioSource;
    [SerializeField] AudioClip laserSound;
    [SerializeField] AudioSource movementSource;
    [SerializeField] AudioSource hitSource;

    public int lifeCount;

    void Awake()
    {
        //planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
        lifeCount = colorManager.GetLifeCount();
        movementSource.Play();
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
        laserAudioSource.PlayOneShot(laserSound);

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
            hitSource.Play();
            Debug.Log("Hit a rock");
            lifeCount = colorManager.LoseLife();
            Destroy(collision.transform.parent.gameObject);
            GameObject effect = Instantiate(vfx, collision.transform.parent.position, collision.transform.rotation);
            Destroy(effect, 2.0f);
            UFOAnimator.SetTrigger("PlayVFX");
            GameObject hitEffect = Instantiate(hitVFX, transform.position, transform.rotation);
            hitEffect.transform.parent = transform;
            Destroy(hitEffect, 2.0f);
        }
    }
}
