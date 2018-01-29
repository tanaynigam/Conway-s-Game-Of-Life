Conway's Game of life
------------------------------

Introduction
----------------

The Game of Life, also known simply as Life, is a cellular automaton devised by the British mathematician John Horton Conway in 1970. The "game" is a zero-player game, meaning that its evolution is determined by its initial state, requiring no further input.

This project is a Unity implementation of Game of life in 2D and 3D. The color White is used for representing "Dead" cells and black for "Alive" cells.

The 2D and 3D version of the game works on the following rules.

1.Any live cell with fewer than two live neighbours dies.
2.Any live cell with two or three live neighbours lives.
3.Any live cell with more than three live neighbours dies.
4.Any dead cell with exactly three live neighbours becomes a live cell.

The game is activated by filling in the initial conditions on a grid of size [60x28] and running **Play**.

Runbook
----------------

Here is how you can run this project on your local machine:

* Clone the Repo from GitHub

* Open the Project on Windows on Unity version 2017.02+ (Preferably 2017.03.0f3)

* Enter the values for Speed_in_Seconds and blocks at the Inputs at the Script Component. The Script attached to the "Grid" GameObject.

* The Inputs to be entered follow these set of rules:
	* The Speed_in_seconds component of the script determines the update speed of the game in seconds.
	* The Blocks component of the Script determines the initial placement of the **Alive** Cells to start the game. The inputs require using the **Size** factor of the block to input number of initial **Alive** Cells and then input coordinates of each **Alive** Cell.
	* The coordinates of the grid corresponds to a regular X-Y grid with X=0 and Y=0 at the center of the grid/Camera.
 