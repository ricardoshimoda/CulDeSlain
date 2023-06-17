using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class LockOnComponent : MonoBehaviour, IPlayerLockOn {

    [Header("Components")]
    [SerializeField] private Transform LockOnTarget;
    [SerializeField] private CharacterController Controller;

    public void PlayerLockOn() {
        GameObject.Find("Player").GetComponent<PlayerCharacter>().SetZoomTarget(LockOnTarget);
    }

    public void ReleasePlayer() {
        GameObject.Find("Player").GetComponent<PlayerCharacter>().ResetZoomTarget();
    }

    public void OnTriggerEnter(Collider other) {
        if (other.transform == GameObject.Find("Player").transform)
            PlayerLockOn();
    }

    public void OnTriggerExit(Collider other) {
        if (other.transform == GameObject.Find("Player").transform)
            ReleasePlayer();
    }
}
