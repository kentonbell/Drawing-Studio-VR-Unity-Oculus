# Drawing Studio — VR for Creative Practice and Handwriting Research

![Unity](https://img.shields.io/badge/Unity-2022.3.45f1-000?style=for-the-badge&logo=unity)
![Research](https://img.shields.io/badge/Research-IEEE_AIxVR_2026-00629B?style=flat-square)
![XR](https://img.shields.io/badge/Oculus-XR_Interaction_Toolkit-5B2C83?style=flat-square)

> **Featured research:** read the included IEEE AIxVR 2026 paper, [*Handwriting Recognition in VR for Enhanced Learning and Immersive Interaction Experience*](https://kentonbell.github.io/images/GraduateThesisHandwritingRecognition.pdf). The paper acknowledges Kenton Bell and Matthew Froese for the virtual environment, developed primarily through the Spring 2025 virtual-painting project represented by this repository.
![Thesis Page 1](<images/ThesisPage1.png>)

An immersive drawing studio built in Unity for Oculus VR. Users hold a virtual brush, draw three-dimensional trail-based strokes, choose colors from a physical palette, change brush width, undo individual strokes, or erase the canvas. That creative interaction became the environment underlying continued senior-project exploration and published research into VR handwriting recognition.

## Why the project matters

The repository sits at the intersection of creative tools, human-computer interaction, and assistive/educational technology. As a studio, it tests whether natural hand/controller movements can make digital drawing feel spatial and playful. As a research platform, it provides the interaction foundation for capturing handwriting and evaluating recognition models.

The IEEE paper describes a broader learning pipeline:

```text
write in an individualized VR workspace
               ↓
capture handwritten strokes as an image
               ↓
scan with OCR / handwriting recognition
               ↓
map model confidence to learner feedback
               ↓
practice, self-evaluate, and improve
```

This direction has potential for language learning and individualized support for people experiencing dysgraphia-related challenges.

## Research findings and senior-project direction

The paper's feasibility study used an Oculus Quest 3 and a collision-based virtual pen. Users could change ink color and line width, undo the most recent stroke, clear all writing, save the result, and scan it with an integrated OCR model.

The initial OCR model exposed an important limitation: it often split or misclassified handwritten characters and produced low or misleading confidence. In a subsequent 20-word VR dataset, TrOCR correctly recognized 19 words. That result makes the next engineering direction concrete—integrate a stronger HTR model, improve stroke quality, and evaluate whether feedback genuinely helps learners.

As a senior-thesis/senior-project platform, the studio supports several testable extensions:

- compare collision, ray-cast, controller, hand-tracking, and haptic writing interfaces
- capture consistent handwriting images from Unity
- evaluate character, word, and sentence recognition models
- classify confidence into useful performance feedback
- study engagement, preference, accessibility, and learning progress

## Interaction design

![Interactions](<images/image4.png>)

`TipScript` is the core drawing component. It watches the grabbed brush's grip and trigger values, instantiates a `TrailRenderer` at the brush tip, detaches the completed stroke, and pushes each stroke onto a stack. That stack gives undo a natural last-in/first-out implementation.

In-world controls call the same drawing object:

| Control | Behavior |
|---|---|
| Color swatch | Copies the swatch material color to the brush and future strokes |
| Undo | Pops and destroys the latest trail from the stroke stack |
| Erase all | Clears every recorded trail |
| Plus / minus | Adjusts `TrailRenderer.widthMultiplier` through brush size |

Buttons physically move to a pressed transform on VR-hand collision, reinforcing actions with visible spatial feedback.

## Technical foundation

- Unity XR Interaction Toolkit and Oculus XR provider
- input actions for analog grip and trigger state
- animated virtual hands
- collision-driven world-space controls
- runtime `TrailRenderer` creation and color assignment
- stack-based command history for undo/clear
- configurable brush width and palette materials
- forest/studio environment with terrain and reusable assets

## Run locally

1. Install Unity Hub and Unity `2022.3.45f1`.
2. Add this repository as a Unity project.
3. Open `Assets/DrawingScene.unity`.
4. Configure an Oculus-compatible headset/controller runtime.
5. Press Play in the editor or create an Oculus/Android build.

## Repository map

```text
.
├── Graduate Thesis - Handwriting Recognition.pdf  # IEEE AIxVR 2026 paper
├── Assets/
│   ├── DrawingScene.unity             # Primary studio environment
│   ├── TipScript.cs                   # Brush strokes, color, size, undo
│   ├── Button.cs                      # Palette selection
│   ├── ClearButton.cs / EraseAll.cs   # Stroke-history controls
│   ├── plus.cs / minus.cs             # Brush-width controls
│   ├── AnimateHandController.cs       # Grip/trigger hand animation
│   ├── Colors/ and Prefabs/           # Studio content
│   └── XR/                             # Headset and controller setup
├── Packages/                           # Locked Unity dependencies
└── ProjectSettings/                    # Editor, physics, input, and XR settings
```

## Skills demonstrated

Unity, C#, Oculus XR, embodied interaction, creative-tool design, spatial UI, controller input, collision systems, trail rendering, command history, research prototyping, OCR/HTR pipeline thinking, and translating a class project into a published-research foundation.
