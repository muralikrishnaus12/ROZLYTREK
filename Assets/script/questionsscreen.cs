using UnityEngine;
using UnityEngine.UI;

public class QuestionScreen : MonoBehaviour
{
    public Text[] textBoxes;    // Array to hold references to the text boxes
    public Image questionImage; // Reference to the sprite
    public GameObject buttonPrefab; // Reference to the button prefab
    public Transform buttonContainer; // Reference to the container for generated buttons

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

        // Update the first three text boxes
        for (int i = 0; i < 3 && i < questionDetails.Length; i++)
        {
            textBoxes[i].text = questionDetails[i];
        }

        // Clear the rest of the text boxes
        for (int i = 3; i < textBoxes.Length; i++)
        {
            textBoxes[i].text = "";
        }

        // Generate buttons for the remaining question details
        for (int i = 3; i < questionDetails.Length; i++)
        {
            Debug.Log("Creating button for: " + questionDetails[i]); // Debug log

            GameObject newButton = Instantiate(buttonPrefab, buttonContainer);
            if (newButton != null)
            {
                newButton.GetComponentInChildren<Text>().text = questionDetails[i];
                int index = i; // Capture the current value of i
                newButton.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(selectedCase, index));
            }
            else
            {
                Debug.LogError("Failed to instantiate button for: " + questionDetails[i]);
            }
        }

        questionImage.sprite = GetQuestionSprite(selectedCase);
    }

    void OnButtonClick(string selectedCase, int index)
    {
        Debug.Log("Button " + index + " clicked.");

        // Prepare data to be sent to the next scene
        CaseStudyManager.selectedCase = selectedCase;
        CaseStudyManager.selectedIndex = index; // Store the index instead of the isCorrectOption

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

    string[] GetQuestionDetails(string selectedCase)
    {
        int selectedIndex = CaseStudyManager.selectedIndex;

        // Replace these with actual details for each case
        switch (selectedCase)
        {
            case "lung":
                if (selectedIndex == 3)
                {
                    return new string[] { "LUNG", "55-year-old male with marked shortness of breath.", "Why not consider large-panel NGS at this stage?\r\n", "The patient is unlikely to present an actionable mutation\r\n", "I would prefer to start the patient on therapy as soon as possible\r\n", "I do not have access to the technology\r\n", "The technology is too expensive" };
                }
                else if (selectedIndex == 3)
                {
                    return new string[] { "LUNG", "55-year-old male with marked shortness of breath.", "Although these targets represent the most common forms of lung cancer, several other molecular targets such as NTRK are known to present somatic variants with therapeutic potentials,4","next" };
                }
                else
                {
                    return new string[] { "LUNG", "55-year-old male with marked shortness of breath.", "Further details about the lung case question.", "Option 1", "Option 2", "Option 3" };
                }
            case "headandneck":
                if (selectedIndex == 3)
                {
                    return new string[] { "Head And Neck", "42-year-old female with large, painful mass and skin infiltration in the parotid area\r\n", "Further details about the head and neck case question.", "Correct Option", "Incorrect Option 1", "Incorrect Option 2" };
                }
                else
                {
                    return new string[] { "Head And Neck", "42-year-old female with large, painful mass and skin infiltration in the parotid area\r\n", "Further details about the head and neck case question.", "Option 1", "Option 2", "Option 3" };
                }
            case "breast":
                if (selectedIndex == 4)
                {
                    return new string[] { "breast", "25-year-old female with bloody discharge from left nipple for\v6 months\r\n", "Further details about the breast case question.", "Correct Option", "Incorrect Option 1", "Incorrect Option 2" };
                }
                else
                {
                    return new string[] { "breast", "25-year-old female with bloody discharge from left nipple for\v6 months\r\n", "Further details about the breast case question.", "Option 1", "Option 2", "Option 3" };
                }
            case "sarcoma":
                if (selectedIndex == 3)
                {
                    return new string[] { "sarcoma", "48-year-old male with a palpable and painful lump on the right thigh", "Further details about the sarcoma case question.", "Correct Option", "Incorrect Option 1", "Incorrect Option 2" };
                }
                else
                {
                    return new string[] { "sarcoma", "48-year-old male with a palpable and painful lump on the right thigh", "Further details about the sarcoma case question.", "Option 1", "Option 2", "Option 3" };
                }
            case "crc":
                if (selectedIndex == 3)
                {
                    return new string[] { "crc", "68-year-old female with progressive fatigue, rectal bleeding and severe abdominal pain", "Further details about the CRC case question.", "Correct Option", "Incorrect Option 1", "Incorrect Option 2" };
                }
                else
                {
                    return new string[] { "crc", "68-year-old female with progressive fatigue, rectal bleeding and severe abdominal pain", "Further details about the CRC case question.", "Option 1", "Option 2", "Option 3" };
                }
            case "ifs":
                if (selectedIndex == 3)
                {
                    return new string[] { "ifs", "8-year-old male with a rapidly\r\n growing mass on the right hand", "Further details about the IFS case question.", "Correct Option", "Incorrect Option 1", "Incorrect Option 2" };
                }
                else
                {
                    return new string[] { "ifs", "8-year-old male with a rapidly\r\n growing mass on the right hand", "Further details about the IFS case question.", "Option 1", "Option 2", "Option 3" };
                }
            default:
                return new string[] { "Unknown case question" };
        }
    }

    Sprite GetQuestionSprite(string selectedCase)
    {
        // Use the same sprites as in the CaseStudyScreen script
        switch (selectedCase)
        {
            case "lung":
                return questionImage.sprite; // Assuming questionImage.sprite is a public reference to the lung sprite
            case "headandneck":
                return questionImage.sprite; // Assuming questionImage.sprite is a public reference to the head and neck sprite
            case "breast":
                return questionImage.sprite; // Assuming questionImage.sprite is a public reference to the breast sprite
            case "sarcoma":
                return questionImage.sprite; // Assuming questionImage.sprite is a public reference to the sarcoma sprite
            case "crc":
                return questionImage.sprite; // Assuming questionImage.sprite is a public reference to the CRC sprite
            case "ifs":
                return questionImage.sprite; // Assuming questionImage.sprite is a public reference to the IFS sprite
            default:
                return null;
        }
    }
}
