# Virtual Window for Unity

Allows the screen to simulate a "window"- users can peek in, look around, even move around a 3-dimensional scene generated in Unity.

This program was first demonstrated through [the museum installation Yayoi Virtual Window](https://github.com/Yasu31/Yayoi_Virtual_Window) in the [Osaka Museum of Yayoi Culture 大阪府立弥生文化博物館](http://www.kanku-city.or.jp/yayoi/), a museum dedicated to the Yayoi period of Japanese history.

# todo- add video
# todo- more demo scenes, like from this [architecture model](https://free3d.com/3d-model/house-modern-40978.html)
# Features
* **displays 3D scenery** that behaves as if the computer display was a **window**
* look & move around the scene freely- please see the video to grasp the concept.
* (relatively) easily integrable to Unity scenes
* does **not** require special hardware to run (only a webcam and a somewhat fast computer)

You are free to use this for your projects (but do let me know if you do, because I'd love to hear about your ideas and implementations! I can link to it here, if you want)

# User Manual
## Requirements
### webcam & [FaceOSC](https://github.com/kylemcdonald/ofxFaceTracker/releases) software
to track user's face. This software is required to be running while Virtual Window is running. Sends face position & gesture data over [OSC](http://opensoundcontrol.org/introduction-osc), a network protocol for transferring data between apps and devices.
## Control

### to look around scene
move your head around to look around. The display changes accordingly.

### to move around scene
arrow keys

mouse (left&right button to go left&right, scroll wheel to go forward & back)

move your head around **more**.(the software sets up a virtual "fence" about 100cm sideways and 60cm front-back in front of the screen, of which if you pass through, the box which the camera is in moves in that direction too)(this feature may feel more of a nuisance, can toggle with "m" key)

### reset camera position
spacebar

### set up parameters
I strongly recommend setting up various parameters for your environment, to ensure a realistic rendering. In the sidebar of the Main Camera, these public parameters can be set:
* fovDegrees- field of view of the camera, in degrees
* numerator- divides this value by the face size to get the face's distance from the screen.
* screenDimension- a Vector2 object. x is the width, y is the height. in centimeters
* cameraPos- a Vector2 object. Sets the offset of the camera. For normal configurations(in which the camera is above the display, as in most notebook PCs), this can be kept to zero. If not, set the horizontal and vertical offset of the camera from that position(right above the screen) in centimeters.

## Finding out the optimal parameters
The first two parameters("fovDegrees" and "numerator") are quite unintuitive, however it is quite important to set them to an appropriate value in order to calculate the correct face position of the user. So there is an easy way to find the optimal value- **set the checkbox "set_parameters_mode" before running**, and you can set the fovDegrees with the right&left arrows, and the numerator with the up&down arrows. The values, along with the face positions, are displayed as well. It is recommended to:
1. check the box "set_parameters_mode" and run the scene
1. using the up&down arrows, change the value of "numerator" until the z value shows the correct distance of your face from the screen(in centimeters)
1. using the right&left arrows, change the value of "fovDegrees" until the x or y value shows the correct offset of your face from the center of screen(in centimeters)
1. Once the face position is satisfactory, take note of the numerator&fovDegrees values.
1. quit the scene, change the values to the optimal ones, and deselect the set_parameters_mode checkbox.

# To use Virtual Window yourself
## creating a scene
**todo: create template scene!**
(TODO) The template scene already includes the necessary scripts and objects to run Virtual Window, so by adding objects to this scene, you can readily generate an interactive scene. If you're going to add this to an existing scene, please follow the steps below...
## adding the Virtual Window program to an existing scene
(TODO- make it very easy to follow along! make a video)

# How it works
(TODO)
