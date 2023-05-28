using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePhysics : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.TryGetComponent<Rigidbody2D>(out var rb);
        Destroy(rb);
        StartCoroutine(AddPhysics());
        
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator AddPhysics()
    {
        yield return new WaitForSeconds(1);
        var rigBody = gameObject.AddComponent<Rigidbody2D>();
        rigBody.bodyType = RigidbodyType2D.Dynamic;
        rigBody.mass = 30;

    }
}
