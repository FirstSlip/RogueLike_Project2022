using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Resolution[] resolutions;
    public Dropdown resolutinList;
    public Toggle fullscreenToggle;
    public static string currentResolution;
    public static int width;
    public static int height;
    public static bool isFullScreen;
    public static int FPS;
    public GameObject settings;
    //public readonly string currentResolutionRead = currentResolution;
    //public readonly int widthRead = width;
    //public readonly int heightRead = height;
    //public readonly bool isFullScreenRead = isFullScreen;
    //public readonly int FPSRead = FPS;

    // Start is called before the first frame update
    void Start()
    {
        settings.SetActive(false);
        resolutinList.options.Clear();
        resolutions = Screen.resolutions;
        fullscreenToggle.isOn = true;
        currentResolution = Screen.currentResolution.ToString();
        for (int i = 0; i < resolutions.Length; i++)
        {
            Dropdown.OptionData a = new Dropdown.OptionData(resolutions[i].ToString());
            resolutinList.options.Add(a);
        }
        //resolutinList.GetComponentInChildren<Text>().text = currentResolution;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Accept()
    {
        currentResolution = resolutinList.GetComponentInChildren<Text>().text;
        var parameters = currentResolution.Split(' ');
        width = int.Parse(parameters[0]);
        height = int.Parse(parameters[2]);
        isFullScreen = fullscreenToggle.isOn;
        FPS = int.Parse(parameters[4].Trim(new char[] { 'H', 'z' }));
        Screen.SetResolution(width, height, isFullScreen, FPS);
        Debug.Log(FPS);
    }

    public void OpenSettings()
    {
        settings.SetActive(true);
    }

    public void CloseSettings()
    {
        settings.SetActive(false);
    }
}
