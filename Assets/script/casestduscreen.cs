using UnityEngine;
using UnityEngine.UI;

public class CaseStudyScreen : MonoBehaviour
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

        // Store selected case and index for later use if needed
        CaseStudyManager.selectedCase = selectedCase;
        CaseStudyManager.selectedIndexcs = index;

        // Example: Switch to another scene
        if (sceneSwitcher != null)
        {
            
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
                return new string[] { "55-year-old male with marked shortness of breath\r\nNever smoker; ECOG PS 2\r\nDiagnostic work up showed left lung adenocarcinoma with involvement of left supraclavicular lymph nodes and left pleural effusion\r\nMRI scan showed one CNS lesion\r\nDiagnosed as NSCLC Stage IVA; T3N3M1b\r\n","Start platinum-based chemotherapy\r\n (no molecular testing)", "Test only for selected targets (PD-L1, EGFR, ALK, ROS1, BRAF) via FISH/IHC", "Test only for selected targets (PD-L1, EGFR, ALK, ROS1, BRAF) via FISH/IHC", "Perform\r\nlarge-panel testing; NGS" };
            case "headandneck":
                // Return case-specific details
                return new string[] { "25-year-old female with bloody discharge from left nipple for 6 months\r\n3-cm mass detected in left breast by sonography (stage IIA)\r\nNo family history of breast cancer\r\nTriple negative for HER2, ER, PR\r\n", "Recommend surgery, radiotherapy and/or chemotherapy\r\n", "Start standard-of-care therapy and request molecular testing\r\n", "Request molecular testing before initiating therapy\r\n" };
            case "breast":
                // Return case-specific details
                return new string[] { "48-year-old male with a palpable and painful lump on the right thigh\r\nMRI imaging and biopsy suggested undifferentiated sarcoma with \v6-cm resectable tumour, histologic grade (G1), stage IB\r\nBiopsy showed positivity for SMA and negative for CD31, CD34, desmin, keratins, S100\r\n", "TNBC\r\n", "Secretory breast carcinoma\r\n", "Other" };
            case "sarcoma":
                // Return case-specific details
                return new string[] { "42-year-old female with large, painful mass and skin infiltration in the parotid area\r\nInitial diagnosis: stage III (pT3N0M0) parotid acinic cell carcinoma\r\n", "Chemotherapy or\r\nradiotherapy", "Surgery\r\n+\r\nchemotherapy", "Primary resection\v\n�\v\nradiotherapy\r\n" };
            case "crc":
                // Return case-specific details
                return new string[] { "68-year-old female with progressive fatigue, rectal bleeding and severe abdominal pain\r\nCT scan revealed a large mass in the sigmoid colon and\vmultiple hepatic lesions\r\nDiagnosed with unresectable metastatic colorectal cancer\r\n", "Chemoradiotherapy\vonly\r\n", "Molecular\vtesting\r\n" };
            case "ifs":
                // Return case-specific details
                return new string[] { "8-year-old male with a rapidly growing mass on the right hand\r\nDiagnosed as infantile fibrosarcoma\r\n", "Surgery\v+\vlarge-panel NGS\r\n", "Surgery\vonly\r\n", "Surgery\v+\vchemotherapy\r\n" };
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
