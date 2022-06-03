using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace K2
{
    public class Cargo : ReferenceSystem
    {
        private float _acceleration;
        private float _speed;
        private float _const2;
        private float _const1;
        private float _const0;
        private float _x;
        public Transform[] RelatedObjects
        {
            get => _relatedObjects;
            set => _relatedObjects = value;
        }
        private Transform[] _relatedObjects;
        private void Start()
        {
            _relatedObjects = new Transform[1];
        }
        private void FixedUpdate()
        {
            Move();
            //transform.Translate()
            //Play();
            //transform.position = NewCoordinates(transform.position.x, transform.position.z, angle);//add time? local coordinates
        }
        //private Vector3 NewCoordinates(float X, float Z, float alfa)
        //{
        //    return new Vector3(X * Mathf.Cos(alfa) + Z * Mathf.Sin(alfa), transform.position.z , X * Mathf.Sin(alfa) + Z * Mathf.Cos(alfa));
        //}
        private protected override void Move()
        {
            //base.Move();  
        }
        private protected override void Rotation()
        {
            //base.Rotation();  
        }
    }
}
