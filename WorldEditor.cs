using Godot;
using System;

public class WorldEditor : Node2D
{
    public World EdytowanySwiat;
    bool clicked = false;
    PackedScene Droga;
    GridPos Aktualna_PozycjaMyszy;
    Timer Czasiak;

    public override void _Ready()
    {
        Droga = GD.Load<PackedScene>("Road.tscn");
        Aktualna_PozycjaMyszy = new GridPos(2,2);
        BuildRoad();
        Aktualna_PozycjaMyszy = new GridPos(3,2);
        BuildRoad();
        Aktualna_PozycjaMyszy = new GridPos(4,2);
        BuildRoad();
        Czasiak = new Timer();
        AddChild(Czasiak);
        Czasiak.OneShot = true;
    }
    public override void _Process(float delta)
    {
        Aktualna_PozycjaMyszy.x = ((int)(GetGlobalMousePosition().x/100));
        Aktualna_PozycjaMyszy.y = ((int)(GetGlobalMousePosition().y/100));

        if(Input.IsMouseButtonPressed(((int)ButtonList.Left)) && !clicked)
        {
            clicked = true;
            BuildRoad();
        }
        else if(!Input.IsMouseButtonPressed(((int)ButtonList.Left)) && clicked)
        {
            clicked = false;
        }
        if(Input.IsMouseButtonPressed((int)ButtonList.Right))
        {
            RemoveRoad();
        }
        if(Input.IsKeyPressed(((int)KeyList.W)) && Czasiak.TimeLeft == 0)
        {
            EdytowanySwiat.PlaceCar();
            Czasiak.Start(1);
        }
    }
    void BuildRoad()
    {
        EdytowanySwiat.PlaceCell(Droga.Instance<Road>(), Aktualna_PozycjaMyszy);
    }
    void RemoveRoad()
    {
        EdytowanySwiat.RemoveCell(Aktualna_PozycjaMyszy);
    }
}