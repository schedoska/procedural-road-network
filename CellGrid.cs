using Godot;
using System;

public class CellGrid
{
    Cell[,] Siatka;
    WorldInfo WorldInfo;

    public CellGrid(int Wsyokosc, int Szerokosc)
    {
        Siatka = new Cell[Wsyokosc, Szerokosc];
        WorldInfo = new WorldInfo();
        WorldInfo.Szerokosc = Szerokosc;
        WorldInfo.Wsyokosc = Wsyokosc;
    }
    public bool AddCell(Cell drivable, GridPos pozycja)
    {
        if(pozycja.x < 0 || pozycja.x > WorldInfo.Szerokosc) return false;
        if(pozycja.y < 0 || pozycja.y > WorldInfo.Wsyokosc) return false;

        Siatka[pozycja.y, pozycja.x] = drivable;

        UpdateCellNeighbourhood(pozycja);
        UpdateCellNeighbourhood(pozycja.Top());
        UpdateCellNeighbourhood(pozycja.Right());
        UpdateCellNeighbourhood(pozycja.Down());
        UpdateCellNeighbourhood(pozycja.Left());
        return true;
    }
    public Cell GetCell(GridPos pozycja)
    {
        if(pozycja.x < 0 || pozycja.x > WorldInfo.Szerokosc) return null;
        if(pozycja.y < 0 || pozycja.y > WorldInfo.Wsyokosc) return null;

        return Siatka[pozycja.y, pozycja.x];
    }
    public Cell RemoveCell(GridPos pozycja)
    {
        if(pozycja.x < 0 || pozycja.x > WorldInfo.Szerokosc) return null;
        if(pozycja.y < 0 || pozycja.y > WorldInfo.Wsyokosc) return null;

        Cell Do_Usuniecia = Siatka[pozycja.y, pozycja.x];
        Siatka[pozycja.y, pozycja.x] = null;

        UpdateCellNeighbourhood(pozycja.Top());
        UpdateCellNeighbourhood(pozycja.Right());
        UpdateCellNeighbourhood(pozycja.Down());
        UpdateCellNeighbourhood(pozycja.Left());

        return Do_Usuniecia;
    }
    public void UpdateCellNeighbourhood(GridPos Pozycja)
    {
        if(GetCell(Pozycja) == null) return;

        Cell Updatowany = GetCell(Pozycja);
        Updatowany.UpdateNeighbours(GetCell(Pozycja.Top()),GetCell(Pozycja.Right()),GetCell(Pozycja.Down()),GetCell(Pozycja.Left()));
        Updatowany.UpdateCell();
    }

}