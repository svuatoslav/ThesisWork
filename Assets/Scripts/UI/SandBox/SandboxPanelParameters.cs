using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SandboxPanelParameters : MonoBehaviour
{
    [SerializeField] private GameObject _panelParameters = null;
    [SerializeField] private GameObject _panelGame = null;
    [SerializeField] private GameObject _inputRotationSpeed = null;
    public void SaveParameters()
    {
        float number = Check(_inputRotationSpeed.GetComponent<InputField>().text);
        Camera.main.GetComponent<SelectObjects>().AngelSpeed = number;
        _panelParameters.SetActive(false);
        _panelGame.SetActive(true);
    }
    private float Check(string text)
    {
        if (float.TryParse(text, out float number) == true)
        {
            return number;
        }
        else
            return 0;
    }
}
