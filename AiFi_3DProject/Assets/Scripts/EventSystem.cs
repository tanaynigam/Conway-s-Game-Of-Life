using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour {
    public float Speed_in_seconds;
    public Transform ParentObject;
    public Transform Cell;
    public Vector2Int[] blocks;
    bool[] CellArray = new bool[60 * 28];
    Vector3Int[] positions = new Vector3Int[60 * 28];
    int index = 0;
    Stack<int> Switch_tile_alive = new Stack<int>();
    Stack<int> Switch_tile_dead = new Stack<int>();
    bool[] check = new bool[60 * 28];
    bool toggle = false;
    int temp;
    bool start = false;

	// Use this for initialization
	void Start () {
        initializeGrid();
        StartCoroutine(Countdown());
    }
	
	// Update is called once per frame
	void Update () {

        if (toggle == true)
        {
            toggle = false;
            Switch();

            for (int i = ParentObject.childCount - 1; i >= 0; i--)
                Destroy(ParentObject.GetChild(i).gameObject);

            while (Switch_tile_alive.Count != 0)
            {
                temp = Switch_tile_alive.Pop();
                CellArray[temp] = true;
                Instantiate(Cell, new Vector3(positions[temp].x, positions[temp].y, 0), Quaternion.identity).transform.parent = ParentObject;
            }

            while (Switch_tile_dead.Count != 0)
            {
                CellArray[Switch_tile_dead.Pop()] = false;
            }

            for (int i = 0; i < index; i++)
                check[i] = false;
            
            StartCoroutine(Countdown());
        }

    }

    private void initializeGrid()
    {
        for (int y = -14; y < 14; y++)
        {
            for (int x = -30; x < 30; x++)
            {
                positions[index] = new Vector3Int(x, y, 0);
                CellArray[index] = false;
                index++;
            }
        }

        for (int i = 0; i < blocks.Length; i++)
        {
            CellArray[(blocks[i].x + 30) + ((blocks[i].y + 14) * 60)] = true;
            Instantiate(Cell, new Vector3(blocks[i].x, blocks[i].y, 0), Quaternion.identity).transform.parent = ParentObject;
        }

        for (int i = 0; i < index; i++)
            check[i] = false;
    }

    private void Switch()
    {
        for (int i = 0; i < index; i++)
        {
            if (CellArray[i] == true)
            {
                check_neighbours(i);

                if (CellArray[i - 61] == false && check[i - 61] == false)
                    check_neighbours(i - 61);
                if (CellArray[i - 60] == false && check[i - 60] == false)
                    check_neighbours(i - 60);
                if (CellArray[i - 59] == false && check[i - 59] == false)
                    check_neighbours(i - 59);
                if (CellArray[i - 1] == false && check[i - 1] == false)
                    check_neighbours(i - 1);
                if (CellArray[i + 1] == false && check[i + 1] == false)
                    check_neighbours(i + 1);
                if (CellArray[i + 59] == false && check[i + 59] == false)
                    check_neighbours(i + 59);
                if (CellArray[i + 60] == false && check[i + 60] == false)
                    check_neighbours(i + 60);
                if (CellArray[i + 61] == false && check[i + 61] == false)
                    check_neighbours(i + 61);

            }
        }

    }

    private void check_neighbours(int i)
    {
        try
        {
            int count = 0;
            if (CellArray[i - 61] == true)
                count++;
            if (CellArray[i - 60] == true)
                count++;
            if (CellArray[i - 59] == true)
                count++;
            if (CellArray[i - 1] == true)
                count++;
            if (CellArray[i + 1] == true)
                count++;
            if (CellArray[i + 59] == true)
                count++;
            if (CellArray[i + 60] == true)
                count++;
            if (CellArray[i + 61] == true)
                count++;

            check[i] = true;
            if (CellArray[i] == true)
            {
                if (count >= 2 && count < 4)
                    Switch_tile_alive.Push(i);
                else
                    Switch_tile_dead.Push(i);
            }
            else
                if (count == 3)
                Switch_tile_alive.Push(i);
        }
        catch (System.Exception)
        {
            Debug.Log("End of Grid");
        }
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(Speed_in_seconds);
        toggle = true;
    }
}
