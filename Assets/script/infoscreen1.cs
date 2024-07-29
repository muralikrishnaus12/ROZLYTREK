using UnityEngine;
using UnityEngine.UI;

public class infoscreen1 : MonoBehaviour
{
    public Text[] textBoxes;
    public Image caseImage;
    public GameObject buttonPrefab;
    public Transform buttonContainer;
    public SceneSwitcher sceneSwitcher;

    void Start()
    {
        string selectedCase = CaseStudyManager.selected;
        Debug.Log("Selected case from CaseStudyScreen: " + selectedCase);

        UpdateUI(selectedCase);
    }

    void UpdateUI(string selectedCase)
    {
        string[] caseDetails = GetCaseDetails(selectedCase);
        string[] commonDetails = CaseStudyManager.GetCommonDetails(selectedCase);

        for (int i = 0; i < commonDetails.Length; i++)
        {
            textBoxes[i].text = commonDetails[i];
        }

        for (int i = 2; i < textBoxes.Length && (i - 2) < caseDetails.Length; i++)
        {
            textBoxes[i].text = caseDetails[i - 2];
        }

        for (int i = (commonDetails.Length + caseDetails.Length); i < textBoxes.Length; i++)
        {
            textBoxes[i].text = "";
        }

        for (int i = 1; i < caseDetails.Length; i++)
        {
            Debug.Log("Creating button for: " + caseDetails[i]);

            GameObject newButton = Instantiate(buttonPrefab, buttonContainer);
            if (newButton != null)
            {
                newButton.GetComponentInChildren<Text>().text = caseDetails[i];
                int index = i;
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
        Debug.Log("Button " + index + " clicked.");

        // Store selected case and index for use in other scripts
        CaseStudyManager.selectedCase = selectedCase;
        CaseStudyManager.selectedIndexrs = index;

        // Set the scene name based on conditions
        string sceneName = "summaryscreen"; // Default scene name, replace with your logic

        // Example condition to set sceneName
        if (selectedCase == "headandneck")
        {
            sceneName = "summaryscreen";
        }
        else if (selectedCase == "lung" && CaseStudyManager.selectedIndexcs == 1 && CaseStudyManager.selectedIndexrs == 1)
        {
            CaseStudyManager.selectedIndexcs = 2;
            sceneName = "infoscreen";
        }
        else if(selectedCase == "crc")
        {
            sceneName = "summaryscreen";
        }
        else if(selectedCase == "ifs")
        {
            sceneName = "resultscreen1";
        }
        // Add more conditions as needed

        // Switch scene if scene switcher is assigned
        if (sceneSwitcher != null)
        {
            // Set the scene name in SceneSwitcher
            sceneSwitcher.SetSceneName(sceneName);
            // Perform the scene switch
            sceneSwitcher.SwitchScene();
        }
        else
        {
            Debug.LogError("SceneSwitcher component is not assigned.");
        }
    }

    string[] GetCaseDetails(string selectedCase)
    {
        int selectedIndex = CaseStudyManager.selectedIndexcs;

        // Replace these with actual details for each case
        switch (selectedCase)
        {
            case "lung":
                // Return case-specific details
                return new string[] { "Although these targets represent the most common forms of lung cancer, several other molecular targets such as NTRK are known to present somatic variants with therapeutic potential3,4\r\n","Next" };
            case "headandneck":
                // Return case-specific details
                return new string[] { "Treatment options for MASC include surgery, radiation therapy, chemotherapy and targeted therapy (TRK inhibitors have shown activity in NTRK fusion-positive MASC) 1,4,5\r\n", "Next" };
            case "breast":
                // Return case-specific details
                return new string[] { };
            case "sarcoma":
                // Return case-specific details
                return new string[] { };
            case "crc":
                // Return case-specific details
                return new string[] { "ESMO guidelines recommend systemic therapy and, when possible, local ablative treatment, as first-line treatment for metastatic CRC. Based on the biomarker profile of the tumour characterised by molecular testing, targeted therapies can be considered as a subsequent line of treatment2,7\r\n", "Next " };
            case "ifs":
                // Return case-specific details
                return new string[] { "Some infantile fibrosarcoma tumours have a characteristic cytogenetic translocation t(12;15)(ETV6-NTRK3)3\r\nMolecular testing may identify targeted treatment options\r\n", "Next " };
            default:
                return new string[] { "Unknown case details" };
        }
    }

    Sprite GetCaseSprite(string selectedCase)
    {
        switch (selectedCase)
        {
            case "lung":
                return CaseStudyManager.lungSprite;
            case "headandneck":
                return CaseStudyManager.headAndNeckSprite;
            case "breast":
                return CaseStudyManager.breastSprite;
            case "sarcoma":
                return CaseStudyManager.sarcomaSprite;
            case "crc":
                return CaseStudyManager.crcSprite;
            case "ifs":
                return CaseStudyManager.ifsSprite;
            default:
                return null;
        }
    }
}
