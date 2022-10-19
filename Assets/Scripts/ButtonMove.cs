using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMove : MonoBehaviour
{
    private float defaultPosY;
    private bool isUp;
    // Start is called before the first frame update
    void Start()
    {
        defaultPosY = transform.localPosition.y;
        isUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.y <= defaultPosY + 0.2f && isUp)
            transform.localPosition = new Vector2(0, transform.localPosition.y + Time.deltaTime);
        else
            isUp = false;
        if (transform.localPosition.y >= defaultPosY - 0.2f && !isUp)
            transform.localPosition = new Vector2(0, transform.localPosition.y - Time.deltaTime);
        else
            isUp = true;
    }
}
