# Rubik's Cube
![Rubik's Cube banner](https://github.com/lpvlnc/rubiks-cube/blob/main/Screenshots/screenshot_01.png)

# 🏫 Academic Context

 This project was initially developed for the **Artificial Inteligence** discipline at Faculdade Dom Bosco de Porto Alegre. Our task was to develop a program that could solve a Rubik's cube, using either our own or a already known algorithm. To make it more fun we choose to make it a game.
 After the version was presented in class, **additional improvements** were made to enhance gameplay experience.

---
## 🎮 About the Game

 The game was developed using **Unity (C#)**. The game consist in solve a Rubik's cube, this can be do manually using the mouse.
   - Left mouse button (drag): Rotates the face of the cube.
   - Right mouse drag: (drag): Rotates the entire cube.

---
## 🧠 Game Features

- **Cube face map**: A map that shows all the faces of the cube in real time.
- **Shuffle (Button)**: A button that automatically shuffles the cube using 10 to 30 random movements.
- **Solve (Button)**: Automatically solve the cube using **Kociemba's** algorithm.
- **Moves list**: Keep track of all the movements used by the **Kociemba's algorithm** to solve the cube.

---
## 🟩 Kociemba's Two-Phase Algorithm

 The algorithm used to solve the cube was made by [Herbert Kociemba](https://kociemba.org/). It was created in 1991–1992, solves the Rubik’s Cube in two stages using dynamic computation and subgroup G1. It finds solutions in at most 30 moves and improves efficiency with pruning tables, symmetry reduction, and by discarding redundant paths.
 
 You can check more details about it at [this page](https://kociemba.org/math/imptwophase.htm).
 
---
## 🔄 Post-Submission Updates

  - Solved animation bugs.
  - Improvements in the UI.
  - Added sound effects.

---
## 🙌 Credit's to Megalomobile

 This project was made based on a playlist tutorial by [Megalomobile](https://www.youtube.com/@megalomatt) teaching how to make a Rubik's Cube in Unity and how to implement a version of Kociemba's algorithm in C#.
 You can check the playlist at this [link](https://www.youtube.com/watch?v=JN9vx0veZ-c&list=PLuq_iMEtykn-ZOJyx2cY_k9WkixAhv11n&ab_channel=Megalomobile).

---
## 📦 How to Run

Download the package release (.zip), run the Rubik's Cube.exe and play!
or
Build locally:
 1. Clone this repository
 2. Use the 6000.1.8f1 version of unity
 3. Run the project

If you have any trouble running the project get in touch with me through my LinkedIn page (on profile)
