using UnityEngine;
using UnityEngine.UI;

public class casestudyscreen : MonoBehaviour
{
    public Text[] textBoxes;    // Array to hold references to the text boxes
    public Image caseImage;     // Reference to the sprite
    public GameObject buttonPrefab; // Reference to the button prefab
    public Transform buttonContainer; // Reference to the container for generated buttons

    public Sprite lungSprite;
    public Sprite headAndNeckSprite;
    public Sprite breastSprite;
    public Sprite sarcomaSprite;
    public Sprite crcSprite;
    public Sprite ifsSprite;

    public SceneSwitcher sceneSwitcher;

    void Start()
    {
        string selectedCase = CaseStudyManager.selected;
        Debug.Log("Selected case from casestudyscreen: " + selectedCase);

        UpdateUI(selectedCase);
    }

    void UpdateUI(string selectedCase)
    {
        string[] caseDetails = GetCaseDetails(selectedCase);

        // Update the first three text boxes
        for (int i = 0; i < 3 && i < caseDetails.Length; i++)
        {
            textBoxes[i].text = caseDetails[i];
        }

        // Clear the rest of the text boxes
        for (int i = 3; i < textBoxes.Length; i++)
        {
            textBoxes[i].text = "";
        }

        // Generate buttons for the remaining case details
        for (int i = 3; i < caseDetails.Length; i++)
        {
            Debug.Log("Creating button for: " + caseDetails[i]); // Debug log

            GameObject newButton = Instantiate(buttonPrefab, buttonContainer);
            if (newButton != null)
            {
                newButton.GetComponentInChildren<Text>().text = caseDetails[i];
                int index = i; // Capture the current value of i
                newButton.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(selectedCase, index));
            }
            else
            {
                Debug.LogError("Failed to instantiate button for: " + caseDetails[i]);
            }
        }

        caseImage.sprite = GetCaseSprite(selectedCase);
    }

    void OnButtonClick(string selectedCase, int index)
    {
        bool shouldSwitchScene = false;

        switch (selectedCase)
        {
            case "lung":
                if (index == 3) shouldSwitchScene = true;
                break;
            case "breast":
                if (index == 4) shouldSwitchScene = true;
                break;
            // Add more cases here as needed
            // case "caseName":
            //     if (index == desiredIndex) shouldSwitchScene = true;
            //     break;
            default:
                shouldSwitchScene = false;
                break;
        }

        if (shouldSwitchScene)
        {
            Debug.Log("Button " + index + " clicked");

            // Call the SwitchScene method of the SceneSwitcher component
            if (sceneSwitcher != null)
            {
                sceneSwitcher.SwitchScene();
            }
            else
            {
                Debug.LogError("SceneSwitcher component is not assigned.");
            }
        }
    }

    string[] GetCaseDetails(string selectedCase)
    {
        // Replace these with actual details for each case
        switch (selectedCase)
        {
            case "lung":
                return new string[] { "LUNG", "55-year-old male with marked shortness of breath.", "• 55-year-old male with marked shortness of breath\r\n• Never smoker; ECOG PS 2\r\n• Diagnostic work up showed left lung adenocarcinoma with involvement of left supraclavicular lymph nodes and left pleural effusion\r\n• MRI scan showed one CNS lesion\r\n• Diagnosed as NSCLC Stage IVA; T3N3M1b", "Start platinum-based chemotherapy\r\n (no molecular testing)", "Test only for selected targets (PD-L1, EGFR, ALK, ROS1, BRAF) via FISH/IHC", "Test only for selected targets (PD-L1, EGFR, ALK, ROS1, BRAF) via FISH/IHC", "Perform\r\nlarge-panel testing; NGS" };
            case "headandneck":
                return new string[] { "Head And Neck", "42-year-old female with large, painful\vmass and skin infiltration in the parotid area\r\n", "42-year-old female with large, painful mass and skin infiltration in the parotid area\r\nInitial diagnosis: stage III (pT3N0M0) parotid acinic cell carcinoma\r\n", "Recommend surgery, radiotherapy and/or chemotherapy\r\n", "Start standard-of-care therapy and request molecular testing\r\n", "Request molecular testing before initiating therapy\r\n" };
            case "breast":
                return new string[] { "breast", "25-year-old female with bloody discharge from left nipple for\v6 months\r\n", "25-year-old female with bloody discharge from left nipple for 6 months\r\n3-cm mass detected in left breast by sonography (stage IIA)\r\nNo family history of breast cancer\r\nTriple negative for HER2, ER, PR\r\n", "TNBC\r\n", "Secretory breast carcinoma\r\n", "Secretory breast carcinoma\r\n" };
            case "sarcoma":
                return new string[] { "sarcoma", "48-year-old male with a palpable and painful lump on the right thigh", "48-year-old male with a palpable and painful lump on the right thigh\r\nMRI imaging and biopsy suggested undifferentiated sarcoma with\r\n6-cm resectable tumour, histologic grade (G1), stage IB\r\nBiopsy showed positivity for SMA and negative for CD31, CD34, desmin, keratins, S100", "Chemotherapy or\r\nradiotherapy", "Surgery\r\n+\r\nchemotherapy", "Primary resection\v±\vradiotherapy\r\n" };
            case "crc":
                return new string[] { "crc", "68-year-old female with progressive fatigue, rectal bleeding and severe abdominal pain", "68-year-old female with progressive fatigue, rectal bleeding and severe abdominal pain\r\nCT scan revealed a large mass in the sigmoid colon and\vmultiple hepatic lesions\r\nDiagnosed with unresectable metastatic colorectal cancer\r\n", "Chemoradiotherapy\vonly\r\n", "Molecular\vtesting\r\n" };
            case "ifs":
                return new string[] { "ifs", "8-year-old male with a rapidly\r\n growing mass on the right hand", "8-year-old male with a rapidly growing mass on the right hand\r\nDiagnosed as infantile fibrosarcoma\r\n", "Surgery\v+\vlarge-panel NGS\r\n", "Surgery\vonly\r\n", "Surgery\v+\vchemotherapy\r\n" };
            default:
                return new string[] { "Unknown case" };
        }
    }

    Sprite GetCaseSprite(string selectedCase)
    {
        // Use the assigned sprites for each case
        switch (selectedCase)
        {
            case "lung":
                return lungSprite;
            case "headandneck":
                return headAndNeckSprite;
            case "breast":
                return breastSprite;
            case "sarcoma":
                return sarcomaSprite;
            case "crc":
                return crcSprite;
            case "ifs":
                return ifsSprite;
            default:
                return null;
        }
    }
}
