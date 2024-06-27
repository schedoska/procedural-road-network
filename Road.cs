using Godot;
using System;

public class Road : Cell
{
	public RoadType TypDrogi = RoadType.Nothing;
	RoadLookManager ManagerWygadu;
	NodesManager NodesManager;
	public override void _Ready()
	{
		ManagerWygadu = new RoadLookManager(GetNode<Sprite>("Droga"));
		ManagerWygadu.UpdateLook(RoadType.Nothing);
		ConnectableSides.ActivateAll();

		NodesManager = new NodesManager(InterfaceNodes);
	}
	public override void UpdateCell()
	{
		TypDrogi = RoadTypeGenerator.GetRoadType(SasiedztwoKomorki);
		ManagerWygadu.UpdateLook(TypDrogi);
		NodesManager.UpdateNodesConnections(TypDrogi, SasiedztwoKomorki);
	}
}
public enum RoadType
{
	Nothing, TopDown, RightLeft, TopLeftDownRight,
	Top, Right, Down, Left,
	TopRight, RightDown, DownLeft, LeftTop,
	TopRightDown, RightDownLeft, DownLeftTop, LeftTopRight
}
//KLasa sterujaca wygladem drogi
public class RoadLookManager
{
	Sprite Tekstura;
	public RoadLookManager(Sprite Teksture)
	{
		Tekstura = Teksture;
	}
	public void UpdateLook(RoadType typDrogi)
	{
		switch(typDrogi)
		{
			case RoadType.Nothing:
				Tekstura.Frame = 0;
				break;
			case RoadType.TopDown:
				Tekstura.Frame = 1;
				break;
			case RoadType.RightLeft:
				Tekstura.Frame = 2;
				break;
			case RoadType.TopLeftDownRight:
				Tekstura.Frame = 3;
				break;
			//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			case RoadType.Top:
				Tekstura.Frame = 4;
				break;
			case RoadType.Right:
				Tekstura.Frame = 5;
				break;
			case RoadType.Down:
				Tekstura.Frame = 6;
				break;
			case RoadType.Left:
				Tekstura.Frame = 7;
				break;
			//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			case RoadType.TopRight:
				Tekstura.Frame = 8;
				break;
			case RoadType.RightDown:
				Tekstura.Frame = 9;
				break;
			case RoadType.DownLeft:
				Tekstura.Frame = 10;
				break;
			case RoadType.LeftTop:
				Tekstura.Frame = 11;
				break;
			//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			case RoadType.TopRightDown:
				Tekstura.Frame = 12;
				break;
			case RoadType.RightDownLeft:
				Tekstura.Frame = 13;
				break;
			case RoadType.DownLeftTop:
				Tekstura.Frame = 14;
				break;
			case RoadType.LeftTopRight:
				Tekstura.Frame = 15;
				break;
		}
	}
}
//Klasa Rozpoznajaca Typ Drogi
public static class RoadTypeGenerator
{
	public static RoadType GetRoadType(NeighbourCells Sasiedztwo)
	{
		bool Top = false;
		bool Right = false;
		bool Down = false;
		bool Left = false;	 
		
		int ConnectedSidesCount = 0;

		if(Sasiedztwo.Top != null && Sasiedztwo.Top.ConnectableSides.Down)
		{
			Top = true;
			ConnectedSidesCount++;
		}
		if(Sasiedztwo.Right != null && Sasiedztwo.Right.ConnectableSides.Left)
		{
			Right = true;
			ConnectedSidesCount++;
		}
		if(Sasiedztwo.Down != null && Sasiedztwo.Down.ConnectableSides.Top)
		{
			Down = true;
			ConnectedSidesCount++;
		}
		if(Sasiedztwo.Left != null && Sasiedztwo.Left.ConnectableSides.Right)
		{
			Left = true;
			ConnectedSidesCount++;
		}

		//0 polaczen
		if(ConnectedSidesCount == 0) return RoadType.Nothing;
		//1 polaczenie
		if(ConnectedSidesCount == 1)
		{
			if(Top) return RoadType.Top;
			if(Right) return RoadType.Right;
			if(Down) return RoadType.Down;
			if(Left) return RoadType.Left;
		}
		//2 poloczenia
		if(ConnectedSidesCount == 2)
		{
			if(Top && Down) return RoadType.TopDown;
			if(Left && Right) return RoadType.RightLeft;
			if(Top && Right) return RoadType.TopRight;
			if(Right && Down) return RoadType.RightDown;
			if(Down && Left) return RoadType.DownLeft;
			if(Left && Top) return RoadType.LeftTop;
		}
		//3 poloczenia
		if(ConnectedSidesCount == 3)
		{
			if(Top && Right && Down) return RoadType.TopRightDown;
			if(Right && Down && Left) return RoadType.RightDownLeft;
			if(Down && Left && Top) return RoadType.DownLeftTop;
			if(Left && Top && Right) return RoadType.LeftTopRight;
		}
		//4 poloczenia
		if(ConnectedSidesCount == 4) return RoadType.TopLeftDownRight;
		return RoadType.Nothing;
	}
}
//Klasa Zarzadza NOdesami
public class NodesManager
{
	InterfaceNodes InterfaceNodes;
	public NodesManager(InterfaceNodes Interfacenode)
	{
		InterfaceNodes = Interfacenode;
	}
	public void UpdateNodesConnections(RoadType typdrogi, NeighbourCells Sasiedztwo)
	{
		InterfaceNodes.ResetNodesConnections();

		switch(typdrogi)
		{
			//2 side connection
			case RoadType.RightLeft:
				RightLeftConnection(Sasiedztwo);
				break;
			case RoadType.TopDown:
				TopDownConnection(Sasiedztwo);
				break;
			case RoadType.TopRight:
				TopRightConnection(Sasiedztwo);
				break;
			case RoadType.RightDown:
				RightDownConnection(Sasiedztwo);
				break;
			case RoadType.DownLeft:
				DownLeftConnection(Sasiedztwo);
				break;
			case RoadType.LeftTop:
				LeftTopConnection(Sasiedztwo);
				break;
			//3 side connection
			case RoadType.TopRightDown:
				TopRightDownConnection(Sasiedztwo);
				break;
			case RoadType.RightDownLeft:
				RightDownLeftConnection(Sasiedztwo);
				break;
			case RoadType.DownLeftTop:
				DownLeftTopConnection(Sasiedztwo);
				break;
			case RoadType.LeftTopRight:
				LeftTopRightConnection(Sasiedztwo);
				break;
			//4 side connection
			case RoadType.TopLeftDownRight:
				TopRightDownLeftConnection(Sasiedztwo);
				break;
			//1 sie connected
			case RoadType.Top:
				TopConnection(Sasiedztwo);
				break;
			case RoadType.Right:
				RightConnection(Sasiedztwo);
				break;
			case RoadType.Down:
				DownConnection(Sasiedztwo);
				break;
			case RoadType.Left:
				LeftConnection(Sasiedztwo);
				break;
		}

		InterfaceNodes.UpdateNodesLook();
	}
	void TopDownConnection(NeighbourCells Sasiedztwo)
	{
		InterfaceNodes.DownInput.Next_Node_Straight = Sasiedztwo.Top.InterfaceNodes.DownInput;
		InterfaceNodes.TopInput.Next_Node_Straight = Sasiedztwo.Down.InterfaceNodes.TopInput;
	}
	void RightLeftConnection(NeighbourCells Sasiedztwo)
	{
		InterfaceNodes.RightInput.Next_Node_Straight = Sasiedztwo.Left.InterfaceNodes.RightInput;
		InterfaceNodes.LeftInput.Next_Node_Straight = Sasiedztwo.Right.InterfaceNodes.LeftInput;
	}
	void TopRightConnection(NeighbourCells Sasiedztwo)
	{
		InterfaceNodes.TopInput.Next_Node_Straight = Sasiedztwo.Right.InterfaceNodes.LeftInput;
		InterfaceNodes.RightInput.Next_Node_Straight = Sasiedztwo.Top.InterfaceNodes.DownInput;
	}
	void RightDownConnection(NeighbourCells Sasiedztwo)
	{
		InterfaceNodes.RightInput.Next_Node_Straight = Sasiedztwo.Down.InterfaceNodes.TopInput;
		InterfaceNodes.DownInput.Next_Node_Straight = Sasiedztwo.Right.InterfaceNodes.LeftInput;
	}
	void DownLeftConnection(NeighbourCells Sasiedztwo)
	{
		InterfaceNodes.DownInput.Next_Node_Straight = Sasiedztwo.Left.InterfaceNodes.RightInput;
		InterfaceNodes.LeftInput.Next_Node_Straight = Sasiedztwo.Down.InterfaceNodes.TopInput;
	}
	void LeftTopConnection(NeighbourCells Sasiedztwo)
	{
		InterfaceNodes.LeftInput.Next_Node_Straight = Sasiedztwo.Top.InterfaceNodes.DownInput;
		InterfaceNodes.TopInput.Next_Node_Straight = Sasiedztwo.Left.InterfaceNodes.RightInput;
	}
	void TopRightDownConnection(NeighbourCells Sasiedztwo)
	{
		InterfaceNodes.TopInput.Next_Node_Straight = Sasiedztwo.Down.InterfaceNodes.TopInput;
		InterfaceNodes.TopInput.Next_Node_Left = Sasiedztwo.Right.InterfaceNodes.LeftInput;

		InterfaceNodes.RightInput.Next_Node_Right = Sasiedztwo.Top.InterfaceNodes.DownInput;
		InterfaceNodes.RightInput.Next_Node_Left = Sasiedztwo.Down.InterfaceNodes.TopInput;

		InterfaceNodes.DownInput.Next_Node_Straight = Sasiedztwo.Top.InterfaceNodes.DownInput;
		InterfaceNodes.DownInput.Next_Node_Right = Sasiedztwo.Right.InterfaceNodes.LeftInput;
	}
	void RightDownLeftConnection(NeighbourCells Sasiedztwo)
	{
		InterfaceNodes.RightInput.Next_Node_Straight = Sasiedztwo.Left.InterfaceNodes.RightInput;
		InterfaceNodes.RightInput.Next_Node_Left = Sasiedztwo.Down.InterfaceNodes.TopInput;

		InterfaceNodes.DownInput.Next_Node_Right = Sasiedztwo.Right.InterfaceNodes.LeftInput;
		InterfaceNodes.DownInput.Next_Node_Left = Sasiedztwo.Left.InterfaceNodes.RightInput;

		InterfaceNodes.LeftInput.Next_Node_Straight = Sasiedztwo.Right.InterfaceNodes.LeftInput;
		InterfaceNodes.LeftInput.Next_Node_Right = Sasiedztwo.Down.InterfaceNodes.TopInput;
	}
	void DownLeftTopConnection(NeighbourCells Sasiedztwo)
	{
		InterfaceNodes.DownInput.Next_Node_Straight = Sasiedztwo.Top.InterfaceNodes.DownInput;
		InterfaceNodes.DownInput.Next_Node_Left = Sasiedztwo.Left.InterfaceNodes.RightInput;

		InterfaceNodes.LeftInput.Next_Node_Right = Sasiedztwo.Down.InterfaceNodes.TopInput;
		InterfaceNodes.LeftInput.Next_Node_Left = Sasiedztwo.Top.InterfaceNodes.DownInput;

		InterfaceNodes.TopInput.Next_Node_Straight = Sasiedztwo.Down.InterfaceNodes.TopInput;
		InterfaceNodes.TopInput.Next_Node_Right = Sasiedztwo.Left.InterfaceNodes.RightInput;
	}
	void LeftTopRightConnection(NeighbourCells Sasiedztwo)
	{
		InterfaceNodes.LeftInput.Next_Node_Straight = Sasiedztwo.Right.InterfaceNodes.LeftInput;
		InterfaceNodes.LeftInput.Next_Node_Left = Sasiedztwo.Top.InterfaceNodes.DownInput;

		InterfaceNodes.TopInput.Next_Node_Right = Sasiedztwo.Left.InterfaceNodes.RightInput;
		InterfaceNodes.TopInput.Next_Node_Left = Sasiedztwo.Right.InterfaceNodes.LeftInput;

		InterfaceNodes.RightInput.Next_Node_Straight = Sasiedztwo.Left.InterfaceNodes.RightInput;
		InterfaceNodes.RightInput.Next_Node_Right = Sasiedztwo.Top.InterfaceNodes.DownInput;
	}
	void TopRightDownLeftConnection(NeighbourCells Sasiedztwo)
	{
		InterfaceNodes.TopInput.Next_Node_Straight = Sasiedztwo.Down.InterfaceNodes.TopInput;
		InterfaceNodes.TopInput.Next_Node_Right = Sasiedztwo.Left.InterfaceNodes.RightInput;
		InterfaceNodes.TopInput.Next_Node_Left = Sasiedztwo.Right.InterfaceNodes.LeftInput;

		InterfaceNodes.RightInput.Next_Node_Straight = Sasiedztwo.Left.InterfaceNodes.RightInput;
		InterfaceNodes.RightInput.Next_Node_Right = Sasiedztwo.Top.InterfaceNodes.DownInput;
		InterfaceNodes.RightInput.Next_Node_Left = Sasiedztwo.Down.InterfaceNodes.TopInput;

		InterfaceNodes.DownInput.Next_Node_Straight = Sasiedztwo.Top.InterfaceNodes.DownInput;
		InterfaceNodes.DownInput.Next_Node_Right = Sasiedztwo.Right.InterfaceNodes.LeftInput;
		InterfaceNodes.DownInput.Next_Node_Left = Sasiedztwo.Left.InterfaceNodes.RightInput;

		InterfaceNodes.LeftInput.Next_Node_Straight = Sasiedztwo.Right.InterfaceNodes.LeftInput;
		InterfaceNodes.LeftInput.Next_Node_Right = Sasiedztwo.Down.InterfaceNodes.TopInput;
		InterfaceNodes.LeftInput.Next_Node_Left = Sasiedztwo.Top.InterfaceNodes.DownInput;
	}
	void TopConnection(NeighbourCells Sasiedztwo)
	{
		InterfaceNodes.TopInput.Next_Node_Straight = Sasiedztwo.Top.InterfaceNodes.DownInput;
	}
	void RightConnection(NeighbourCells Sasiedztwo)
	{
		InterfaceNodes.RightInput.Next_Node_Straight = Sasiedztwo.Right.InterfaceNodes.LeftInput;
	}
	void DownConnection(NeighbourCells Sasiedztwo)
	{
		InterfaceNodes.DownInput.Next_Node_Straight = Sasiedztwo.Down.InterfaceNodes.TopInput;
	}
	void LeftConnection(NeighbourCells Sasiedztwo)
	{
		InterfaceNodes.LeftInput.Next_Node_Straight = Sasiedztwo.Left.InterfaceNodes.RightInput;
	}
}
