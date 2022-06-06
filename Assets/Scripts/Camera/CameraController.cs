using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace K2
{
    public sealed class CameraController : MonoBehaviour
    {
        [SerializeField] Player player = null;
        private void LateUpdate()
        {
            transform.position = player.transform.position;
        }
    }
}
