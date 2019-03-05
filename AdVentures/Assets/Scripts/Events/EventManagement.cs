using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Events
{
    private static Dictionary<GameEventsEnum, EventControllerBase> actionList = new Dictionary<GameEventsEnum, EventControllerBase>();

    #region Raise
    public static void Raise(GameEventsEnum name)// where T : struct
    {
        AddIfNotExists(name);

        actionList[name].Raise();
    }

    public static void Raise<T>(T data, GameEventsEnum name)// where T : struct
    {
        AddIfNotExists<T>(name);

        actionList[name].Raise(data);
    }

    public static void Raise<T, V>(T data, V data2, GameEventsEnum name)// where T : struct
    {
        AddIfNotExists<T, V>(name);
        actionList[name].Raise(data, data2);
    }

    public static void Raise<T, V, U>(T data, V data2, U data3, GameEventsEnum name)// where T : struct
    {
        AddIfNotExists<T, V, U>(name);
        actionList[name].Raise(data, data2, data3);
    }

    public static void Raise<T, V, U, X>(T data, V data2, U data3, X data4, GameEventsEnum name)// where T : struct
    {
        AddIfNotExists<T, V, U>(name);
        actionList[name].Raise(data, data2, data3, data4);
    }
    #endregion

    #region Register
    public static void Register(GameEventsEnum name, Action callback)
    {
        AddIfNotExists(name);
        actionList[name].Add(callback);
    }

    public static void Register<T>(GameEventsEnum name, Action<T> callback)
    {
        AddIfNotExists<T>(name);
        actionList[name].Add(callback);
    }

    public static void Register<T, V>(GameEventsEnum name, Action<T, V> callback)
    {
        AddIfNotExists<T, V>(name);
        actionList[name].Add(callback);
    }

    public static void Register<T, V, U>(GameEventsEnum name, Action<T, V, U> callback)
    {
        AddIfNotExists<T, V, U>(name);
        actionList[name].Add(callback);
    }

    public static void Register<T, V, U, X>(GameEventsEnum name, Action<T, V, U, X> callback)
    {
        AddIfNotExists<T, V, U, X>(name);
        actionList[name].Add(callback);
    }
    #endregion

    #region AddIfNotExists
    private static void AddIfNotExists(GameEventsEnum name)
    {
        if (actionList.ContainsKey(name) == false)
            actionList[name] = new EventController();
    }

    private static void AddIfNotExists<T>(GameEventsEnum name)
    {
        if (actionList.ContainsKey(name) == false)
            actionList[name] = new EventController<T>();
    }

    private static void AddIfNotExists<T, V>(GameEventsEnum name)
    {
        if (actionList.ContainsKey(name) == false)
            actionList[name] = new EventController<T, V>();
    }

    private static void AddIfNotExists<T, V, U>(GameEventsEnum name)
    {
        if (actionList.ContainsKey(name) == false)
            actionList[name] = new EventController<T, V, U>();
    }

    private static void AddIfNotExists<T, V, U, X>(GameEventsEnum name)
    {
        if (actionList.ContainsKey(name) == false)
            actionList[name] = new EventController<T, V, U, X>();
    }

    #endregion

    public static void StartCoroutine(IEnumerator coroutine, MonoBehaviour behaviorToRunOn)
    {
        behaviorToRunOn.StartCoroutine(coroutine);
    }
}

public abstract class EventControllerBase
{
    public abstract void Raise(object data = null, object data2 = null, object data3 = null, object data4 = null);
    public abstract void Add(Delegate callback);
}

public class EventController : EventControllerBase
{
    protected List<Delegate> subscribers = new List<Delegate>();
    protected event Action OnRaise;

    public override void Raise(object data = null, object data2 = null, object data3 = null, object data4 = null)
    {
        OnRaise?.Invoke();
    }

    public override void Add(Delegate callback)
    {
            subscribers.Add((Action)callback);
            OnRaise += (Action)callback;
    }

    public virtual void RemoveSubscriber(Action d)
    {
        subscribers.Remove(d);
        OnRaise -= d;
    }

    public void UnhookSubscribers()
    {
        foreach (var subscriber in subscribers)
        {
            OnRaise -= (Action)subscriber;
        }
    }
}

public class EventController<T> : EventControllerBase
{
    private List<Action<T>> subscribers = new List<Action<T>>();
    private event Action<T> OnRaise;

    public override void Raise(object data = null, object data2 = null, object data3 = null, object data4 = null)
    {
        if (data is T == false)
            Debug.LogError(string.Format("Invalid cast in events. Expected type: {0}. Received Type: {1}", typeof(T).Name, data.GetType().Name));
        OnRaise?.Invoke((T)data);
    }

    public override void Add(Delegate callback)
    {
        if (callback is Action<T>)
        {
            subscribers.Add((Action<T>)callback);
            OnRaise += (Action<T>)callback;
        }
        else
        {
            Debug.LogError("Cannot register with this callback");
        }
    }

    public virtual void RemoveSubscriber(Action<T> d)
    {
        subscribers.Remove(d);
        OnRaise -= d;
    }

