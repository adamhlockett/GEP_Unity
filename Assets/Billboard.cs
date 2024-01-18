using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera _camera;
    private void LateUpdate() // happens after other updates
    {
        transform.forward = _camera.transform.forward;
    }
}
