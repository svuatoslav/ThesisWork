using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace K2
{
    public class SandboxPanelParameters : MonoBehaviour
    {
        [SerializeField] private GameObject _panelParameters = null;
        [SerializeField] private GameObject _panelGame = null;
        [SerializeField] private GameObject _inputRotationSpeed = null;
        [SerializeField] private GameObject _inputAngularAcceleration = null;
        public void SaveParameters()
        {
            float speed = Check(_inputRotationSpeed.GetComponent<InputField>().text);
            float acceleration = Check(_inputAngularAcceleration.GetComponent<InputField>().text);
            if (speed >= 0 && acceleration >= 0)
            {
                Camera.main.GetComponent<SelectObjects>().AngelSpeed = speed;
                _panelParameters.SetActive(false);
                _panelGame.SetActive(true);
                //panel
            }
            else
            {
                //panel
            }
        }
        private float Check(string text)
        {
            if (float.TryParse(text, out float number) == true)
            {
                return number;
            }
            else
                return -1;
        }
    }
}
