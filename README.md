# Virtual Window for Unity

Allows the screen to simulate a "window"- users can peek in, look around, even move around a 3-dimensional scene generated in Unity.

This program was first demonstrated in the [大阪府立弥生文化博物館](http://www.kanku-city.or.jp/yayoi/), a museum dedicated to the Yayoi period of Japanese history. As such, the scenes featured in this program are all(for now) related to the Yayoi period and its culture. However, I believe there are wider applications that are achievable from a such system as this.
# todo- Settings for display are accessible while running (can configure “numerator” intuitively)
# todo- add video
# Features
* **displays 3D scenery** that behaves as if the computer display was a **window**
* look & move around the scene freely
* (relatively) easily integrable to Unity scenes
* does **not** require special hardware to run (only a webcam and a somewhat fast computer)

I think it's easier to get the idea by watching the video linked above.(TODO)

You are free to use this for your projects (but do let me know if you do, because I'd love to hear about your ideas and implementations! I can link to it here, if you want)

# User Manual
## Requirements
### webcam & [FaceOSC](https://github.com/kylemcdonald/ofxFaceTracker/releases) software
to track user's face. This software is required to be running while Virtual Window is running. Sends face position & gesture data over [OSC](http://opensoundcontrol.org/introduction-osc), a network protocol for transferring data between apps and devices.
## Control
### switch between scenes
Enter(return) key

Open mouth (like yawning) for 2 seconds (this may not work at times)
### to look around scene
move your head around to look around. The display changes accordingly.

### to move around scene
arrow keys

mouse (left&right button to go left&right, scroll wheel to go forward & back)

move your head around **more**.(the software sets up a virtual "fence" about 100cm sideways and 60cm front-back in front of the screen, of which if you pass through, the box which the camera is in moves in that direction too)(this feature may feel more of a nuisance, can toggle with "m" key)

### reset camera position
spacebar

# To use Virtual Window yourself
## creating a scene
**todo: create template scene!**
(TODO) The template scene already includes the necessary scripts and objects to run Virtual Window, so by adding objects to this scene, you can readily generate an interactive scene. If you're going to add this to an existing scene, please follow the steps below...
## adding the Virtual Window program to an existing scene
(TODO- make it very easy to follow along!)

# How it works
(TODO)
