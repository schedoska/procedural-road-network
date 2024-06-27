using Godot;
using System;

public class Node : Node2D
{
    public Node Next_Node_Straight, Next_Node_Right, Next_Node_Left; 
    public Vector2 Direction;
    bool isVisible = true;
    public Node(int x, int y, Vector2 kierunek)
    {
        Position = new Vector2(x,y);
        Direction = kierunek;
        ZIndex = 100;
    }
    public override void _Draw()
    {
        if(!isVisible) return;

        DrawCircle(new Vector2(0,0),5, Colors.Aqua);

        if(Next_Node_Straight != null)
        {
            DrawLine(new Vector2(0,0), Next_Node_Straight.GlobalPosition - GlobalPosition, Colors.Blue, 3);
        }
        if(Next_Node_Right != null)
        {
             DrawLine(new Vector2(0,0), Next_Node_Right.GlobalPosition - GlobalPosition, Colors.Blue, 3);
        }
        if(Next_Node_Left != null)
        {
             DrawLine(new Vector2(0,0), Next_Node_Left.GlobalPosition - GlobalPosition, Colors.Blue, 3);
        }
        DrawLine(new Vector2(0,0),Direction*15,Colors.DarkSeaGreen,3);
    }
    public void ClearNextNodes()
    {
        Next_Node_Left = null;
        Next_Node_Right = null;
        Next_Node_Straight = null;
    }
}