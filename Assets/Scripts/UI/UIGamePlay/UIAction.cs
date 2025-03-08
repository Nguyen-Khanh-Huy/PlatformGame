using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIAction : MonoBehaviour
{
    [SerializeField] private Button _btnAttact;
    [SerializeField] private Button _btnBullet;
    [SerializeField] private Button _btnFly;
    [SerializeField] private Button _btnJump;

    private void Start()
    {
        RegisterButtonEvents(_btnAttact);
        RegisterButtonEvents(_btnBullet);
        RegisterButtonEvents(_btnFly);
        RegisterButtonEvents(_btnJump);
    }

    //private void RegisterButtonEvents(Button button)
    //{
    //    EventTrigger eventTrigger = button.AddComponent<EventTrigger>();

    //    EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
    //    pointerDownEntry.eventID = EventTriggerType.PointerDown;
    //    pointerDownEntry.callback.AddListener((eventData) => OnPointerDown(button));
    //    eventTrigger.triggers.Add(pointerDownEntry);

    //    EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry();
    //    pointerUpEntry.eventID = EventTriggerType.PointerUp;
    //    pointerUpEntry.callback.AddListener((eventData) => OnPointerUp(button));
    //    eventTrigger.triggers.Add(pointerUpEntry);
    //}

    private void RegisterButtonEvents(Button button)
    {
        EventTrigger eventTrigger = button.gameObject.AddComponent<EventTrigger>();
        AddEventTrigger(eventTrigger, EventTriggerType.PointerDown, (eventData) => OnPointerDown(button));
        AddEventTrigger(eventTrigger, EventTriggerType.PointerUp, (eventData) => OnPointerUp(button));
    }

    private void AddEventTrigger(EventTrigger eventTrigger, EventTriggerType eventType, UnityAction<BaseEventData> action)
    {
        var entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener(action);
        eventTrigger.triggers.Add(entry);
    }

    private void OnPointerDown(Button button) => SetGamePadState(button, true);
    private void OnPointerUp(Button button) => SetGamePadState(button, false);

    private void SetGamePadState(Button button, bool state)
    {
        if (button == _btnAttact) GamePad.Ins.CanAttack = state;
        else if (button == _btnBullet) GamePad.Ins.CanBullet = state;
        else if (button == _btnFly) GamePad.Ins.CanFly = state;
        else if (button == _btnJump)
        {
            GamePad.Ins.CanJump = state;
            GamePad.Ins.CanJumpHolding = state;
        }
    }
}
