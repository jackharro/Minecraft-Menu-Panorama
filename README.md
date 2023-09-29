Procedure:
========================

Preliminaries (all optional):
------------------------

     • Download and install [Fabric loader](https://fabricmc.net/use/installer/) and [Carpet mod](https://github.com/gnembon/fabric-carpet/releases).
        • When in the game, use the command `/carpet commandTick true` to enable `tick`, then run `/tick freeze` to stop animals from moving
        
    • Download and install [Fabric loader](https://fabricmc.net/use/installer/) and [Iris mod](https://github.com/IrisShaders/Iris/releases).
        • iirc, Iris requires another mod, but it'll spit an error once you load the game and tell you what to do
    
    • Download and install [PowerToys](https://github.com/microsoft/PowerToys/releases) and set it up so that you can use Po&werRename // the ampersand is intentional: that is the shell syntax for ‘the following character's key can be pressed to select this menu item’
        • You will be instructed how to do this in step STEP_NUMBER_HERE
        
    • Create a new profile in the launcher so that your current settings are not changed with the parameters detailed in step 2 and the settings detailed in step 3
    
    • When in-game, disable clouds so that the stitching doesn't have issues
        • Alternatively, load each view and use a script like AHK to perform the screenshots within 6 frames (the higher the fps, the shorter the interpolation) so that the distance the clouds move is minimised


Preparation
------------------

1. Download `Generating Large World/latest` and put it into your saves folder

2. In the launcher, edit your installation's profile so that the resolution is square (such as 1080 x 1080 or 720 x 720) and set the `-Xmx` argument to a little under the RAM your PC has (e.g. if you have 16GB of RAM, you can use `-Xmx12G` or `Xmx14G`.)

3. In-game, set your FOV to 90 and your render distance to 32 (even if you don't get playable fps, the frame will eventually render)
    • Optionally apply a texture pack or shaders

Taking screenshots


4. Run the command `tp @s ~ ~ ~ 0 0` to reset your position and take a screenshot (this guide will assume in-game screenshots with F2)

5. Run the command

History:
========================

