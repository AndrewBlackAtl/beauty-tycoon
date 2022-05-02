using TMPro;
using UnityEditor;
using UnityEngine;

public class DesiredOptionPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void SetOption(int option) 
    {
        text.text = option.ToString();
    }

    public void SetActive(bool value) 
    {
        gameObject.SetActive(value);
    }
}