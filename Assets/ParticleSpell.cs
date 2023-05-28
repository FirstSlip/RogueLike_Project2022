using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpell : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
