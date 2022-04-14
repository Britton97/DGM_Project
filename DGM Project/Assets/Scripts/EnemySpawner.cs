using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject test;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SpawnRock();
        }
    }

    public void SpawnRock()
    {
        Quaternion m = Random.rotation;
        GameObject n = Instantiate(test, transform.position, m);
        n.transform.position = n.transform.forward * 10;
    }
}
