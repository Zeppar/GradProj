using UnityEngine;

[System.Serializable]
public class DialogueInfo
{
    public string Name;
    [TextArea]
    public string Text;
    public Sprite Icon;

    public DialogueInfo(string _name, string _text, Sprite _icon)
    {
        Name = _name;
        Text = _text;
        Icon = _icon;

    }
}
