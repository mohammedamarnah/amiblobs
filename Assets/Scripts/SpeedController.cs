using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpeedController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {
    public void OnPointerDown(PointerEventData _) {
        AmebaMovement.speed = 0.3f;
    }
    public void OnPointerUp(PointerEventData _) {
        AmebaMovement.speed = 0.1f;
    }
}
