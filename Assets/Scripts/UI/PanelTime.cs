using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace K2
{
    public class PanelTime : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log(Time.realtimeSinceStartup);
            Debug.Log(Time.timeSinceLevelLoad);
        }

    }
}