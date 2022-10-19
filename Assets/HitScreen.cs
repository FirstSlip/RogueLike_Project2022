using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitScreen : MonoBehaviour
{
    public bool enableHitScreen;
    // Start is called before the first frame update
    void Start()
    {
        enableHitScreen = false;
        GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enableHitScreen)
            StartCoroutine(EnableHitScreen());
    }
    private IEnumerator EnableHitScreen()
    {
        GetComponent<Image>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        GetComponent<Image>().enabled = false;
        enableHitScreen = false;
    }
}
