using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    //private Transform[] _relatedObjects = new Transform[8];
    public Transform[] RelatedObjects { get; set; } = new Transform[8];
    public float Radius
    {
        get => _radius;
        set
        {
            if (_radius == 0f)
                _radius = value;
        }
    }
    [SerializeField] private float _radius;
    //private float _F = 0f;
    private float _speedOnRim;
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
    private float _angelSpeed;
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
    public float Height { get; } = 1.1f;
    private void FixedUpdate()
    {
        Rot();
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
}
