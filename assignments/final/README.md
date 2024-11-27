# Final Project: Taxi Driver (not definitive name)
**Shamil Kalmatov, Antony Navarro**
**CSC-470**  
**Fall 2024**  

---

## Gameplay Description  
The player is a taxi driver navigating an open-world city. The primary objective is to complete missions involving picking up passengers and delivering them to their destinations. Successful missions reward the player with money, which can be used to repair their vehicle or purchase new cars.

The ultimate goal is to save enough money to buy a Lamborghini, the player’s dream car. To achieve this, players will complete various missions, upgrade their vehicles for better rewards, and avoid collisions with other vehicles, which can damage their car and even destroy it.

You win if when you buy the lambo

You lose if all you vehicules are destroyed

---

## Input  
- **Keyboard**: Use `W`, `A`, `S`, `D` or arrow keys to control car movement.  
- **Mouse**: Click to select cars for purchase, interact with the user interface, and accept missions.  

---

## Visual Style  
- **Perspective**: The camera is positioned overhead, giving a top-down view of the city and the player’s car.  
- **Assets**: All the assets will come from **Poly.pizza** so that the game has a low poly style.  

---

## Audio Style  
- **Background Music**: Cool background music.  
- **Sound Effects**:  
  - Engine sounds while driving.  
  - Collision sounds when the car crashes.  

---

## Interface Sketches  
1. **Main Menu**:  
   - Game title at the top.  
   - A single “Start Game” button.  

2. **Car Selection Pop-Up**:  
   - Displays car stats (speed, health points, visual).  
   - Includes a button to confirm the purchase or decline. 

3. **Mission Acceptance Screen**:  
   - Mission description and reward details.  
   - Accept and Decline buttons.  

4. **In-Game HUD**:  
   - **Health Meter**: Shows the car's health.  
   - **Money Counter**: Displays the player’s current balance.  
   - **Main Menu Button** To go back to the main menu

5. **Win/Lose Screens**:  
   - A win screen when the Lamborghini is purchased.  
   - A game over screen when no functional cars remain.  

---

## Story/Theme Description  
You are a hardworking taxi driver with a big dream: buying your dream car, a Lamborghini. To achieve this, you must take on various missions from NPCs scattered around the city. However, the roads are filled with reckless drivers, and you must avoid collisions to prevent damaging your car.  

---

## Feature Set Targets  

### 1. Low-Bar (Minimum Viable Product):  
- Players can lose when they have no cars left to drive.  
- Players can win by purchasing the Lamborghini, triggering a win screen.  

### 2. Target (Expected Outcome):  
- Fully functioning game mechanics, including:  
  - Traffic system.  
  - Mission acceptance and completion.  
  - Car purchase with UI stats.  
  - Sound effects and music.  
  - Particule effects for collisions

### 3. High-Bar (Stretch Goals):  
- Add additional challenges to increase difficulty (e.g., time-limited missions, restricted routes or cars).  
- Introduce bonus items that provide special rewards or abilities.  

---

## Timeline  

| **Date**    | **Task**                                    |  
|-------------|---------------------------------------------|  
| **11/27**   | Create the control script for the car.      |  
| **12/01**   | Implement traffic and expand the city.      |  
| **12/06**   | Add the car health bar and Game Over screen.|  
| **12/08**   | Add the repair/change car option.           |  
| **12/10**   | Add main menu, sound effects, and music.    |  
| **12/13**   | Fix bugs, finalize the report, and submit.  |  
