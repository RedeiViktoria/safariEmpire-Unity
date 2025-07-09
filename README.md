# Safari Empire – Unity Simulation Game

This is a 2.5D top-down wildlife park management simulation game developed as a **team project** for the **Software Technology** course at **Eötvös Loránd University (ELTE)**.

The game puts the player in the role of a park director managing an African safari park. Players must balance economic growth, animal welfare, tourist satisfaction, and park safety to achieve long-term success.

> The project was developed using **Unity** and **C#**, with **GitLab** version control, following **agile practices** (Scrum methodology, weekly sprints).

---

## 🎮 Gameplay Summary

- Real-time simulation with adjustable speed (hour/day/week)
- Three difficulty levels (selected before starting)
- Dynamic animal behaviors (feeding, herding, aging, breeding)
- Terrain elements: roads, plants, ponds, rivers, hills
- Jeep rental system and tourists
- AI rangers and poachers
- Economy simulation with capital, income, and expenses
- Winning/losing conditions based on population and economy

---

## 🧪 Technologies & Skills Used

### 🎮 Game Development
- **Unity Engine** (2022+)
- **C# Programming**
- 2.5D layered rendering
- Unity Animator & prefabs
- Custom UI design (menus, economy HUD)
- Unity Physics and Navigation systems

### 🤖 Simulation & AI
- State-based and heuristic-driven animal behavior (needs-based AI)
- Dynamic herd formation, breeding, and movement
- Poacher and ranger interactions (visibility logic, pathfinding)
- Terrain-aware movement and line-of-sight calculations

### 🔧 Software Engineering
- **Agile methodology** (Scrum with weekly meetings)
- **GitLab** for version control (branching, MR-based development)
- **Collaborative design** (class diagrams, feature planning)
- Modular architecture: each system (animals, economy, UI) developed independently and integrated

### 🧩 System Design
- Modular object-oriented design (e.g., `Animal`, `Predator`, `Herbivore`, `ShopItem`, `TerrainTile`)
- MVC-style separation of data and display logic
- Custom event system for game interactions (e.g. animal hunger triggers pathfinding)

---

## 🚀 How to Run the Project

1. **Install [Unity Hub](https://unity.com/download)**
2. Open the project folder (`Codes/`) with **Unity Editor (2022 or later)**
3. Open the main scene (e.g. `Scenes/MainScene.unity`)
4. Click **Play** in the Unity Editor to run the game

📁 The project structure follows Unity conventions: `Assets/`, `Scenes/`, `Scripts/`, `Prefabs/`.

---

## 📂 Project Contents

- `Assets/` – Unity assets, scripts, prefabs
- `Scenes/` – Game levels (main scene, menus)
- `Scripts/` – Core game logic
- `Docs/` – Design documents, diagrams
- `diagramok.pdf` – Architecture, gameplay plans, UI layouts

---

## 📈 Win/Lose Conditions

- **Win**: Sustain thresholds for capital, visitors, herbivores and carnivores for 3–12 months
- **Lose**: Bankruptcy or extinction of all animals

---

## 🔍 Advanced Features

- 🦁 **Herd behavior and reproduction**
- 🚙 **Tourist jeeps with capacity limits and predefined routes**
- 🕵️ **Poachers** that attack animals and avoid rangers
- 🦺 **Rangers** that protect the park and hunt by assignment
- 🛰️ **Surveillance** with fixed cameras, drones, and airships
- ⛰️ **Terrain types** (rivers, hills) that affect visibility and movement
- 💬 **Heuristics** and random exploration behavior for animals

---

## 🧑‍🤝‍🧑 Team Collaboration

- **Course**: Software Technology – ELTE
- **Tools**: Unity, GitLab, Visual Studio, Figma
- **Teamwork**: Multiple contributors, weekly Scrum meetings, shared planning
- **Practices**: Git-based version control, modular development, peer code reviews
