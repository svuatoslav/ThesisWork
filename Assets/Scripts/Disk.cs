using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk : MonoBehaviour
{
    //private Transform[] relatedObjects = new Transform[6];
    public Transform[] RelatedObjects { get; set; } = new Transform[6];
    private float _angelSpeed;
    public float AngelSpeed {
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
                    {
                        if (RelatedObjects[i].GetComponent<Ring>().RelatedObjects[i + this.RelatedObjects.Length - 2].gameObject == gameObject)//magic
                            RelatedObjects[i].GetComponent<Ring>().SpeedOnRim = _speedOnRim;
                        else
                            RelatedObjects[i].GetComponent<Ring>().SpeedOnRim = -_speedOnRim;
                    }
                }
                else if (4 <= i && i < RelatedObjects.Length)
                {
                    if (RelatedObjects[i].CompareTag("Disk"))
                        RelatedObjects[i].GetComponent<Disk>().AngelSpeed = this.AngelSpeed;
                    else if (RelatedObjects[i].CompareTag("Ring"))
                        RelatedObjects[i].GetComponent<Ring>().AngelSpeed = this.AngelSpeed;
                }
            }
        }
    }
    public float Height { get; } = 0.5f;
    private void FixedUpdate()
    {
        Rot();
    }
    private void Rot()
    {
        transform.Rotate(AngelSpeed * Time.fixedDeltaTime * Vector3.up);
    }
    private enum RelativePositionDisk
    {
        _xRighr,
        _xLeft,
        _zRighr,
        _zLeft,
        _yTop,
        _yBot
    }
}
