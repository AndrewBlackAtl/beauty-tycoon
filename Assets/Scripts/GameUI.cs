using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TopPanelUI topPanelUI;
    [SerializeField] private WorkdayResultPopup workdayResultPopup;
    

    public TopPanelUI TopPanelUI => topPanelUI;
    public WorkdayResultPopup WorkdayResultPopup => workdayResultPopup;
}