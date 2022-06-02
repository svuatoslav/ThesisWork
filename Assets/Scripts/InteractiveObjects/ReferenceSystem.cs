using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace K2
{
    public abstract class ReferenceSystem : MonoBehaviour
    {
        private protected abstract void Move();
        private protected abstract void Rotation();
    }
}
