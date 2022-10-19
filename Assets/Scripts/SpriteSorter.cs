using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorter : MonoBehaviour
{
    public float offset;
    private int sortingOrderBase = 0;
    private Renderer renderer;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        renderer.sortingOrder = (int)((sortingOrderBase - transform.position.y)/50 + offset);
    }
}
