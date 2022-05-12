using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SandboxPanelCreate : MonoBehaviour
{
    [SerializeField] private GameObject _panelGreateObject = null;
    [SerializeField] private GameObject _panelGame = null;
    [SerializeField] private GameObject _objectType = null;
    [SerializeField] private GameObject _inputRadius = null;
    [SerializeField] private GameObject _errorInput = null;
    [SerializeField] private GameObject _disk  = null;
    [SerializeField] private GameObject _ring  = null;

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
            _inputRadius.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Введите радиус Шайбы";
        }
        else if(_objectType.GetComponent<Dropdown>().value == 1)
        {
            _inputRadius.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Введите радиус Кольца с дном";
        }
    }
    public void CreateObject()// Исправить порядок проверки и действий
    {
        float number = Check(_inputRadius.GetComponent<InputField>().text);
        if (number > 0)
        {
            if (_objectType.GetComponent<Dropdown>().value == 0)
            {
                _newObject = gameObject.GetComponent<Creator>().GetData(number, _disk, _firstObjects);
            }
            else if (_objectType.GetComponent<Dropdown>().value == 1)
            {
                _newObject = gameObject.GetComponent<Creator>().GetData(number, _ring, _firstObjects);
            }
            Camera.main.GetComponent<SelectObjects>().NewObject = _newObject;
            Camera.main.GetComponent<SelectObjects>().RadiusNewObject = number;
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
        if (float.TryParse(text,out float number) == true)
        {
            return number;
        }
        else
            return 0;
    }
}
