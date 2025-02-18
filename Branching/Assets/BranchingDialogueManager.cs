using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
public class DialogueNode
{
    public string id;
    public string speaker;
    public string line;
    public List<DialogueChoice> choices;
}

[System.Serializable]
public class DialogueChoice
{
    public string text;
    public string nextNodeId;
}

public class BranchingDialogueManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI dialogueText;
    public GameObject choicesContainer;
    public Button choiceButtonPrefab;

    [Header("Dialogue Data")]
    public List<DialogueNode> dialogueNodes;
    
    private DialogueNode currentNode;

    void Start()
    {
        StartDialogue("node1");
    }

    public void StartDialogue(string startNodeId)
    {
        currentNode = dialogueNodes.Find(node => node.id == startNodeId);
        DisplayNode();
    }

    private void DisplayNode()
    {
        speakerText.text = currentNode.speaker;
        dialogueText.text = currentNode.line;

        foreach (Transform child in choicesContainer.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var choice in currentNode.choices)
{
    Button newButton = Instantiate(choiceButtonPrefab, choicesContainer.transform);
    newButton.GetComponentInChildren<TMPro.TMP_Text>().text = choice.text;


    string nextNode = choice.nextNodeId;  // Store choice in a local variable
    newButton.onClick.AddListener(() => ChooseNextNode(nextNode));
}

    }

    public void ChooseNextNode(string nextNodeId)
    {
        currentNode = dialogueNodes.Find(node => node.id == nextNodeId);
        if (currentNode != null)
        {
            DisplayNode();
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        dialogueText.text = "Dialogue Ended.";
        speakerText.text = "";
        foreach (Transform child in choicesContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
