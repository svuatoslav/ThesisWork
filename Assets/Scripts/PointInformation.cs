using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInformation : MonoBehaviour
{
    private bool _informationActive = false;
    public void CreateInformation()
    {
        if(_informationActive)
        {
            _informationActive = false;
            //Destroy(); ������
        }
        else
        {
            _informationActive = true;
            //Create() ������
        }
    }
}
