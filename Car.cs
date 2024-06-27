using Godot;
using System;

public class Car : Node2D
{
	public Node CurrentlyFolowedNode;
	CellGrid Komorki;
	public void SetGridCell(CellGrid cells)
	{
		Komorki = cells;
		CurrentlyFolowedNode = Komorki.GetCell(new GridPos(2,2)).InterfaceNodes.LeftInput;
		Position = Komorki.GetCell(new GridPos(2,2)).InterfaceNodes.LeftInput.GlobalPosition + new Vector2(-20,0);
		Rotation = Position.DirectionTo(Komorki.GetCell(new GridPos(2,2)).InterfaceNodes.LeftInput.GlobalPosition).Angle();
		ZIndex = 1000;
	}
	public override void _Process(float delta)
	{
		if(CurrentlyFolowedNode == null) return;

		if(Position.DistanceTo(CurrentlyFolowedNode.GlobalPosition) > 1)
		{
			Rotation = Position.DirectionTo(CurrentlyFolowedNode.GlobalPosition).Angle();
			Position += Position.DirectionTo(CurrentlyFolowedNode.GlobalPosition) * 1.3f;
		}
		else
		{
			bool Succes = false;
			while(!Succes)
			{
				Random kol = new Random();
				int Los = kol.Next(1,4);
				GD.Print(Los);
				
				if(Los == 1 && CurrentlyFolowedNode.Next_Node_Left != null)
				{
					CurrentlyFolowedNode = CurrentlyFolowedNode.Next_Node_Left;
					Succes = true;
				} 
				if(Los == 2 && CurrentlyFolowedNode.Next_Node_Straight != null)
				{
					CurrentlyFolowedNode = CurrentlyFolowedNode.Next_Node_Straight;
					Succes = true;
				} 
				if(Los == 3 && CurrentlyFolowedNode.Next_Node_Right != null)
				{
					CurrentlyFolowedNode = CurrentlyFolowedNode.Next_Node_Right;
					Succes = true;
				} 
				Succes = true;
			}
		}
	}
}
