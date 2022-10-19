using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("HitScreen").GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
