using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    /// <summary>
    /// An object that the player can target, whether to fire upon, or interact with.
    /// </summary>
    public abstract class TargetableObject : MonoBehaviour
    {
        public abstract void SetTarget(bool state);
    }
}