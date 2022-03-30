using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasherScr : MonoBehaviour
{
    //private Transform[] relatedObjects = new Transform[6];
    public Transform[] RelatedObjects { get; set; } = new Transform[6];
    public float AngelSpeed { get; set; }
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
    public float Height { get; } = 0.5f;
    private void FixedUpdate()
    {
        Rot();
    }
    private void Rot()
    {
        transform.Rotate(Vector3.up * AngelSpeed * Time.fixedDeltaTime);
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
