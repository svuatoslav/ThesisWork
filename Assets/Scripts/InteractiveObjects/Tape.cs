using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace K2
{
    public class Tape : ReferenceSystem
    {
        private float _acceleration;
        private float _speed;
        public Transform[] RelatedObjects
        {
            get => _relatedObjects;
            set => _relatedObjects = value;
        }
        private Transform[] _relatedObjects;
        private void Start()
        {
            _relatedObjects = new Transform[2];
        }
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
