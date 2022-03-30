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
    private float _radius;
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
                AngelSpeed = _speedOnRim / _radius;
                for (int i = 0; i < RelatedObjects.Length; i++)
                {
                    if (RelatedObjects[i] != null)
                    {
                        if (i < 4)
                        {
                            if (RelatedObjects[i].tag == "Washer")
                                RelatedObjects[i].parent.GetComponent<WasherScr>().SpeedOnRim = -_speedOnRim;
                            else if (RelatedObjects[i].tag == "RingWithBottom")
                                RelatedObjects[i].parent.GetComponent<Ring>().SpeedOnRim = -_speedOnRim;
                            else if (RelatedObjects[i].tag == "Disk")
                                RelatedObjects[i].GetComponent<WasherScr>().SpeedOnRim = -_speedOnRim;
                            else
                                RelatedObjects[i].GetComponent<Ring>().SpeedOnRim = -_speedOnRim;
                        }
                        else
                        {
                            if (RelatedObjects[i].tag == "Washer")
                                RelatedObjects[i].parent.GetComponent<WasherScr>().AngelSpeed = this.AngelSpeed;
                            else if (RelatedObjects[i].tag == "RingWithBottom")
                                RelatedObjects[i].parent.GetComponent<Ring>().AngelSpeed = this.AngelSpeed;
                            else if (RelatedObjects[i].tag == "Disk")
                                RelatedObjects[i].GetComponent<WasherScr>().AngelSpeed = this.AngelSpeed;
                            else
                                RelatedObjects[i].GetComponent<Ring>().AngelSpeed = this.AngelSpeed;
                        }

                    }
                }
            }
        }
    }
    public float AngelSpeed { get; set; }

    //private float _height;
    public float Height { get; } = 1.1f;
    //public float Height
    //{
    //    get => _height;
    //    set
    //    {
    //        if (_height == 0f)
    //            _height = value;
    //    }
    //}
    private void FixedUpdate()
    {
        Rot();
    }
    private void Rot()
    {
        transform.Rotate(Vector3.up * AngelSpeed * Time.fixedDeltaTime);
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
