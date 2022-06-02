using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace K2
{
    public class CameraMove : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;
        private Vector3 _direction = Vector3.zero;
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
            var speed = _speed * Time.fixedDeltaTime * _direction;
            transform.Translate(speed);
        }
    }
}
