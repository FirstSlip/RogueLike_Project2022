using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TextAppearence : MonoBehaviour
{

    private Text dialog;
    private string str;
    private Image e_button;
    private Coroutine dialogCor;
    private PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        controller.enabled = false;
        dialog = GetComponentInChildren<Text>();
        dialog.text = "";
        str = "Hello, traveller!\n" +
            "You showed power by defeating this embodiment\nof evil. " +
            "Keep going, we'll meet again later";
        dialogCor = StartCoroutine(AddElementsToText());
        e_button = GetComponentsInChildren<Image>()[4];
        e_button.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StopCoroutine(dialogCor);
            controller.enabled = true;
            gameObject.SetActive(false);
        }
    }

    private IEnumerator AddElementsToText()
    {
        foreach (var symbol in str)
        {
            dialog.text += symbol;
            yield return new WaitForSeconds(0.05f);
        }
        DoInEndOfDialog();
    }

    private void DoInEndOfDialog()
    {
        e_button.enabled = true;
    }
}
