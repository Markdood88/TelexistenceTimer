Topics to elaborate on:

1. What should be considered in order to render the application usable in iPad devices?

(Please note I don't have an iPad to test functionality / haven't deployed to iPad before, so I may have missed some important considerations.)

When exporting this Unity project to an iPad, first we should make sure that the iPad software supports the current version of Unity (2021.3.19f in the case of this project). Unity provides support for iOS platforms, but depending on how old the model is, and its current firmware, the project settings may need to be configured to optimize performance for iPad devices. Since the majority of this project is a Manager Script + Buttons + Text Displays, there shouldn't be any noticeable graphical/cpu strain on the device. iPad devices have limited processing power and memory compared to Windows devices, but this project isn't so complex to strain the CPU.

We should also be mindful of the screen size and resolution of the iPad. Since the Ipad 11 has a display aspect ratio equivalent to A4, it should be able to display the contents of this project without any cutoff. However and iPad pro 12.9 supports A4 with a small margin on the left and right side. So the Unity background might be visible in this case. Depending on your preference, you might want to add an extra black background behind the Timer Canvas to disguise the default unity background. The buttons should all be large enough to support the user's touch on the iPad. On windows, the project is configured to display at 420x594 (A4) resolution. On iPad, this may be a bit small, so all components (Background, TextMesh, Buttons) can be scaled larger if the application doesn't automatically scale to full screen / if there is no pinch-zoom function.

To export the project to an iPad, it should also be considered if the raw application file will be uploaded to the iPad or if the app will be submitted to the AppStore. On android devices, you can usually export a .apk file or simply connect the device to your development PC and build and deploy while developing. So depending on the scenario for development, it would be better to build and deploy to a test iPad, and distribute the final file once the visual scale has been verified.

2. What should be considered in order to render the application usable in VR devices?

(Please note my experience in Unity VR projects is limited to HTC Vive/Pro Eye, Varjo, Quest Rift S, Quest 2/Pro, so I may have missed some important considerations on AR devices, or other headsets.)

When exporting this project to a VR device, first we should consider the functionality of the VR device. If it is exported to Quest, we should remember that the Quest headsets are like standalone android devices, with limited CPU and GPU power. However of course, since this timer isn't so CPU expensive, there shouldn't really be a problem at runtime. If the headset will remain connected to the PC and act solely as a display (in the case of Quest + Link Cable, Vive, Varjo) we can consider the power of the development PC instead for CPU operations, and GPU depending on the device output port (either from GPU, or USB-C port).

In the VR Scene, this project is designed to maintain its A4 size aspect ratio. This means that it should either be docked to the side of the user's view as an HUD, or an OVR/XR Interactable object similar in shape to a clipboard. Since we can move objects closer or further away in VR, the scale of the object doesn't matter so much here, although it should be an acceptable resolution when closest to the eye, which it appears to be in desktop mode.

Depending on the VR interaction system you are using, and whether you plan for users to use controllers or finger trackers, you will need to add appropriate interactable components to the buttons. In many cases with the vive, controllers are used with laser raycasting to make selections in VR canvases. However, sometimes instead of the straight laser, the parabolic-style teleportation raycast is used instead. The components that facilitate the raycast colliders will need to be set to choose which raycasts to listen to and which to ignore. In the case of Oculus Finger Tracking interaction, we should include OVR Interactables on the buttons so that they can be pressed when the finger mesh overlaps them. In the case of a user who has arm impairments, there may need to be eye tracking, using something like the Tobii Eyetracking SDK, which can provide eye raycasts to select an object, and a physical button can be used to trigger the press functions. 

3. Any extra remarks regarding your code or design decisions

For this project, my design choice was to create a script which would act as the clock/timer/stopwatch Manager. While all of the functionality happens within the script itself, it has 3 configurable output textmesh objects to be used as displays (in case my visual A4 design is to be scrapped). However in some cases displays are not needed and the function logic is all that needs to be accessed for other objects to make decisions. With this in mind, my goal was to create a set of base functions in a neat order so that all 3 Timer Components could be accessed, and set if necessary.

In typical fashion, the update function leads to several other functions that run on every update. Namely updateClock, updateTimer, updateStopwatch.

Clock - The clock has an overloaded updateClock() function to pull from system time, or to use a System.DateTime as input. It then updates clockText, and if the optionalDateText is set, it updates the date as well in MM/DD/YYYY format. It also has some get functions to return hour/minute/second/day/month/year.

Timer - updateTimer() checks if timerActive is true, then starts the timer subtraction each frame. If the timer value < 0, it turns active mode off, and sets its value to 0. The maximum value of the timer in minutes is also public, so it can be set in case we need a larger timer (up to 1 hour recommended since hours are not shown). Originally the plan was to use a System.DateTime to manage this variable, but some operations cause console errors to display, so I switched to primitive variables instead.

There is a timerAdd() in case anyone needs to manually add x minutes, y seconds, or z milliseconds to the timer. There is also timerGetMins(), timerGetSeconds(), and timerGetMilliseconds() so that the timer value can be accessed by other gameObjects.

Finally there is a collection of UI button functions which can help make button interaction more simple. Such as timerAddOneMin(), timerSubOneMin(), timerToggle().

Design Choice: The timerStart() function saves the current value of the timer, so that timerReset() reloads the timer to that last saved value. This is usually intuitive with most timer systems, and would be quite annoying if the timer had to be manually raised from 0 every time.

Stopwatch - The stopwatch is just a backwards timer. Counting up from 0 every frame if active, setting time to maximumStopwatchMinutes if the value is above, and checking if stopwatchActive is true. Reset always resets to 0, the PlayPause button is really all that is necessary here for toggling active mode, but Start (only sets active true) is helpful for verbal clarification. Users will more likely want to press 'Start!' than 'Play/Pause'.