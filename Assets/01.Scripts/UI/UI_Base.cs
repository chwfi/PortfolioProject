using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public enum UITypeEnum
{
    PopupUI,
    Button,
}

public class UI_Base : MonoBehaviour
{
    [Header("UI Type")]
    public UITypeEnum UIType;
}
