using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimationEventHandler : MonoBehaviour
{
    public event Action onFinish;

    public void InvokeOnFinish()
    {
        onFinish?.Invoke();
    }

}
