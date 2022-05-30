using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Ring : ReferenceSystem
{
    public Transform[] RelatedObjects
    {
        get => _relatedObjects;
        set => _relatedObjects = value;
    }
    private Transform[] _relatedObjects;
    private void Start()
    {
        _relatedObjects = new Transform[8];
    }
    public float Height { get; } = 1.1f;

    private float _radius;
    private float _speedOnRim;
    private float _angelSpeed;
    public float Radius
    {
        get => _radius;
        set
        {
            if (_radius == 0f)
                _radius = value;
        }
    }
    public float SpeedOnRim
    {
        get => _speedOnRim;
        set
        {
            if (_speedOnRim != value)
            {
                _speedOnRim = value;
                _angelSpeed = _speedOnRim / _radius;
                Debug.LogWarning(gameObject.name);
                SetMovementScheme();
            }
        }
    }
    public float AngelSpeed
    { 
        get => _angelSpeed;
        set
        {
            if (_angelSpeed != value)
            {
                _angelSpeed = value;
                _speedOnRim = _angelSpeed * _radius;
                Debug.LogWarning(gameObject.name);
                SetMovementScheme();
            }
        }
    }
    private void SetMovementScheme()
    {
        for (int i = 0; i < RelatedObjects.Length; i++)
        {
            if (RelatedObjects[i] != null)
            {
                if (i < 4)//magic
                {
                    if (RelatedObjects[i].CompareTag("Disk"))
                        RelatedObjects[i].GetComponent<Disk>().SpeedOnRim = -_speedOnRim;
                    else if (RelatedObjects[i].CompareTag("Ring"))
                        RelatedObjects[i].GetComponent<Ring>().SpeedOnRim = -_speedOnRim;
                }
                else if (4 <= i && i < RelatedObjects.Length)
                {
                    if (RelatedObjects[i].CompareTag("Disk"))
                        RelatedObjects[i].GetComponent<Disk>().SpeedOnRim = _speedOnRim;
                    else if (RelatedObjects[i].CompareTag("Ring"))
                        RelatedObjects[i].GetComponent<Ring>().SpeedOnRim = _speedOnRim;
                }
                //else
                //{
                //    if (RelatedObjects[i].CompareTag("Disk"))
                //        RelatedObjects[i].GetComponent<Disk>().AngelSpeed = this.AngelSpeed;
                //    else if (RelatedObjects[i].CompareTag("Ring"))
                //        RelatedObjects[i].GetComponent<Ring>().AngelSpeed = this.AngelSpeed;
                //}
            }
        }
    }
    private void FixedUpdate()
    {
        Rot();
        Move();
        //Play();
    }
    private void Rot()
    {
        transform.Rotate(AngelSpeed * Time.fixedDeltaTime * Vector3.up);
    }
    private enum RelativePositionRWB//связать с SpeedOnRim
    {
        _xRighrOut,
        _xLeftOut,
        _zRighrOut,
        _zLeftOut,
        _xRighrIn,
        _xLeftIn,
        _zRighrIn,
        _zLeftIn,
    }
    public Ring(float radius)
    {
        _radius = radius;
    }
    private protected override void Rotation()
    {
        //base.Rotation();  
    }
    private protected override void Move()
    {
        //base.Move();  
    }
}
