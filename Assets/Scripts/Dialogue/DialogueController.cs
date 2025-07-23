using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NPCNameText;
    [SerializeField] private TextMeshProUGUI NPCDialogueText;
    [SerializeField] private int _typeSpeed = 10;

    private Queue<string> _paragraphs = new();

    private bool _conversationEnded;
    private bool _isTyping;

    private string _text;

    private Coroutine _typeDialogueCoroutine;

    private const string HTML_ALPHA = "<color=#00000000>";
    private const float MAX_TYPE_TIME = 0.1f;
    public void DisplayNextParagraph(DialogueText dialogueText) {
        if (_paragraphs.Count == 0) {
            if (!_conversationEnded) {
                StartConversation(dialogueText);
            } else if (_conversationEnded && !_isTyping) {
                EndConversation();
                return;
            }
        }

        if (!_isTyping) {
            _text = _paragraphs.Dequeue();

            _typeDialogueCoroutine = StartCoroutine(TypeDialogueText(_text));
        } else {
            FinishParagraphEarly();
        }

        if (_paragraphs.Count == 0) {
            _conversationEnded = true;
        }
    }

    private void StartConversation(DialogueText dialogueText) {
        if (!gameObject.activeSelf) {
            gameObject.SetActive(true);
        }

        NPCNameText.text = dialogueText.SpeakerName;

        for (int i = 0; i < dialogueText.Paragraphs.Length; i++) {
            _paragraphs.Enqueue(dialogueText.Paragraphs[i]);
        }
    }

    private void EndConversation() {
        _conversationEnded = false;

        if (gameObject.activeSelf) {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator TypeDialogueText(string text) {
        _isTyping = true;

        NPCDialogueText.text = "";

        string originalText = text;
        string displayedText = "";
        int maskIndex = 0;

        foreach (char c in text.ToCharArray()) {
            maskIndex++;
            NPCDialogueText.text = originalText;

            displayedText = NPCDialogueText.text.Insert(maskIndex, HTML_ALPHA);
            NPCDialogueText.text = displayedText;

            yield return new WaitForSeconds(MAX_TYPE_TIME / _typeSpeed);
        }

        _isTyping = false;
    }

    private void FinishParagraphEarly() {
        StopCoroutine(_typeDialogueCoroutine);
        NPCDialogueText.text = _text;
        _isTyping = false;
    }
}
