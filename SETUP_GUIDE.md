# CS-Shooter Setup Guide

## Prerequisites
- Unity 2021 LTS or higher
- TextMesh Pro (included in newer Unity versions)

## Installation Steps

### 1. Create Folders Structure
In Assets folder, create:
```
Assets/
├── Scripts/
│   ├── Player/
│   ├── Weapon/
│   ├── Enemy/
│   ├── Manager/
│   └── UI/
├── Prefabs/
├── Scenes/
and Models/
```

### 2. Create Main Scene
1. Right-click in Scenes folder → Create Scene
2. Name it "MainGame"
3. Save it

### 3. Setup Player
1. Create empty GameObject → Name: "Player"
2. Add components:
   - CharacterController
   - PlayerController script
   - PlayerHealth script
   - Tag: "Player"
3. Create child object "Camera" with Camera component
4. Create child object "FirePoint" (for bullets)
5. Position camera at eye level

### 4. Setup Weapon
1. Create empty GameObject child of Player → Name: "Weapon"
2. Add components:
   - WeaponManager script
   - Assign FirePoint to firePoint field

### 5. Create Enemy Prefab
1. Create Capsule → Name: "Enemy"
2. Add components:
   - NavMeshAgent
   - Enemy script
   - Collider (Box or Capsule)
3. Drag into Prefabs folder to create prefab
4. Delete from scene

### 6. Bake NavMesh
1. Window → AI → Navigation
2. Select all floor objects → Mark as "Walkable"
3. Click "Bake"

### 7. Create Spawn Points
1. Create empty GameObjects around the scene
2. Name them "SpawnPoint_1", "SpawnPoint_2", etc.
3. Position them strategically

### 8. Setup Wave Manager
1. Create empty GameObject → Name: "WaveManager"
2. Add WaveManager script
3. Assign:
   - Enemy prefab
   - Spawn points array
4. Set initial enemy count (3-5)

### 9. Create UI Canvas
1. Right-click → UI → Canvas
2. Create TextMeshPro elements:
   - AmmoText (top right)
   - HealthText (top left)
   - HealthBar (under health text)
   - WaveText (center top)
   - EnemyCountText (center top, below wave)

### 10. Setup GameUI
1. Create empty GameObject → Name: "UIManager"
2. Add GameUI script
3. Assign all UI elements to script fields

### 11. Player Input Settings
Windows → Input Manager:
- Horizontal: A/D keys
- Vertical: W/S keys
- Mouse X/Y: Mouse movement

### 12. Create Floor
1. Create Plane → Name: "Floor"
2. Scale it (X: 50, Y: 1, Z: 50)
3. Mark as "Walkable" for NavMesh

## Controls
- **W/A/S/D** - Move
- **SPACE** - Jump
- **LEFT SHIFT** - Sprint
- **MOUSE** - Look around
- **LEFT CLICK** - Shoot
- **R** - Reload
- **ESC** - Unlock cursor (optional)

## Testing
1. Press Play
2. Walk around and shoot enemies
3. Enemies should spawn in waves
4. Health and ammo should display
5. Wave counter should increase

## Troubleshooting

**Enemies not moving:**
- Bake NavMesh again
- Check NavMeshAgent is enabled

**UI not showing:**
- Check Canvas is set to Screen Space - Overlay
- Verify TextMeshPro is imported

**Player can't shoot:**
- Verify FirePoint is assigned
- Check enemy prefab has collider

**Game too easy/hard:**
- Adjust enemy health
- Adjust bullet damage
- Change enemy count per wave

## Next Steps
- Add weapon models (3D models)
- Add sound effects
- Add particle effects for bullets
- Create multiple weapon types
- Add difficulty settings
- Add score system
