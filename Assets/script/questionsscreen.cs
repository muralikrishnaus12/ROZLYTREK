using UnityEngine;
using UnityEngine.UI;

public class QuestionScreen : MonoBehaviour
{
    public Text[] textBoxes;
    public Image questionImage;
    public GameObject buttonPrefab;
    public Transform buttonContainer;
    public SceneSwitcher sceneSwitcher;

    void Start()
    {
        string selectedCase = CaseStudyManager.selected;
        Debug.Log("Selected case from QuestionScreen: " + selectedCase);

        UpdateUI(selectedCase);
    }

    void UpdateUI(string selectedCase)
    {
        string[] questionDetails = GetQuestionDetails(selectedCase);
        string[] commonDetails = CaseStudyManager.GetCommonDetails(selectedCase);

        for (int i = 0; i < commonDetails.Length; i++)
        {
            textBoxes[i].text = commonDetails[i];
        }

        for (int i = 2; i < textBoxes.Length && (i - 2) < questionDetails.Length; i++)
        {
            textBoxes[i].text = questionDetails[i - 2];
        }

        for (int i = (commonDetails.Length + questionDetails.Length); i < textBoxes.Length; i++)
        {
            textBoxes[i].text = "";
        }

        for (int i = 1; i < questionDetails.Length; i++)
        {
            Debug.Log("Creating button for: " + questionDetails[i]);

            GameObject newButton = Instantiate(buttonPrefab, buttonContainer);
            if (newButton != null)
            {
                newButton.GetComponentInChildren<Text>().text = questionDetails[i];
                int index = i;
                newButton.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(selectedCase, index));
            }
            else
            {
                Debug.LogError("Failed to instantiate button for: " + questionDetails[i]);
            }
        }