    public void UnhookSubscribers()
    {
        foreach (var subscriber in subscribers)
        {
            OnRaise -= subscriber;
        }
    }
}

public class EventController<T, V> : EventControllerBase
{
    private List<Action<T, V>> subscribers = new List<Action<T, V>>();
    private event Action<T, V> OnRaise;

    public override void Raise(object data = null, object data2 = null, object data3 = null, object data4 = null)
    {
     //   if (data is T == false || data2 is V == false)
     //       Debug.LogError(string.Format("Invalid cast in events. Expected typse: {0} and {1}. Received Types: {2} and {3}", typeof(T).Name, typeof(V).Name, data.GetType().Name, data2.GetType().Name));
        OnRaise?.Invoke((T)data, (V)data2);
    }

    public override void Add(Delegate callback)
    {
        if (callback is Action<T, V>)
        {
            subscribers.Add((Action<T, V>)callback);
            OnRaise += (Action<T, V>)callback;
        }
        else
        {
            Debug.LogError("Cannot register with this callback");
        }
    }

    public virtual void RemoveSubscriber(Action<T, V> d)
    {
        subscribers.Remove(d);
        OnRaise -= d;
    }

    public void UnhookSubscribers()
    {
        foreach (var subscriber in subscribers)
        {
            OnRaise -= subscriber;
        }
    }
}

public class EventController<T, V, U> : EventControllerBase
{
    private List<Action<T, V, U>> subscribers = new List<Action<T, V, U>>();
    private event Action<T, V, U> OnRaise;

    public override void Raise(object data = null, object data2 = null, object data3 = null, object data4 = null)
    {
      //  if (data is T == false || data2 is V == false)
      //      Debug.LogError(string.Format("Invalid cast in events. Expected typse: {0} and {1}. Received Types: {2} and {3}", typeof(T).Name, typeof(V).Name, data.GetType().Name, data2.GetType().Name));
        OnRaise?.Invoke((T)data, (V)data2, (U)data3);
    }

    public override void Add(Delegate callback)
    {
        if (callback is Action<T, V, U>)
        {
            subscribers.Add((Action<T, V, U>)callback);
            OnRaise += (Action<T, V, U>)callback;
        }
        else
        {
            Debug.LogError("Cannot register with this callback");
        }
    }

    public virtual void RemoveSubscriber(Action<T, V, U> d)
    {
        subscribers.Remove(d);
        OnRaise -= d;
    }

    public void UnhookSubscribers()
    {
        foreach (var subscriber in subscribers)
        {
            OnRaise -= subscriber;
        }
    }
}

public class EventController<T, V, U, X> : EventControllerBase
{
    private List<Action<T, V, U, X>> subscribers = new List<Action<T, V, U, X>>();
    private event Action<T, V, U, X> OnRaise;

    public override void Raise(object data = null, object data2 = null, object data3 = null, object data4 = null)
    {
     //  if (data is T == false || data2 is V == false)
     //      Debug.LogError(string.Format("Invalid cast in events. Expected typse: {0} and {1}. Received Types: {2} and {3}", typeof(T).Name, typeof(V).Name, data.GetType().Name, data2.GetType().Name));
        OnRaise?.Invoke((T)data, (V)data2, (U)data3, (X)data4);
    }

    public override void Add(Delegate callback)
    {
        if (callback is Action<T, V, U, X>)
        {
            subscribers.Add((Action<T, V, U, X>)callback);
            OnRaise += (Action<T, V, U, X>)callback;
        }
        else
        {
            Debug.LogError("Cannot register with this callback");
        }
    }

    public virtual void RemoveSubscriber(Action<T, V, U, X> d)
    {
        subscribers.Remove(d);
        OnRaise -= d;
    }

    public void UnhookSubscribers()
    {
        foreach (var subscriber in subscribers)
        {
            OnRaise -= subscriber;
        }
    }
}

public enum GameEventsEnum
{
    // game stats
    DataGoldChanged = 1,
    DataAnimalWeightChanged = 2,
    DataGoldCostModifier = 3,
    DataWeightModifier = 4,
    DataGoldProduction = 5,
    DataAnimalSoldChanged = 6,
    DataTotalFoodBought = 7,
    DataTotalWeightAcquired = 8,
    DataWeightProduction = 9,

    // animal stats
    DataAnimalDamageChanged = 10,
    DataAnimalArmorChanged = 11,
    DataAnimalSpeedChanged = 12,
    DataAnimalCritChanceChanged = 13,
    DataAnimalCritDamageChanged = 14,
    
    // game events
    EventUpgrade = 15,
    EventAnimalDeath = 16,    
   // EventLootDropped = 17,
    EventLootSpawned = 18,
    EventLootPickedUp = 19,
    EventGoldGained = 20,
    EventLootGained = 31,
    EventShopItemPurchased = 21,
    EventAnimalSold = 22,

    // level management
    EventGamePauseToggle = 23,
    EventLoadLevel = 24,
    EventStartLevel = 25,
    EventAdvanceLevel = 26,
    EventGameOver = 27,
    EventGameRestart = 28,

    // misc
    EventCreateDamageText = 29,
    EventLaunchItem = 30
}