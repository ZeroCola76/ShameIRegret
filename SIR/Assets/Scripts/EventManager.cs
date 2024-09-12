using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private static EventManager instance;
    public static EventManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EventManager>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<EventManager>();
                    singletonObject.name = typeof(EventManager).ToString() + " (Singleton)";
                }
            }
            return instance;
        }
    }

    private Dictionary<EventType, List<IObserver>> observers = new Dictionary<EventType, List<IObserver>>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    public void RegisterObserver(EventType eventType, IObserver observer)
    {
        if (!observers.ContainsKey(eventType))
        {
            observers[eventType] = new List<IObserver>();
        }
        if (!observers[eventType].Contains(observer))
        {
            observers[eventType].Add(observer);
        }
    }

    public void RemoveObserver(EventType eventType, IObserver observer)
    {
        if (observers.ContainsKey(eventType) && observers[eventType].Contains(observer))
        {
            observers[eventType].Remove(observer);
        }
    }

    public void NotifyObservers(EventType eventType)
    {
        if (observers.ContainsKey(eventType))
        {
            foreach (var observer in observers[eventType])
            {
                observer.OnNotify(eventType);
            }
        }
    }

    public void SomethingHappens(EventType eventType)
    {
        NotifyObservers(eventType);
    }
}
