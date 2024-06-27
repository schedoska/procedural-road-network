using Godot;
using System;

public class Economics : Node2D
{
	World Swiat;
	WorldEditor Edytor;
	public override void _Ready()
	{
		Swiat = GetNode<World>("World");
		Edytor = new WorldEditor();
		Edytor.EdytowanySwiat = Swiat;
		AddChild(Edytor);
	}
}
