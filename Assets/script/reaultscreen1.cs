using UnityEngine;
using UnityEngine.UI;

public class resultscreen1 : MonoBehaviour
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

        for (int i = 2; i < caseDetails.Length; i++)
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
        if (selectedCase == "crc")
        {
            sceneName = "infoscreen1";
        }
        else if(selectedCase == "ifs")
        {
            CaseStudyManager.selectedIndexcs = 1;
            sceneName = "infoscreen";
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
                return new string[] { "No gene alteration identified in the specific targets tested by IHC/FISH \nNGS identified NTRK1-SQSTM1 fusion\r\n","Knowing that the patient has CNS disease, what treatment would you select?\r\n→ Large-panel sequencing performed", "TRK inhibitor (including clinical trial)\r\n", " Further chemotherapy\r\n", " Radiotherapy for CNS metastases, along with systemic treatment (including TRK inhibitor)", "Other" };
            case "headandneck":
                // Return case-specific details
                return new string[] { };
            case "breast":
                // Return case-specific details
                return new string[] { };
            case "sarcoma":
                // Return case-specific details
                return new string[] { };
            case "crc":
                // Return case-specific details
                return new string[] { "Biopsy of primary tumour: KRAS and BRAF WT; MSI-H\r\n• Additional testing performed to further characterise the molecular profile of the tumour\r\nLMNA-NTRK1 gene fusion detected by NGS\r\n","What treatment would you recommend knowing the tumour has an\r\nLMNA-NTRK1 fusion?\r\n", "Further chemotherapy\r\n" , "Regorafenib","TRK inhibitor (including enrolment into clinical trial)", "Other" };
            case "ifs":
                // Return case-specific details
                return new string[] { "What treatment would you select for this patient?\r\n", "TRK inhibitor\r\n", "Surgery + chemotherapy\r\n", "Surgery alone\r\n", "Other" };
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
