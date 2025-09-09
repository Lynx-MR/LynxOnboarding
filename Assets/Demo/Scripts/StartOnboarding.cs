using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartOnboarding : MonoBehaviour
{
    public UnityEvent Starting;


    // Start is called before the first frame update
    void Start()
    {
        Starting.Invoke();   
    }
}
