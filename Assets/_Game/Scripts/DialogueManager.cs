using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("Text settings")]
    [Tooltip("Put the TMPro name of NPC in here for it to be displayed")]
    public TextMeshProUGUI _nameText;
    [Tooltip("Use this to adjust text color and opacity of the text related to the NPC's name")]
    [SerializeField] private Color _nameColor;
    [Tooltip("Put the TMPro dialogue of NPC in here for it to be displayed")]
    public TextMeshProUGUI _dialogueText;
    [Tooltip("Use this to adjust text color and opacity of the text related to the NPC's dialogue")]
    [SerializeField] private Color _dialogueTextColor;

    [Header("Dialogue box settings")]
    [Tooltip("Place the dialogue box in here to access it. MUST BE A IMAGE.")]
    public Image _dialogueBox;
    [Tooltip("Use this to change the color and opacity of the dialogue box")]
    [SerializeField] private Color _dialogueBoxColor;
    [Tooltip("Affects how fast text moves, make the number high to go slower, make it low to go faster.")]
    public float _characterDelay = 0.05f;
    [Tooltip("Animator used to make the dialogue box appear and disappear via a side swipe")]
    public Animator _animator;
    

    public Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    private void Update()
    {
        _nameText.color = _nameColor;
        _dialogueText.color = _dialogueTextColor;
        _dialogueBox.color = _dialogueBoxColor;
    }

    public void StartDialogue (Dialogue dialogue)
    {
        _animator.SetBool("IsOpen", true);

        _nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence, _characterDelay));
    }

    IEnumerator TypeSentence (string sentence, float letterDelay)
    {
        _dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            _dialogueText.text += letter;
            yield return new WaitForSeconds(letterDelay);
        }
    }

    void EndDialogue()
    {
        _animator.SetBool("IsOpen", false);
    }
}
