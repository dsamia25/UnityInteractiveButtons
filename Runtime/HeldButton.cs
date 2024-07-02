using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HeldButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public float holdTime = 2f;             // How long the button needs to be held before being activated.
    public float decayTime = 0f;            // How long it takes for the button's fill to return to empty after an early release.
    public float decayDelayTime = 0f;       // How long it should wait before starting to decay the timer.
    public bool releaseOnExit = false;      // If the button release when the mouse exits the ui element.
    public bool autoReset = true;           // If the button resets itself after being activated.
    public float resetTime = 1f;            // How long it takes the for button to reset itself if autoReset is enabled.

    public bool IsHeld { get; private set; }
    public float HeldTime => Time.time - _holdTime;
    public float HeldPercent
    {
        get
        {
            // If no hold time, just return 100% if held at all.
            if (holdTime <= 0 && IsHeld) return 1.0f;
            
            var percent = (HeldTime) / holdTime;
            return percent >= 1.0f ? 1.0f : percent;
        }
    }

    public GameObject fillLayer;
    private float _fill = 0f;               // The current fill percent of the button.
    private float _resetTimer = 0f;         // How much longer until the button is reset.
    private float _holdTime = 0f;
    
    public UnityEvent OnCompleteHold;       // Event triggered when the button is released after being held filled.
    public UnityEvent OnEarlyRelease;       // Event triggered when the button is released without being at full.

    private void Update()
    {
        if (IsHeld)
        {
            // Increase hold time counter.
        } else if (!IsHeld && HeldTime > 0)
        {
            // TODO: Add a decay delay check here.
            // Decay hold time counter.
        }
    }

    public void Hold()
    {
        // TODO: change the hold time to use deltatime instead
        if (IsHeld) return;
        
        IsHeld = true;
        _holdTime = Time.time;
        Debug.Log($"HoldButton: Hold ({_holdTime}).");
    }
    
    public void Release()
    {
        // TODO: change the hold time to use deltatime instead
        if (!IsHeld) return;
        
        Debug.Log($"HoldButton: Released ({Time.time}) after ({HeldTime}).");
        _holdTime = 0;
        IsHeld = false;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("HoldButton: Pointer Enter.");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("HoldButton: Pointer Exit.");
        if (releaseOnExit && IsHeld)
        {
            Release();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("HoldButton: Pointer Down.");
        Hold();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("HoldButton: Pointer Up.");
        Release();
    }
}
