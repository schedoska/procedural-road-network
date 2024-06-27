using Godot;
using System;

public class World : Node2D
{
	CellGrid Komorki;
	public override void _Ready()
	{
		Komorki = new CellGrid(20,25);
	}
	public void PlaceCell(Cell Komorka, GridPos Pozycja)
	{
		if(Komorki.GetCell(Pozycja) != null)
		{
			GD.Print("Cell is occupied");
			return;
		}
		AddChild(Komorka);
		Komorka.Position = new Vector2(Pozycja.x * 100, Pozycja.y * 100);
		Komorki.AddCell(Komorka, Pozycja); //Dodanie do systemu kratek
	}
	public void RemoveCell(GridPos Pozycja)
	{
		RemoveChild(Komorki.GetCell(Pozycja));
		Komorki.RemoveCell(Pozycja);
	}
	public void PlaceCar()
	{
		PackedScene scenaZWozem = GD.Load<PackedScene>("Car.tscn");
		Car Samochod = scenaZWozem.Instance<Car>();
		AddChild(Samochod);
		Samochod.SetGridCell(Komorki);
	}
}


