using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameOfLife : MonoBehaviour {
    Tilemap tilemap;
    public TileBase dead;
    public TileBase alive;
    public float Speed_in_Seconds;
    public Vector2Int[] blocks;
    Vector3Int[] positions = new Vector3Int[60*28];
    TileBase[] tileArray = new TileBase[60 * 28];
    int index = 0;
    Stack<int> Switch_tile_alive = new Stack<int>();
    Stack<int> Switch_tile_dead = new Stack<int>();
    bool[] check = new bool[60 * 28];
    bool toggle = true;
    

    void Start () {
        //Calls Function for initializing all Tiles
        initializeTiles(); 
	}
	
	void Update () {

        
        if (toggle == true)
        {
            //Calls Countdown Coroutine to switch Toggle every "Speed_in_Seconds" time
            StartCoroutine(Countdown());

            //Searches "Alive" Stack or "Dead" Stack for index of the tiles to be switched between "Alive" or "Dead"
            while (Switch_tile_alive.Count != 0)
            {
                tileArray[Switch_tile_alive.Pop()] = alive;
            }

            while(Switch_tile_dead.Count != 0)
            {
                tileArray[Switch_tile_dead.Pop()] = dead;
            }

            //Implements the entire tileArray into the tilemap
            tilemap = GetComponentInChildren<Tilemap>();
            tilemap.SetTiles(positions, tileArray);

            //Resets Check counter
            for (int i = 0; i < index; i++)
                check[i] = false;

            toggle = false;

            //Calls Switch() Function for implementing the Game Of Life Algorithm
            Switch();
        }
        
	}


    private void initializeTiles()
    {
        //Initializes all tiles of the Grid into "Dead" tiles
        for(int y=-14; y<14; y++)
        {
            for(int x= -30; x<30; x++)
            {
                positions[index] = new Vector3Int(x, y, 0);
                tileArray[index] = dead;
                index++;
            }
        }

        //Takes User Input to convert the coordinates of the Input into tileArray indexes and implements "Alive" Tiles in those specific locations
        for(int i= 0; i< blocks.Length; i++)
        {
            tileArray[(blocks[i].x + 30) + ((blocks[i].y + 14) * 60)] = alive;
        }

        //Implements the entire tileArray into the tilemap and Resets Check counter
        tilemap = GetComponentInChildren<Tilemap>();
        tilemap.SetTiles(positions, tileArray);

        for (int i = 0; i < index; i++)
            check[i] = false;
    }

    private void Switch()
    {
       for(int i= 0; i<index; i++)
        {
            //Searches the neighbour of all "Alive" Cells and implements "Check_Neighbour" Function
            //Searches the neighbour of all "Dead" Cells for "Dead" Cells and implements "Check_Neighbour" Function on each 
            if (tileArray[i] == alive)
            {
                check_neighbours(i);

                if (tileArray[i - 61] == dead && check[i-61] == false)
                    check_neighbours(i-61);
                if (tileArray[i - 60] == dead && check[i - 60] == false)
                    check_neighbours(i - 60);
                if (tileArray[i - 59] == dead && check[i - 59] == false)
                    check_neighbours(i - 59);
                if (tileArray[i - 1] == dead && check[i - 1] == false)
                    check_neighbours(i - 1);
                if (tileArray[i + 1] == dead && check[i + 1] == false)
                    check_neighbours(i + 1);
                if (tileArray[i + 59] == dead && check[i + 59] == false)
                    check_neighbours(i+ 59);
                if (tileArray[i + 60] == dead && check[i + 60] == false)
                    check_neighbours(i + 60);
                if (tileArray[i + 61] == dead && check[i + 61] == false)
                    check_neighbours(i + 61);

            }
        }

    }

    private void check_neighbours(int i)
    {
        //Checks the Neighbours of the given location for "Alive" Cells.
        try
        {
            int count = 0;
            if (tileArray[i - 61] == alive)
                count++;
            if (tileArray[i - 60] == alive)
                count++;
            if (tileArray[i - 59] == alive)
                count++;
            if (tileArray[i - 1] == alive)
                count++;
            if (tileArray[i + 1] == alive)
                count++;
            if (tileArray[i + 59] == alive)
                count++;
            if (tileArray[i + 60] == alive)
                count++;
            if (tileArray[i + 61] == alive)
                count++;

            check[i] = true;

            //If the Tile is "Alive" with 2-3 "Alive" Neighbours, the tile is passed into "Alive" Stack else it is passed in "Dead" Stack
            //Similarly checks the "Dead" tile for exactly 3 "Alive" Neighbours
            if (tileArray[i] == alive)
            {
                if (count >= 2 && count < 4)
                    Switch_tile_alive.Push(i);
                else
                    Switch_tile_dead.Push(i);
            }
            else if (count == 3)
                Switch_tile_alive.Push(i);
        }
        catch (System.Exception)
        {
            Debug.Log("End of Grid");
        }
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(Speed_in_Seconds);
        toggle = true;
    }


}
