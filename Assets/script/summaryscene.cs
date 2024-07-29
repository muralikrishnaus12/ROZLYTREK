using UnityEngine;
using UnityEngine.UI;

public class SummaryScreen : MonoBehaviour
{
    public Text[] textBoxes;
    public Image caseImage;

    void OnEnable()
    {
        // Update UI whenever the object is enabled
        UpdateUI(CaseStudyManager.selectedCase, CaseStudyManager.selectedIndexis);
    }

    void UpdateUI(string selectedCase, int selectedIndex)
    {
        Debug.Log("Selected case in SummaryScreen: " + selectedCase);

        string[] summaryDetails = GetSummaryDetails(selectedCase, selectedIndex);

        // Update summary details in text boxes
        for (int i = 0; i < summaryDetails.Length && i < textBoxes.Length; i++)
        {
            textBoxes[i].text = summaryDetails[i];
        }

        // Clear remaining text boxes
        for (int i = summaryDetails.Length; i < textBoxes.Length; i++)
        {
            textBoxes[i].text = "";
        }

        // Update case image
        caseImage.sprite = GetCaseSprite(selectedCase);
    }

    string[] GetSummaryDetails(string selectedCase, int selectedIndex)
    {
        // Replace with actual summary details retrieval logic
        switch (selectedCase)
        {
            case "lung":
                return new string[] { "Lung Summary", "NSCLC accounts for 85% of lung cancer cases and mainly comprises two histological subtypes: adenocarcinoma (60%) and squamous cell carcinoma (35%)", "Molecular genetic testing for the following targets identifies patients most likely to benefit from specific targeted therapies:", "•EGFR (~10–30% of patients)\r\n•ALK (~5%)3\r\n•ROS1 (~2%)4\r\n•NTRK (~0.1–1%)5\r\n•BRAF (~2–4%)6", "Diagnosis includes:7,8\r\n", "•Clinical examination\r\n•Sputum cytology\r\n•Molecular testing\r\n•Imaging tests (e.g. bronchoscopy)\r\n•Transthoracic fine needle aspiration and/or core biopsy", "•Clinical examination\r\n•Sputum cytology\r\n•Molecular testing\r\n•Imaging tests (e.g. bronchoscopy)\r\n•Transthoracic fine needle aspiration and/or core biopsy" };
            case "headandneck":
                return new string[] { "Head and Neck Summary", "MASC is a rare condition (<0.3% of all salivary gland tumours), difficult to distinguish from other tumours in this category,2", ">80% of MASC cases present an ETV6-NTRK3 fusion; identification of this fusion by molecular testing (NGS, IHC, FISH, RNA-seq) is the gold standard to reach a definitive diagnosis of MASC1-3\r\n", "Diagnosis can also include:4\r\n• Clinical examination\r\n• Imaging assessment: CT, MRI, PET-CT\r\n• Histology, morphology and immunohistochemical staining", "", "", "Treatment options: include surgery, radiation therapy, chemotherapy and targeted therapy (TRK inhibitors have shown activity in NTRK fusion-positive MASC)2,5,6" };
            case "breast":
                return new string[] { "Breast Summary", "Testing molecular characteristics of breast tumours may help to identify the type of cancer and make an informed therapeutic decision", "NTRK fusions are found in more than 25 different cancers and are highly prevalent in secretory breast carcinoma (>90%)'\r\nSecretory breast carcinoma is a rare condition that accounts for <0.1% of all cases of invasive breast cancer?\r\n","","","Key diagnostic features in secretory breast carcinoma:\r\n• Usually triple negative for HER2, ER, PR3\r\n• Histological patterns including solid, microcystic, and tubular\r\n• Molecular testing for the ETV6-NTRK3 fusion4", "Treatment options: there is no clear consensus on the treatment of secretory breast cancer, but surgery seems to currently be the primary treatment.? Targeted therapies including TRK inhibitors have shown activity in NTRK fusion-positive breast cancers 6"};
            case "sarcoma":
                return new string[] { "Sarcoma Summary", "Soft tissue sarcomas constitute a group of >50 histology subtypes that can arise from most parts of the body1,2", "Specific driver molecular abnormalities have been identified in up to 40% of soft tissue sarcomas?; <5% of sarcoma cases present NTRK fusions\r\nDiagnosis consists of imaging assessments (MRI, CT or PET) and tissue biopsy to determine histological type, stage and resectability of the tumour1,5", "", "", "", "Surgery (wide excision with negative margins (RO) when possible) is the standard of care for sarcoma, associated with radiation therapy and chemotherapy.1\r\nTargeted agents such as tyrosine kinase inhibitors are becoming important treatment options for soft tissue sarcomas®" };
            case "crc":
                return new string[] { "CRC Summary ", "CRC is a genetically diverse type of cancer that accounts for ~10% of all cancers worldwide 1,2", "Diagnosis includes: 1,3\r\n• Endoscopy\r\n• Imaging (ultrasonography, CT, MRI, CT colonography)\r\n• Molecular testing", "Molecular testing in CRC was historically focused on one or a few targets (MSI-H status, KRAS pathway), but new biomarkers (PIKCA, PTEN) keep emerging. Broad testing methods such as NGS are increasingly used to guide treatment decisions", "", "", "ESMO guidelines recommend systemic therapy and, when possible, local ablative treatment, as first-line treatment for metastatic CRC.\r\nBased on the biomarker profile of the tumour characterised by molecular testing, targeted therapies can be considered as a subsequent line of treatment1,3" };
            case "ifs":
                return new string[] { "IFS Summary ", "Infantile fibrosarcoma is mostly seen in children <2 years of age.1\r\nIt is biologically and clinically different from adult fibrosarcoma1,2", ">90% of infantile fibrosarcoma cases present NTRK gene fusions, in particular the cytogenetic translocation t(12;15)(ETV6-NTRK3)1,3,4", "Diagnosis includes:2\r\n• Imaging (CT, MRI, PET, bone scan, chest CT to detect metastases)\r\n• Core-needle biopsy\r\n• Molecular testing to identify chromosomal abnormalities", "", "", "Primary excision often is the first-line treatment for infantile fibrosarcoma, but can have debilitating effects. 1,2,5 Radiotherapy and chemotherapy may complement surgery in some cases.2,5\r\nTargeted agents such as TRK inhibitors have shown promising results across trials in patients with infantile fibrosarcoma2,6" };
            default:
                return new string[] { "Unknown summary details" };
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
