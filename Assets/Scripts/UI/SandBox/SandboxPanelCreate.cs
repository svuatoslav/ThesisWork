using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace K2
{
    public class SandboxPanelCreate : MonoBehaviour
    {
        [SerializeField] private GameObject _panelGreateObject = null;
        [SerializeField] private GameObject _panelGame = null;
        [SerializeField] private GameObject _objectType = null;
        [SerializeField] private GameObject _inputRadius = null;
        [SerializeField] private GameObject _connectionType = null;
        [SerializeField] private GameObject _tapeLength = null;
        [SerializeField] private GameObject _errorInput = null;
        [SerializeField] private GameObject _disk = null;
        [SerializeField] private GameObject _ring = null;
        [SerializeField] private GameObject _cargo = null;
        [SerializeField] private GameObject _cylinder = null;
        [SerializeField] private GameObject _tape = null;

        private bool _firstObjects = true;
        private GameObject _newObject = null;

        private Vector3 _oldposition = Vector3.zero;
        public void GetPosition(Vector3 position)
        {
            _oldposition = position;
        }
        public void ChangeAmoutInputData()
        {
            if (_objectType.GetComponent<Dropdown>().value == 0)
            {
                _inputRadius.SetActive(true);
                _tapeLength.SetActive(false);
                _connectionType.SetActive(true);
            }
            else if (_objectType.GetComponent<Dropdown>().value == 1)
            {
                _inputRadius.SetActive(true);
                _tapeLength.SetActive(false);
                _connectionType.SetActive(true);
            }
            else if (_objectType.GetComponent<Dropdown>().value == 2)
            {
                //_inputRadius.transform.GetChild(3).gameObject.GetComponent<Text>().text = "??????? ?????? ?????? ? ????";
                _inputRadius.SetActive(false);
                _tapeLength.SetActive(true);
                _connectionType.SetActive(false);
            }
            else if (_objectType.GetComponent<Dropdown>().value == 3)
            {
                //_inputRadius.transform.GetChild(3).gameObject.GetComponent<Text>().text = "??????? ?????? ?????? ? ????";
                _inputRadius.SetActive(true);
                _tapeLength.SetActive(false);
                _connectionType.SetActive(false);
            }
        }
        public void CreateObject()// ????????? ??????? ???????? ? ????????
        {
            float number = Check(_inputRadius.GetComponent<InputField>().text);
            float number1 = Check(_tapeLength.GetComponent<InputField>().text);
            if (_connectionType.GetComponent<Dropdown>().value == 0)
            {
                Camera.main.GetComponent<SelectObjects>().ConnectionType = true;
            }
            else
                Camera.main.GetComponent<SelectObjects>().ConnectionType = false;
            if (number > 0 || number1 >= 0)
            {
                if (_objectType.GetComponent<Dropdown>().value == 0)
                {
                    _newObject = gameObject.GetComponent<Creator>().GetData(number, _disk, _firstObjects);
                    Camera.main.GetComponent<SelectObjects>().RadiusNewObject = number;
                }
                else if (_objectType.GetComponent<Dropdown>().value == 1)
                {
                    _newObject = gameObject.GetComponent<Creator>().GetData(number, _ring, _firstObjects);
                    Camera.main.GetComponent<SelectObjects>().RadiusNewObject = number;
                }
                else if (_objectType.GetComponent<Dropdown>().value == 2)
                {
                    _newObject = gameObject.GetComponent<Creator>().GetData(number1, _cargo, _firstObjects);
                    Camera.main.GetComponent<SelectObjects>().RadiusNewObject = number1;
                }
                else if (_objectType.GetComponent<Dropdown>().value == 3)
                {
                    _newObject = gameObject.GetComponent<Creator>().GetData(number1, _cylinder, _firstObjects);
                    Camera.main.GetComponent<SelectObjects>().RadiusNewObject = number1;
                }
                Camera.main.GetComponent<SelectObjects>().NewObject = _newObject;
                _firstObjects = false;
                _panelGreateObject.SetActive(false);
                _panelGame.SetActive(true);
                _errorInput.SetActive(false);
                Camera.main.transform.position = _oldposition;
            }
            else
            {
                _errorInput.SetActive(true);
            }
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
}
