using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(3f);
        Instantiate(Resources.Load("Prefabs/Enemy") as GameObject, transform.position, Quaternion.Euler(0, 0, 0));
        Destroy(gameObject);
    }
}
