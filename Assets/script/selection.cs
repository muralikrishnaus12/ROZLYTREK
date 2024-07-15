// File: CaseStudyManager.cs
using UnityEngine;
using UnityEngine.UI;

public class CaseStudyManager : MonoBehaviour
{
    public Button lungButton;
    public Button headAndNeckButton;
    public Button breastButton;
    public Button sarcomaButton;
    public Button crcButton;
    public Button ifsButton;

    public static string selected = "lung";
    public static string selectedCase;
    public static int selectedIndex;

    void Start()
    {
        lungButton.onClick.AddListener(OnLungButtonClick);
        headAndNeckButton.onClick.AddListener(OnHeadAndNeckButtonClick);
        breastButton.onClick.AddListener(OnBreastButtonClick);
        sarcomaButton.onClick.AddListener(OnSarcomaButtonClick);
        crcButton.onClick.AddListener(OnCrcButtonClick);
        ifsButton.onClick.AddListener(OnIfsButtonClick);

        CheckForEmptyGameObjectNames();
    }

    void OnLungButtonClick()
    {
        Debug.Log("Lung button clicked");
        selected = "lung";
    }

    void OnHeadAndNeckButtonClick()
    {
        Debug.Log("Head and Neck button clicked");
        selected = "headandneck";
    }

    void OnBreastButtonClick()
    {
        Debug.Log("Breast button clicked");
        selected = "breast";
    }

    void OnSarcomaButtonClick()
    {
        Debug.Log("Sarcoma button clicked");
        selected = "sarcoma";
    }

    void OnCrcButtonClick()
    {
        Debug.Log("CRC button clicked");
        selected = "crc";
    }

    void OnIfsButtonClick()
    {
        Debug.Log("IFS button clicked");
        selected = "ifs";
    }

    void CheckForEmptyGameObjectNames()
    {
        foreach (GameObject go in FindObjectsOfType<GameObject>())
        {
            if (string.IsNullOrEmpty(go.name))
            {
                Debug.LogError("Found a GameObject with an empty name!");
                go.name = "UnnamedGameObject";
            }
        }
    }
}
