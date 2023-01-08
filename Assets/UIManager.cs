using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject[] uiElements;

    public void HideUIElements()
    {
        foreach (GameObject element in uiElements)
        {
            element.SetActive(false);
        }
    }
}
