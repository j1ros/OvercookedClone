using System;
using System.Collections.Generic;
using UnityEngine;

namespace Overcooked
{
    public class EventManager : MonoBehaviour
    {
        private Dictionary<EventType, Action<Dictionary<EventMessageType, object>>> eventDictionary;

        private static EventManager eventManager;

        public static EventManager instance
        {
            get
            {
                if (!eventManager)
                {
                    eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                    if (!eventManager)
                    {
                        Debug.LogError("Event manager not active!!!");
                    }
                    else
                    {
                        eventManager.Init();
                        DontDestroyOnLoad(eventManager);
                    }
                }
                return eventManager;
            }
        }

        void Init()
        {
            if (eventDictionary == null)
            {
                eventDictionary = new Dictionary<EventType, Action<Dictionary<EventMessageType, object>>>();
            }
        }

        public static void StartListening(EventType eventName, Action<Dictionary<EventMessageType, object>> listener)
        {
            Action<Dictionary<EventMessageType, object>> thisEvent;

            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent += listener;
                instance.eventDictionary[eventName] = thisEvent;
            }
            else
            {
                thisEvent += listener;
                instance.eventDictionary.Add(eventName, thisEvent);
            }
        }

        public static void StopListening(EventType eventName, Action<Dictionary<EventMessageType, object>> listener)
        {
            if (eventManager == null) return;
            Action<Dictionary<EventMessageType, object>> thisEvent;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent -= listener;
                instance.eventDictionary[eventName] = thisEvent;
            }
        }

        public static void TriggerEvent(EventType eventName, Dictionary<EventMessageType, object> message)
        {
            Action<Dictionary<EventMessageType, object>> thisEvent = null;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke(message);
            }
        }
    }
}
