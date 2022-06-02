using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace K2
{
    public class PointInformation : MonoBehaviour
    {
        private bool _informationActive = false;
        public void CreateInformation()
        {
            if (_informationActive)
            {
                _informationActive = false;
                //Destroy(); окошко
            }
            else
            {
                _informationActive = true;
                //Create() окошко
            }
        }
    }
}