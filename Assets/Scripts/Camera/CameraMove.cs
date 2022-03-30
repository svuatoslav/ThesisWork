using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    //[SerializeField] private GameObject _panel = null;
    private Vector3 _direction = Vector3.zero;
    //private Ray _ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
    //private RaycastHit _hit;
    private void Update()
    {
        //if (Input.GetMouseButtonDown(2))
        //{
        //    //Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        //    //RaycastHit _hit;
        //    if (Physics.Raycast(_ray, out _hit, Mathf.Infinity))
        //    {
        //        if (_hit.transform.tag == "Point")
        //            _hit.transform.gameObject.GetComponent<PointInformation>().CreateInformation();
        //    }
        //}
        //else if(Input.GetMouseButtonDown(1))
        //{
        //    //Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        //    //RaycastHit _hit;
        //    if (Physics.Raycast(_ray, out _hit, Mathf.Infinity))
        //    {
        //        if (_hit.transform.tag == "Point")
        //        {
        //            if (_hit.transform.childCount == 0)
        //                Create(_hit, _panel.GetComponent<PanelScr>().CreateGameObject());
        //            else
        //                Debug.LogError("шайба здесь уже есть!");
        //            //Destroy(_hit.transform.GetChild(0).gameObject);
        //        }
        //    }
        //}
        _direction.z = Input.GetAxis("Vertical");
        _direction.x = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.E))
            _direction.y = 1;
        else if (Input.GetKey(KeyCode.Q))
            _direction.y = -1;
        else
            _direction.y = 0;

    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        var speed = _direction * Time.fixedDeltaTime * _speed;
        transform.Translate(speed);
    }
    //private void Create(RaycastHit hit, GameObject newGameObject)
    //{
    //    if (newGameObject == null)
    //    {

    //    }
    //    else
    //    {
    //        newGameObject = new GameObject();
    //        newGameObject.transform.position = hit.transform.position - new Vector3(0f, 0f, 0.5f);
    //        newGameObject.transform.rotation = hit.transform.rotation;
    //        newGameObject.transform.parent = hit.transform;
    //    }
    //}
}
