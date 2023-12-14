using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Device:MonoBehaviour
{
    static public bool isMobile;
    void Start() {
        isMobile = false;
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
        Application.platform == RuntimePlatform.Android) {
            isMobile = true;
        }
    }
}