using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDisableAfterAnimation : MonoBehaviour {

    /// <summary>
    /// Intended to be called by an animation event. Disables the gameobject it is attached to.
    /// </summary>
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
