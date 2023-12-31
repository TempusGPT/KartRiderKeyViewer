using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorInput : MonoBehaviour
{
    [Header("Targets")]
    [SerializeField]
    private Image preview;

    [SerializeField]
    private TMP_InputField hex;

    [Header("Properties")]
    [SerializeField]
    private Color color;

    [SerializeField]
    private UnityEvent<Color> onColorChanged;

    public Color Color
    {
        get => color;
        set
        {
            color = value;
            onColorChanged?.Invoke(color);
            ApplyColor();
        }
    }

    public void HandleValueChanged()
    {
        hex.text = Regex.Replace(hex.text, "[^0-9a-fA-F]", "").ToUpper();
    }

    public void HandleEndEdit()
    {
        if (ColorUtility.TryParseHtmlString($"#{hex.text}", out var newColor))
        {
            Color = newColor;
        }
    }

    private void Awake()
    {
        ApplyColor();
    }

    private void ApplyColor()
    {
        preview.color = color;
        hex.text = ColorUtility.ToHtmlStringRGB(color);
    }
}
