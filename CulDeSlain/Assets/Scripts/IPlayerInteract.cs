using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlayerInteract {

    public void OnInteract();

    public void Interact();

    public void OnInteractCanceled();
}
