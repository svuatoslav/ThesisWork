using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelScr : MonoBehaviour
{
    [SerializeField] private Mesh _meshWasher = null;
    [SerializeField] private Material _material = null;
    [SerializeField] private List<GameObject> _cylinders = null;
    private GameObject _washer = null;
    private GameObject _gameObject = null;
    public void Washer()
    {
        for (int i = 0; i < _cylinders.Count; i++)
        {
            if(_cylinders[i].transform.childCount != 0)
            {
                if (_cylinders[i].transform.GetChild(0).tag != "Washer")
                {
                    _washer = new GameObject();
                    _washer.AddComponent<MeshFilter>();
                    _washer.AddComponent<MeshRenderer>();
                    _washer.GetComponent<MeshFilter>().mesh = _meshWasher;
                    _washer.GetComponent<MeshRenderer>().material = _material;

                    _washer.transform.position = _cylinders[i].transform.position - new Vector3(0f, 0f, 0.5f);
                    _washer.transform.rotation = _cylinders[i].transform.rotation;
                    _washer.transform.parent = _cylinders[i].transform;
                    _washer.name = "Washer";
                    _washer.tag = "Washer";
                }
            }
            else
            {
                /*
                _washer = new GameObject();
                _washer.AddComponent<MeshFilter>();
                _washer.AddComponent<MeshRenderer>();
                _washer.GetComponent<MeshFilter>().mesh = _meshWasher;
                _washer.GetComponent<MeshRenderer>().material = _material;
                _washer.transform.position = _cylinders[i].transform.position - new Vector3(0f, 0f, 0.5f);
                _washer.transform.rotation = _cylinders[i].transform.rotation;
                _washer.transform.parent = _cylinders[i].transform;
                _washer.name = "Washer";
                _washer.tag = "Washer";
                */
            }
            
            
        }
    }
    public GameObject CreateGameObject()
    {
        return _gameObject;
    }
    public void InfGameObject(GameObject gameObject)
    {
        _gameObject = gameObject;
    }
    
}
