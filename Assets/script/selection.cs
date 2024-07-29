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
    public static int selectedIndexcs;
    public static int selectedIndexqs;
    public static int selectedIndexis;
    public static int selectedIndexrs;
    public static int selectedcrcop1 = 0;

    public static Sprite lungSprite;
    public static Sprite headAndNeckSprite;
    public static Sprite breastSprite;
    public static Sprite sarcomaSprite;
    public static Sprite crcSprite;
    public static Sprite ifsSprite;

    public Sprite lungSpriteRef;
    public Sprite headAndNeckSpriteRef;
    public Sprite breastSpriteRef;
    public Sprite sarcomaSpriteRef;
    public Sprite crcSpriteRef;
    public Sprite ifsSpriteRef;

    void Start()
    {
        lungButton.onClick.AddListener(OnLungButtonClick);
        headAndNeckButton.onClick.AddListener(OnHeadAndNeckButtonClick);
        breastButton.onClick.AddListener(OnBreastButtonClick);
        sarcomaButton.onClick.AddListener(OnSarcomaButtonClick);
        crcButton.onClick.AddListener(OnCrcButtonClick);
        ifsButton.onClick.AddListener(OnIfsButtonClick);

        lungSprite = lungSpriteRef;
        headAndNeckSprite = headAndNeckSpriteRef;
        breastSprite = breastSpriteRef;
        sarcomaSprite = sarcomaSpriteRef;
        crcSprite = crcSpriteRef;
        ifsSprite = ifsSpriteRef;

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

    public static string[] GetCommonDetails(string selectedCase)
    {
        switch (selectedCase)
        {
            case "lung":
                return new string[] { "LUNG", "55-year-old male with marked shortness of breath." };
            case "headandneck":
                return new string[] { "Head And Neck", "42-year-old female with large, painful mass and skin infiltration in the parotid area" };
            case "breast":
                return new string[] { "breast", "25-year-old female with bloody discharge from left nipple for 6 months" };
            case "sarcoma":
                return new string[] { "sarcoma", "48-year-old male with a palpable and painful lump on the right thigh" };
            case "crc":
                return new string[] { "crc", "68-year-old female with progressive fatigue, rectal bleeding and severe abdominal pain" };
            case "ifs":
                return new string[] { "ifs", "8-year-old male with a rapidly growing mass on the right hand" };
            default:
                return new string[] { "Unknown case" };
        }
    }
}
