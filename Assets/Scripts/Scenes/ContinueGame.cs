using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindObjectsOfType(typeof(Transform));
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(PlayerPrefs.GetFloat("s"));
    }

    public void NextSceneWithLoad()
    {
        //SceneManager.LoadScene(a)
        //EditorSceneManager.CreateScene("scene1 copy", )
    }
}
