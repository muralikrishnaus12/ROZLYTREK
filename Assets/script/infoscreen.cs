using Unity.VisualScripting;
using UnityEditor.Presets;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;
using static UnityEditor.ShaderData;

public class infoscreen : MonoBehaviour
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

        int selectedIndex = CaseStudyManager.selectedIndexcs;
        string[] caseDetails = GetCaseDetails(selectedCase, selectedIndex);
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
        for (int i = 1; i < caseDetails.Length; i++)
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
        CaseStudyManager.selectedIndexis = index;

        // Set the scene name based on conditions
        string sceneName = "resultscreen"; // Default scene name, replace with your logic

        // Example condition to set sceneName
        if (selectedCase == "lung" && CaseStudyManager.selectedIndexcs > 2)
        {
            sceneName = "summaryscreen";
        }
        else if (selectedCase == "crc" && CaseStudyManager.selectedIndexcs == 1 && CaseStudyManager.selectedIndexis == 1 && CaseStudyManager.selectedcrcop1 == 1 && CaseStudyManager.selectedIndexqs == 1)
        {
            sceneName = "resultscreen1";
        }
        else if (selectedCase == "headandneck" &&((CaseStudyManager.selectedIndexqs==1 && CaseStudyManager.selectedIndexcs==2)|| (CaseStudyManager.selectedIndexqs == 1 && CaseStudyManager.selectedIndexcs == 3)))
        {
            sceneName = "questionscreen";
        }
        else if (selectedCase == "breast"  && CaseStudyManager.selectedIndexqs == 1)
        {
            sceneName = "questionscreen";
        }
        else if (selectedCase == "crc "&& ((CaseStudyManager.selectedIndexcs == 1 && CaseStudyManager.selectedIndexqs == 1)))
        {
            sceneName = "questionscreen";
        }
        else if (selectedCase == "crc" && ((CaseStudyManager.selectedIndexcs == 1 && CaseStudyManager.selectedIndexis == 1)))
        {
            sceneName = "questionscreen";
        }
        else if(selectedCase =="ifs" && CaseStudyManager.selectedIndexcs == 1)
        {
            sceneName = "summaryscreen";
        }
        else if (selectedCase=="ifs" && CaseStudyManager.selectedIndexis == 2)
        {
            CaseStudyManager.selectedIndexcs = 1;
            sceneName = "questionscreen";
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



    string[] GetCaseDetails(string selectedCase, int selectedIndex)
    {
        // Replace with actual case-specific details retrieval logic

        
        switch (selectedCase)
        {
            case "lung":
                if (selectedIndex == 1)
                {
                    return new string[] { "Many different genetic alterations have been found in lung cancer cells.\r\nMolecular testing is recommended to characterise the type of lung cancer and identify targeted treatment options?\r\n", "Next" };

                }
                else if (selectedIndex == 2)
                {
                    return new string[] { "No gene alteration identified in the specific targets tested by IHC/FISH\r\n→ Large-panel sequencing performed\r\nNGS identified NTRK1-SQSTM1 fusion\r\nKnowing that the patient has CNS disease, what treatment would you select?\r\n", "TRK inhibitor (including clinical trial)\r\n", " Further chemotherapy\r\n", "Radiotherapy for CNS metastases, along with systemic treatment (including TRK inhibitor)\r\n", "Other" };
                }
                else
                {
                    return new string[] { "Treatment options for advanced/metastatic NSCLC include surgery, chemotherapy, radiation therapy, immunotherapy and targeted therapy (TRK inhibitors have shown activity in NTRK fusion-positive NSCLC) 3 6\r\n", "Next" };
                }
            case "headandneck":
                if (selectedIndex == 1)
                {
                    return new string[] { "Standard of care surgery and RT → tumour recurrence after 19 months\r\nWould you perform molecular testing at this stage?\r\n", " No / Yes, selected targets (MYB, EGFR, HER2) via FISH/IHC\r\n", " Yes, selected targets (MYB, EGFR, HER2, CRCT3, TRK) via\r\nFISH/IHC\r\n", "Yes, large-panel NGS" };
                }
                else if ((selectedIndex == 2 && CaseStudyManager.selectedIndexqs == 2) || (selectedIndex == 2 && CaseStudyManager.selectedIndexqs == 3))
                {
                    return new string[] { "Standard of care surgery and RT → tumour recurrence after 19 months\r\nNGS testing, FISH/IHC\r\n→ reclassification as MASC with ETV6-NTRK3 fusion\r\n>80% of MASC cases present an ETV6-NTRK3 fusion. 1-3\r\nIdentifying this fusion in this type of tumour strongly suggests\r\nMASC diagnosis\r\nWhat treatment would you recommend knowing the tumour\r\nhas an ETV6-NTRK3 fusion?\r\n", "TRK inhibitor (including enrolment into clinical trial)\r\n", "Other" };
                }
                else if ((selectedIndex == 3 && CaseStudyManager.selectedIndexqs == 2) || (selectedIndex == 2 && CaseStudyManager.selectedIndexqs == 3))
                {
                    return new string[] { "NGS testing, FISH/IHC\n→ reclassification as MASC with ETV6-NTRK3 fusion\n>80% of MASC cases present an ETV6-NTRK3 fusion. 1-3\nIdentifying this fusion in this type of tumour strongly suggests\nMASC diagnosis\nWhat treatment would you recommend knowing the tumour\nhas an ETV6-NTRK3 fusion?", "TRK inhibitor (including enrolment into clinical trial)\r\n", "Other" };
                }
                else
                {
                    return new string[] { "This patient may have MASC, a rare form of cancer that is often misdiagnosed\r\nas acinic cell carcinoma, mucoepidermoid carcinoma or adenocarcinoma\r\nnot-otherwise-specified.1\r\n>80% of MASC cases present an ETV6-NTRK3 fusion 1-3\r\n", "Explore other options" };
                }
            case "breast":
                if (CaseStudyManager.selectedIndexcs == 2 && CaseStudyManager.selectedIndexqs == 1)
                {
                    return new string[] { ">90% of secretory breast carcinoma cases present an\r\nETV6-NTRK3 fusion. Testing for this fusion could provide useful information and treatment options\r\n", "Explore other options" };
                }
                else if (CaseStudyManager.selectedIndexqs == 1)
                {
                    return new string[] { "This patient could have secretory breast carcinoma, a rare type of cancer that can resemble TNBC. >90% of secretory breast carcinoma cases present an ETV6-NTRK3 fusion'\r\n", "Explore other options" };
                }
                else if (CaseStudyManager.selectedIndexqs == 2 || CaseStudyManager.selectedIndexqs == 3)
                {
                    return new string[] { "• Successful resection of breast mass\n• Molecular testing identified ETV6-NTRK3 gene fusion → Classification as secretory breast carcinoma What treatment would you recommend knowing this is a case of secretory breast cancer with ETV6-NTRK3 fusion ?", " Follow up, no additional treatment\r\n", "TRK inhibitor (including enrolment into clinical trial)\r\n", "Other" };
                }
                else
                {
                    return new string[] { "Molecular testing identified ETV6-NTRK3 gene fusion → Classification as secretory\r\nbreast carcinoma\r\nWhat treatment would you recommend knowing this is a case of secretory breast cancer with ETV6-NTRK3 fusion?\r\n", "Follow up, no additional treatment\r\n", "TRK inhibitor (including enrolment into clinical trial)\r\n", "Other" };
                }
            case "sarcoma":
                if (CaseStudyManager.selectedIndexcs == 2 && CaseStudyManager.selectedIndexqs == 1)
                {
                    return new string[] { "Why not consider large-panel NGS at this stage?\r\n", "The patient is unlikely to present with an actionable mutation\r\n", " I would prefer to start therapy as soon as possible\r\n", "I do not have access to the technology\r\n", "The technology is too expensive" };
                }
                else if (CaseStudyManager.selectedIndexqs == 2)
                {
                    return new string[] { "NGS identified TPM3-NTRK1 gene fusion\nWhat treatment would you recommend knowing the tumor has a TPM3-NTRK1 fusion?", "Further chemotherapy\r\n", "TRK inhibitor\r\n", "Other" };
                }
                else
                {
                    return new string[] { "Chemotherapy, radiotherapy, and, if possible, resection of metastases are the standard treatment options for advanced/metastatic soft tissue sarcoma.\nBroad molecular testing may provide additional treatment options.\nNGS identified TPM3-NTRK1 gene fusion.\nWhat treatment would you recommend knowing the tumor has a TPM3-NTRK1 fusion?", "Further chemotherapy\r\n", "TRK inhibitor (including enrollment into clinical trial)\r\n", "Other" };
                }

            case "crc":
                
                if ((CaseStudyManager.selectedIndexqs==1 && CaseStudyManager.selectedIndexcs==2) || (CaseStudyManager.selectedIndexcs == 1 && CaseStudyManager.selectedIndexis == 1))
                {
                    return new string[] { "Although BRAF/KRAS status is classically tested in CRC to evaluate eligibility for EGFR-targeted antibodies? and MSI-H/MLH/MSH can characterise a somatic inactivation of the same pathway or a germline mutation associated with Lynch sydrome, other mutations are also found in metastatic CRC patients and may direct treatment decisions. For example, the TPM3-NTRK1 gene rearrangement is a recurrent event in CRCo\r\n", "Next" };
                }
                
                else if (CaseStudyManager.selectedIndexcs == 1)
                {
                    CaseStudyManager.selectedcrcop1 = 1;
                    return new string[] { "Patient relapsed after first-line FOLFOX and second-line FOLFIRI/cetuximab\r\nHow would you proceed?\r\n", "Test for mutations / gene fusions\r\n", "Third-line chemotherapy (e.g. irinotecan)" };
                    
                }
                else
                {
                    return new string[] { "LMNA-NTRK1 gene fusion detected by NGS\r\nWhat treatment would you recommend knowing the tumour has an\r\nLMNA-NTRK1 fusion?\r\n", " Further chemotherapy\r\n", "Regorafenib", "TRK inhibitor (including enrolment into clinical trial)\r\n", "Other" };
                }
            case "ifs":
                if (selectedIndex == 1)
                {
                    return new string[] { "Primary excision and complementary radiotherapy and chemotherapy are the historic standard of care for the treatment of infantile fibrosarcoma. 3-5\r\nMore recently, targeted agents such as TRK inhibitors have shown promising results across trials in this disease\r\n", "Next" };
                }
                else
                {
                    return new string[] { "Local relapse after surgery and post-operative vincristine and dactinomycin regimen\nWould you test for NTRK gene fusions at this stage?\r\n", "No", "Yes" };
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
