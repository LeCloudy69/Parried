# Parried
Parried is a fast-paced 3D first-person action game built in Unity, focusing on fluid movement and precision defensive mechanics. Navigate through simple toon environment using advanced movement techniques and master the parry system to survive.  


## Demo 

https://github.com/user-attachments/assets/7e46fa76-8fa3-434e-90ce-2f60e26e2f16

Demo created 12/17/25

## Gameplay & Mechanics
- Parry System: A core combat mechanic allowing players to deflect incoming threats. The timing is tuned for high-risk, high-reward gameplay.
- Advanced Movement: Features a custom Bunnyphopping (BHOP) system to maintain momentum, alongside standard first-person navigation.
- Dynamic Hazards: Interactive "Killzone" environments that require precise platforming and awareness.
- Configurable Input: Fully integrated with the Unity New Input System. Keybinds for jump and parry can be remapped directly within the Inspector for player preference.


## Actions | Default Key | Notes
Move  | "W, A, S, D"  | Standard directional movement  
Jump  |     Space     | Common button  
Parry |      F        | Configurable in Inspector  


## Credits and Development
Art and Assets
- Models: Original character created my cousin. Enemy and enviroment is created by me.
- Rigging: All character rigging was performed manually by me.
- Environment: Constructed using simple geometric shapes utilizing custom Toon Materials for a stylized aesthetic.
- UI/UX: Fully custom UI elements and health bar animations, inspired by the interface logic found in Assignment 4 of my "CS461: Computer Games Programming" class at Umass Boston.

## Engineering and Logic
- Movement Physics: Core first-person movement logic was developed following industry-standard patterns.
    - Player movement made following YouTube video “FIRST PERSON MOVEMENT in 10 MINUTES - Unity Tutorial”: https://youtu.be/f473C43s8nE?si=2xYz3-hQMMC_JJBD 
- BHOP Logic: Designed and implemented a custom momentum-based jumping system.
- Hazard Systems: Developed a modular hazard script based on established "killzone" logic to streamline environmental danger.
- General Scripting: Logic scripts were tuned and debugged in collaboration with AI tools to ensure optimal performance and clean code structures.

## Build and Installation instructions
- Clone the repository.
- Open the project in Unity (ensure the New Input System package is installed).
- The project includes pre-configured Build Settings. Navigate to File > Build Settings and select your target platform.
- Select Build and Run.

## Developer Retrospective
- This project was a deep dive into 3D game architecture within a compressed timeline. One of the primary challenges was balancing the solo development of both assets and code. While the project was completed individually, the process highlighted the value of collaborative brainstorming in the 3D space. Future iterations would benefit from expanded peer-review sessions to further bolster creative mechanics and movement tuning.

## Potential additions to the project
- Endless mode: Scaling Health of enemies, number of enemies, and reduced enemy shooting cooldown.
- Difficulty Option: Easy, Medium, or Hard that effects the game scaling. 

