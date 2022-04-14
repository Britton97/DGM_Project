using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float rotSpeed;
    Vector3 direction;
    Vector3 rotDirection;
    [SerializeField] public GameObject rock;
    [SerializeField] public List<GameObject> rockList = new List<GameObject>();


    private void Start()
    {
        rotSpeed = Random.Range(10, 26);
        float x = Random.Range(-10, 11);
        float z = Random.Range(-10, 11);
        direction = new Vector3(x, 0, z).normalized;
        SpawnRock();
    }
    private void Update()
    {
        transform.RotateAround(Vector3.zero, direction, rotSpeed * Time.deltaTime);
        rock.transform.Rotate(rotDirection * 50 * Time.deltaTime);
    }

    private void SpawnRock()
    {
        float xRock = Random.Range(-10, 11);
        float zRock = Random.Range(-10, 11);
        rock = Instantiate(rockList[Random.Range(0, rockList.Count)], transform.position, Quaternion.identity);
        rock.transform.parent = gameObject.transform;
        rotDirection = new Vector3(xRock, 0, zRock).normalized;

        ColorManager colorManager = GameObject.Find("ColorManager").GetComponent<ColorManager>();
        Renderer rockMat = rock.GetComponent<Renderer>();
        colorManager.MakeRockRandomColor(rockMat);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player_Model")
        {
            Debug.Log("Hit the player");
        }
        else
        {
            Debug.Log("Hit something else");
        }
    }
}
