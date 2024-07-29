using UnityEngine;
using UnityEngine.UI;

public class resultscreen : MonoBehaviour
{
    public Text[] textBoxes;
    public Image caseImage;
    public GameObject buttonPrefab;
    public Transform buttonContainer;
    public SceneSwitcher sceneSwitcher;

    void OnEnable()
    {
        // This method is called whenever the object is enabled, ensuring the selected case is up to date.
        UpdateUI(CaseStudyManager.selected);
    }

    void UpdateUI(string selectedCase)
    {
        Debug.Log("Selected case from CaseStudyScreen: " + selectedCase);

        string[] caseDetails = GetCaseDetails(selectedCase);
        string[] commonDetails = CaseStudyManager.GetCommonDetails(selectedCase);

        // Update common details in text boxes
        for (int i = 0; i < commonDetails.Length && i < textBoxes.Length; i++)
        {
            textBoxes[i].text = commonDetails[i];
        }

        // Update case-specific details in text boxes
        for (int i = 0; i < caseDetails.Length && i < textBoxes.Length - commonDetails.Length; i++)
        {
            textBoxes[i + commonDetails.Length].text = caseDetails[i];
        }

        // Clear remaining text boxes
        for (int i = commonDetails.Length + caseDetails.Length; i < textBoxes.Length; i++)
        {
            textBoxes[i].text = "";
        }

        // Create buttons for case-specific details
        for (int i = 2; i < caseDetails.Length; i++)
        {
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

        // Update case image
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
        print(CaseStudyManager.selectedIndexcs);
        print(CaseStudyManager.selectedIndexis);
        print(CaseStudyManager.selectedIndexqs);
        print(CaseStudyManager.selectedIndexrs);
        // Example conditions to set sceneName
        if (selectedCase == "lung" && CaseStudyManager.selectedIndexcs == 1 && CaseStudyManager.selectedIndexrs == 2)
        {
            sceneName = "infoscreen";
        }
        else if (selectedCase == "lung" && CaseStudyManager.selectedIndexcs == 1 && CaseStudyManager.selectedIndexrs==3)
        {
            sceneName = "infoscreen1";
        }
        else if (selectedCase == "lung" && CaseStudyManager.selectedIndexcs == 1 && CaseStudyManager.selectedIndexrs==4)
        {
            CaseStudyManager.selectedIndexcs = 2;
            sceneName = "infoscreen";
        }
        else if (selectedCase == "sarcoma" && CaseStudyManager.selectedIndexqs == 1)
        {
            sceneName = "questionscreen";
        }
        else if (selectedCase == "headandneck" && CaseStudyManager.selectedIndexis == 1 && CaseStudyManager.selectedIndexcs == 1)
        {
            sceneName = "infoscreen";
        }
        else if (selectedCase == "headandneck" && CaseStudyManager.selectedIndexcs == 1)
        {
            sceneName = "infoscreen1";
        }
        else if(selectedCase =="crc" && CaseStudyManager.selectedIndexqs== 1)
        {
            sceneName = "infoscreen1";
        }
        else if((CaseStudyManager.selectedIndexcs == 1 && CaseStudyManager.selectedIndexis == 2))
        {
            sceneName = "infoscreen";
        }
        else if (CaseStudyManager.selectedIndexis == 1)
        {
            sceneName = "infoscreen1";
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



        // Replace with actual case-specific details retrieval logic
        switch (selectedCase)
        {
            case "lung":
                if (selectedIndex == 1)
                {
                    return new string[] { "Symptomatic and radiological progression on first-line treatment with carboplatin + paclitaxel\r\n", "Would you perform molecular testing at this stage?\r\n", "No", " Yes, selected targets (PD-L1, EGFR, ALK, ROS1, BRAF) via\r\n= S-/-0\r\n", "Yes, large-panel NGS" };
                }
                else
                {
                    return new string[] { "Treatment options for advanced/metastatic NSCLC include surgery, chemotherapy, radiation therapy, immunotherapy and targeted therapy (TRK inhibitors have shown activity in NTRK fusion-positive NSCLC)\r\n", "", "Next" };
                }
            case "headandneck":
                if (CaseStudyManager.selectedIndexis == 1 && CaseStudyManager.selectedIndexcs == 1)
                {
                    return new string[] { "This patient may have MASC, a rare form of cancer that is often misdiagnosed\r\nas acinic cell carcinoma, mucoepidermoid carcinoma or adenocarcinoma\r\nnot-otherwise-specified.1\r\n>80% of MASC cases present an ETV6-NTRK3 fusion 1-3\r\n", "", "Explore other options" };
                }
                else if (CaseStudyManager.selectedIndexis > 1)
                {
                    return new string[] { "NGS testing, FISH/IHC\n→ reclassification as MASC with ETV6-NTRK3 fusion\n>80% of MASC cases present an ETV6-NTRK3 fusion. 1-3\nIdentifying this fusion in this type of tumour strongly suggests\nMASC diagnosis\n", "What treatment would you recommend knowing the tumour\nhas an ETV6-NTRK3 fusion?", "TRK inhibitor (including enrolment into clinical trial)\r\n", "Other" };
                }
                else
                {
                    return new string[] { "Treatment options for MASC include surgery, radiation therapy, chemotherapy and targeted therapy (TRK inhibitors have shown activity in NTRK fusion-positive MASC) 1,4,5", "", "Next" };
                }
            case "breast":
                return new string[] { "There is no clear consensus on the treatment of secretory breast cancer, but surgery seems to currently be the primary treatment.2\r\nTargeted therapies including TRK inhibitors have shown activity in\r\nNTRK fusion-positive breast cancers3-5\r\n", "", "Next" };

            case "sarcoma":
                if (CaseStudyManager.selectedIndexqs == 1)
                {
                    return new string[] { "Chemotherapy/radiotherapy and, if possible, resection of metastases are the standard treatment at this stage, but high-risk patients tend to relapse within\r\n2-3 years. 1,2 Broad molecular testing to further characterise the tumour profile could support additional treatment options\r\n", "", "Explore other options" };
                }
                else
                {
                    return new string[] { "Surgery is the standard of care for sarcoma, associated with radiation therapy and chemotherapy.' Targeted agents such as tyrosine kinase inhibitors are becoming important treatment options for soft tissue sarcomas", "", "Next" };
                }
            case "crc":

                if (CaseStudyManager.selectedIndexqs==1)
                {
                    return new string[] { "• Biopsy of primary tumour: KRAS and BRAF WT; MSI-H\r\n• Additional testing performed to further characterise the molecular profile of the tumour\r\nRoche\r\nLMNA-NTRK1 gene fusion detected by NGS\r\n", "What treatment would you recommend knowing the tumour has an\r\nLMNA-NTRK1 fusion?\r\n", "Further chemotherapy\r\n", "Regorafenib", "TRK inhibitor (including enrolment into clinical trial)\r\n", "Other" };
                }
                else if (CaseStudyManager.selectedIndexcs == 1 && CaseStudyManager.selectedIndexis == 2)
                {
                    return new string[] { "Chromosomal rearrangements and mutations of specific oncogenes are observed in up to 85% of patients with CRC1\r\nPerforming molecular testing on this patient may identify targeted treatment options?\r\n","", "Explore other options " };
                }
                else
                {
                    return new string[] { "ESMO guidelines recommend systemic therapy and, when possible, local ablative treatment, as first-line treatment for metastatic CRC. Based on the biomarker profile of the tumour characterised by molecular testing, targeted therapies can be considered as a subsequent line of treatment2,7\r\n","", "Next " };
                }
            case "ifs":
                if (CaseStudyManager.selectedIndexis==1)
                {
                    return new string[] { "Why not consider large-panel NGS at this stage?", "The patient is unlikely to present with an actionable mutation\r\n", "I would prefer to start the next line of therapy as soon as possible\r\n", " I do not have access to the technology\r\n", "The technology is too expensive" };
                }
                else
                {
                    
                    return new string[] {  };
                }
            default:
                return new string[] { "Unknown case question" };
        }
    }

    Sprite GetCaseSprite(string selectedCase)
    {
        // Replace with actual sprite retrieval logic
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
