using Godot;
using System;

public class Cell : Node2D
{
    public GridPos Pozycja;
    public NeighbourCells SasiedztwoKomorki;
    public ConnectableSides ConnectableSides;
    public InterfaceNodes InterfaceNodes;
    public Cell()
    {
        ConnectableSides = new ConnectableSides();
        InterfaceNodes = new InterfaceNodes();
		AddChild(InterfaceNodes);
    }
    public void UpdateNeighbours(Cell Top, Cell Right, Cell Down, Cell Left)
    {
        SasiedztwoKomorki.Top = Top;
        SasiedztwoKomorki.Right = Right;
        SasiedztwoKomorki.Down = Down;
        SasiedztwoKomorki.Left = Left;
    }
    public virtual void UpdateCell(){}
}