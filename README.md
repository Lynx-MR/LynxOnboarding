# Lynx Onboarding


# Context

This application is launched the first time the headset is switched on. It consists of short tutorials to help users understand how the headset works.

# Description

## Tutorials

0- Terms Of Services
0- Language selection

1- Headset positioning
2- Headset buttons
3- Handtracking gestures
4- Handtracking interfaces

5- Lynx Menu & Exit

## Project architecture

The project sources are located in the Assets/Demo folder.

The Scene is located in Assets/Demo/Scenes/Onboarding.unity.

The project uses the following specific plugins:

- XR Hands 4.0 Preview : Gesture detection is currently only supported by the preview version, Samples are essential for gesture detection.
- Localization : To support multiple languages (by default we've only implemented two languages, English and French).