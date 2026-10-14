# Lompat Anjay

## About

Lompat Anjay is a 2D arcade platformer built as a university assignment, inspired by the
jumping mini-game from *Pou*. The goal is simple: jump as high as you can without falling.
Before the game starts, players choose between two characters — each with their own sprite.

<br>

<table>
  <tr>
    <td>
      <img src="LompatAnjay.gif" width="100%">
    </td>
    <td valign="top">

**Platform:** WebGL (Browser) <br>
**Engine:** Unity <br>
**Language:** C# <br>

<br>

[![Play Game](https://img.shields.io/badge/▶_Play_on_itch.io-FF6B35?style=for-the-badge&logoColor=white)](https://nataalyly.itch.io/lompat-anjay)
[![GitHub](https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white)](https://github.com/nataalyly/Lompat-Anjay)

  </td>
  </tr>
</table>

---

## Gameplay

Select your character, then jump from platform to platform as high as you can.
Platforms spawn automatically — just keep moving and don't fall.

## Controls

| Input | Action |
|---|---|
| `A` / `←` | Move Left |
| `D` / `→` | Move Right |

> The character jumps automatically on platform contact.

## Key Features

- **Character Selection** — choose between two playable characters before each run
- **Endless Platforming** — platforms spawn procedurally with increasing difficulty the higher you climb
- **Sprite-based Animation** — jump animation handled through sprite switching
- **Persistent High Score** — best score is saved locally across sessions
- **Full Audio** — background music and sound effects throughout gameplay
- **Pause & Resume** — players can pause at any time during a run

## Personal Contribution

Solo developer on this project — handled everything end to end:
- **Game Design** — defined core loop, character concept, and difficulty curve
- **Programming** — built all gameplay systems from scratch in Unity with C#
- **Balancing** — tuned platform spawn rate and spacing for a fair but challenging experience
- **Art** — used free assets from [Kenney](https://kenney.nl); no original art created

## Code Architecture

- **Singleton pattern** via `GameManager` and `AudioManager` to handle global state, score tracking, and audio across scenes
- **`DontDestroyOnLoad`** to persist managers through scene transitions
- **`PlayerPrefs`** for local high score persistence
- **Unity's new Input System** for keyboard input handling
- **Procedural spawning** logic with difficulty scaling based on height progression
- **`Rigidbody2D.linearVelocity`** for physics-based movement to ensure reliable collision

## Development Duration

~2 weeks — solo, from initial setup to WebGL deployment on itch.io

## Notes

- Best played on desktop via Google Chrome or Microsoft Edge.
- Found a bug? Feel free to open an issue.

## Author

Made by [Natnat](https://github.com/nataalyly)
