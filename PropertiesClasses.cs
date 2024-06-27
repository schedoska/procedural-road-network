using System;
using Godot;
using System.Collections.Generic;

public struct WorldInfo
{
	public int Szerokosc, Wsyokosc;
	public int Rozmiar_Kraty;
}
public class GridPos
{
	public int x, y;
	public GridPos(int x, int y)
	{
		this.x = x;
		this.y = y;
	}
	public GridPos Top()
	{
		return new GridPos(x, y - 1);
	}
	public GridPos Right()
	{
		return new GridPos(x + 1, y);
	}
	public GridPos Down()
	{
		return new GridPos(x, y + 1);
	}
	public GridPos Left()
	{
		return new GridPos(x - 1, y);
	}
}
public struct NeighbourCells
{
	public Cell Top, Right, Down, Left;
}
public class ConnectableSides
{
	public bool Top, Right, Down, Left;
	public ConnectableSides()
	{
		Top = true;
		Right = true;
		Left = true;
		Down = true;
	}
	public void ActivateAll()
	{
		Top = true;
		Right = true;
		Left = true;
		Down = true;
	}
	public void DeActivateAll()
	{
		Top = false;
		Right = false;
		Left = false;
		Down = false;
	}
}
public class InterfaceNodes : Node2D
{

	public Node TopInput, RightInput, DownInput, LeftInput;
	public InterfaceNodes()
	{
		int Offset = 30;

		TopInput = new Node(Offset, 0, new Vector2(0, 1));
		RightInput = new Node(100, Offset, new Vector2(-1, 0));
		DownInput = new Node(100 - Offset, 100, new Vector2(0, -1));
		LeftInput = new Node(0, 100 - Offset, new Vector2(1, 0));

		AddChild(TopInput);
		AddChild(RightInput);
		AddChild(DownInput);
		AddChild(LeftInput);
	}
	public void ResetNodesConnections()
	{
		TopInput.ClearNextNodes();
		RightInput.ClearNextNodes();
		DownInput.ClearNextNodes();
		LeftInput.ClearNextNodes();
	}
	public void UpdateNodesLook()
	{
		TopInput.Update();
		RightInput.Update();
		DownInput.Update();
		LeftInput.Update();
	}
}


















