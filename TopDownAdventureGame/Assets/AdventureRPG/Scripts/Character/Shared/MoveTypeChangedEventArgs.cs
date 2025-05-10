using System;

public class MoveTypeChangedEventArgs : EventArgs
{
    public MoveType PreviousType { get; }
    public MoveType NewType { get; }

    public MoveTypeChangedEventArgs(MoveType previous, MoveType current)
    {
        PreviousType = previous;
        NewType = current;
    }
}