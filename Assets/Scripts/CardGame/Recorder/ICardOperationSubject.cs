using UnityEngine.Events;

public interface ICardOperationSubject//报社
{
    UnityEvent<CardLogEntry> OnDrawCard{get;}
    UnityEvent<CardLogEntry> OnPlayCard{get;}
    UnityEvent<CardLogEntry> OnDiscardCard{get;}
}

