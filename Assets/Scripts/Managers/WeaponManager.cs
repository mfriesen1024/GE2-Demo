using Assets.Scripts.Entities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    /// <summary>
    /// Handles weaponry, target selection and firing behavior.
    /// </summary>
    public class WeaponManager : MonoBehaviour
    {
        [Header("Debug")]
        [SerializeField] bool debugMode = false;

        [Header("Refs")]
        [SerializeField] InputManager inputManager;
        [SerializeField] CameraManager cameraManager;

        // In a more robust system, we'd use a weapon class, but I'm lazy.
        [Header("Weapon Details")]
        [SerializeField] float maxRange = 10;

        List<TargetableObject> targets = new();

        private void Awake()
        {
            if (inputManager == null) { Debug.LogException(new Exception("inputman was null. this bad.")); }
            if (cameraManager == null) { Debug.LogException(new Exception("cameraman was null. this bad.")); }
        }

        private void Update()
        {
            UpdateTargets();
        }

        /// <summary>
        /// Called when we want to update the targets.
        /// </summary>
        public void UpdateTargets()
        {
            // Reset targeting state and then discard.
            foreach (TargetableObject target in targets) { target.SetTarget(false); }
            targets.Clear();

            // Get new references
            Vector3 forward = cameraManager.playerCamera.transform.forward;
            Vector3 right = cameraManager.playerCamera.transform.right;
            DebugHelper(forward, right);
            RaycastHit[] allHits = Physics.RaycastAll(transform.position, forward, maxRange);
            if (debugMode)
            {
                Debug.Log($"Caught {allHits.Length} hits.");
                Debug.DrawRay(transform.position, cameraManager.playerCamera.transform.forward, Color.blue);
            }

            // For every hit, if it has a targetable, add it as a target.
            foreach (RaycastHit hitData in allHits)
            {
                if (hitData.transform.gameObject.TryGetComponent(out TargetableObject target))
                {
                    targets.Add(target);
                    target.SetTarget(true);
                }
            }

            void DebugHelper(Vector3 forward, Vector3 right)
            {
                Debug.DrawRay(transform.position, forward, Color.red);
                Debug.DrawLine(transform.position, transform.position + (forward * maxRange), Color.blue);
                Debug.DrawRay(transform.position, right, Color.red);
                Debug.DrawLine(transform.position, transform.position + (right * maxRange), Color.blue);
            }
        }

        /// <summary>
        /// Called when the player clicks with LMB
        /// </summary>
        public void Fire()
        {
            // Update targets just in case they are out of date.
            UpdateTargets();

            // For each target, if its a box, disable it.
            foreach (TargetableObject target in targets)
            {
                if (target is DestructableBox)
                {
                    target.gameObject.SetActive(false);
                }
            }
        }
    }
}