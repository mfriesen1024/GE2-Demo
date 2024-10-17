using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    /// <summary>
    /// A target for the player to shoot.
    /// </summary>
    public class DestructableBox : TargetableObject
    {
        Renderer renderer;

        public override void SetTarget(bool active)
        {
            if (active) { renderer.material.color = Color.red; }
            else { renderer.material.color = Color.blue; }
        }

        // Use this for initialization
        void Awake()
        {
            renderer = GetComponent<Renderer>();
            renderer.material.color = Color.blue;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}