        questionImage.sprite = GetCaseSprite(selectedCase);
    }

    void OnButtonClick(string selectedCase, int index)
    {
        Debug.Log("Button " + index + " clicked.");

        // Store selected case and index for use in other scripts
        CaseStudyManager.selectedCase = selectedCase;
        CaseStudyManager.selectedIndexqs = index;

        // Set the scene name based on conditions
        string sceneName = "infoscreen"; // Default scene name, replace with your logic

        // Example condition to set sceneName
        if (selectedCase == "sarcoma" && CaseStudyManager.selectedIndexcs == 1)
        {
            sceneName = "duplicate";
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

    string[] GetQuestionDetails(string selectedCase)
    {
        
        int selectedIndex = CaseStudyManager.selectedIndexcs;
        print(CaseStudyManager.selectedcrcop1+"q");
        // Replace these with actual details for each case
        switch (selectedCase)
        {
            case "lung":
                if (selectedIndex == 1)
                {
                    return new string[] { "Why not consider large-panel NGS at this stage?\r\n", "The patient is unlikely to present an actionable mutation\r\n", "I would prefer to start the patient on therapy as soon as possible\r\n", "I do not have access to the technology\r\n", "The technology is too expensive" };
                }
                else if (selectedIndex == 2)
                {
                    return new string[] { "Although these targets represent the most common forms of lung cancer, several other molecular targets such as NTRK are known to present somatic variants with therapeutic potentials,4", "next" };
                }
                else if (selectedIndex == 3)
                {
                    return new string[] { "Symptomatic and radiological progression on first-line treatment with carboplatin + paclitaxel\r\nNGS identified NTRK1-SQSTM1 fusion\r\nKnowing that the patient has CNS disease, what treatment would you select?\r\n", "TRK inhibitor (including clinical trial)\r\n", "Further chemotherapy\r\n", "Radiotherapy for CNS metastases, along with systemic treatment including TRK inhibitor)\r\n", "Other" };
                }
                else
                {
                    return new string[] { "NGS identified NTRK1-SQSTM1 fusion\r\nKnowing that the patient has CNS disease, what treatment would you select?\r\n", " TRK inhibitor (including clinical trial)\r\n", "Further chemotherapy\r\n", "Radiotherapy for CNS metastases, along with systemic treatment (including TRK inhibitor)\r\n", "Other" };
                }
            case "headandneck":
                if (selectedIndex == 1)
                {
                    return new string[] { "Why not consider large-panel NGS at this stage?\r\n","The patient is unlikely to present with an actionable mutation\r\n", " I would prefer to start the patient on therapy as soon as possible\r\n", "I do not have access to the technology\r\n", "The technology is too expensive" };
                }
                else
                {
                    return new string[] { "What kind of molecular testing would you perform?","Selected targets (MYB, EGFR, HER2) via FISH/IHC\r\n", "Selected targets (MYB, EGFR, HER2, CRCT3, TRK) via\r\nFISH/IHC\r\n", "Large-panel testing; NGS" };
                }
            case "breast":
                if (selectedIndex == 1)
                {
                    return new string[] { "How would you proceed?\r\n", "Surgery + ChT + RT + molecular testing of selected targets (PD-L1 &\r\nBRCA1/2) via FISH/IHC\r\n", "Surgery + ChT + RT + molecular testing of selected targets (PD-L1,\r\nBRCA1/2 & NTRK) via FISH/IHC\r\n", "Surgery + ChT + RT + large-panel NGS\r\n", "Molecular testing before any SoC therapy" };
                }
                else
                {
                    return new string[] { "How would you proceed?\r\n", "Surgery + ChT + RT + molecular testing of selected targets (PD-L1 &\r\nBRCA1/2) via FISH/IHC\r\n", "Surgery + ChT + RT + molecular testing of selected targets (PD-L1,\r\nBRCA1/2 & NTRK) via FISH/IHC\r\n", "Surgery + ChT + RT + large-panel NGS\r\n", "Molecular testing before any SoC therapy" };
                }
            case "sarcoma":
                if (selectedIndex == 1)
                {
                    return new string[] { "ESMO-EURACAN guidelines recommend primary resection as first-line treatment for resectable soft-tissue sarcomal\r\n", "Explore other options" };
                }
                else
                {
                    return new string[] { "• Patient underwent primary resection\r\n• Subsequent progression in lung with multiple resectable lung metastases\r\n• Patient had an episode of convulsions → brain\r\nCT scan suggested resectable brain metastasis\r\nHow would you proceed?\r\n", "Excision of lung metastases + chemotherapy + WBRT or stereotactic radiosurgery (no NGS testing)\r\n", "Excision of lung metastases + chemotherapy + WBRT or stereotactic radiosurgery + large-panel NGS\r\n", "Large-panel NGS" };
                }
            case "crc":
                if (selectedIndex == 1 && CaseStudyManager.selectedcrcop1 == 0)
                {
                    return new string[] { "Why not consider large-panel NGS at this stage?\r\n", " The patient is unlikely to present with an actionable mutation\r\n", " I would prefer to start the next line of therapy as soon as possible", "I do not have access to the technology\r\n", "The technology is too expensive" };
                }
                else
                {
                    return new string[] { "What kind of molecular testing would you perform?\r\n", "Selected targets (BRAF, KRAS, NRAS, MLH, MSH, MSI-H) via\r\nFISH/IHC\r\n", "Large Panel testing; NGS" };
                }
            case "ifs":
                if (selectedIndex == 1 )
                {
                    return new string[] { "Characteristic cytogenetic translocation t(12;15)(ETV6-NTRK3) identified by NGS\r\nWhat treatment would you select for this patient?\r\n", "TRK inhibitor\r\n", "Surgery + chemotherapy\r\n","Surgery alone\r\n", "Other" };
                }
                else
                {
                    return new string[] { "Although surgery is the recommended therapy for infantile fibrosarcoma,\r\n>90% of cases present NTRK gene fusions. 1.2 Testing for NTRK gene fusions may be considered in parallel with therapy\r\n", "Next" };
                }
            default:
                return new string[] { "Unknown case question" };
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
