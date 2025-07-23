using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/New Dialog Container")]
public class DialogueText : ScriptableObject
{
    public string SpeakerName;

    [TextArea(5, 20)]
    public string[] Paragraphs;
}
