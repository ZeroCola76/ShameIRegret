public interface ISubject
{
    void RegisterObserver(IObserver observer);
    void RemoveObserver(IObserver observer);
    void NotifyObservers();
}

//°üÂûÀÚ
public interface IObserver
{
    void OnNotify(EventType eventType);
}

public enum EventType
{
    rhythmHurt,
    rhythmDie,
}