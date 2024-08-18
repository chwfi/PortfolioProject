using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupUIController : MonoSingleton<PopupUIController>
{
    public Dictionary<string, UI_Popup> PopupDictionary = new();
    public Stack<UI_Popup> PopupStack = new();

    private void Awake()
    {
        var list = GetComponentsInChildren<UI_Popup>();

        foreach (var popup in list)
        {
            if (!PopupDictionary.ContainsKey(popup.name))
                PopupDictionary.Add(popup.name, popup);
        }
    }

    public UI_Popup GetPopupUI(string popupName)
    {
        return PopupDictionary[popupName];
    }
}
