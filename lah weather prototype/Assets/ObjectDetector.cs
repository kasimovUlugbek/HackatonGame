using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    public List<Collider> colliders;

    private void OnTriggerEnter(Collider other)
    {
        colliders.Add(other);
    }
    private void OnTriggerExit(Collider other)
    {
        colliders.Remove(other);
    }
}
