using UnityEngine;

[CreateAssetMenu(fileName = "Text", menuName = "Localization/Text")]
public class TextAsset : ScriptableObject
{
    [SerializeField] [TextArea] private string text;

    public string Text => text;
}
