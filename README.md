# Welcome!
This is a repo that makes generating new menu panoramas from the original world easy for resource pack developers or users. Or, you can grab a resource pack that has a custom menu panorama from the [releases page](https://github.com/jacko-png/Minecraft-Menu-Panorama/releases). <Format the releases such that each resource pack is underneath the version>

Procedure:
========================

Preliminaries (all optional)

     • Download and install [Fabric loader](https://fabricmc.net/use/installer/) and [Carpet mod](https://github.com/gnembon/fabric-carpet/releases).
        • When in the game, use the command `/carpet commandTick true` to enable `tick`, then run `/tick freeze` to stop animals from moving
        
    • Download and install [Fabric loader](https://fabricmc.net/use/installer/) and [Iris mod](https://github.com/IrisShaders/Iris/releases).
        • iirc, Iris requires another mod, but it'll spit an error once you load the game and tell you what to do
    
    • Download and install [PowerToys](https://github.com/microsoft/PowerToys/releases) and set it up so that you can use Po&werRename <the ampersand is intentional: that is the shell syntax for ‘the following character's key can be pressed to select this menu item’>
        • You will be instructed how to do this in step [10b](#preparing-files) <preparing-files is close enough>
        
    • Create a new profile in the launcher so that your current settings are not changed with the parameters detailed in step 2 and the settings detailed in step 3
    
    • When in-game, disable clouds so that the stitching doesn't have issues
        • Alternatively, load each view and use a program like AHK to perform the screenshots within 6 frames (the higher the fps, the shorter the interpolation) so that the distance the clouds move is minimised


Preparation
-------------------

1. Download `/User-Custom` and put `world` in your saves folder and `pack` in a known directory.
    • The world will have the title `8091867987493326313` in-game, one of the valid seeds to generate the world.

2. In the launcher, edit your installation's profile so that the resolution is square (such as 1080 x 1080 or 720 x 720) and set the `-Xmx` argument to a little under the RAM your PC has (e.g. if you have 16GB of RAM, you can use `-Xmx12G` or `Xmx14G`.)

3. In-game, set your FOV to 90 and your render distance to 32 (even if you don't get playable fps, the frame will eventually render)
    • Optionally apply a texture pack or shaders

Taking screenshots
--------------------

4. Run the command `/tp @s ~ ~ ~ 0 0` to reset your position and take a screenshot (this guide will assume in-game screenshots with F2)
    • The inital coordinates are 61.48, 75, -68.73

5. Run the command `/tp @s ~ ~ ~ ~90 0` and press F2

6. Repeat step 5 three times

7. Run the command `/tp @s ~ ~ ~ 0 ~-90` and press F2

8. Repeat step 7 once more

Preparing files
---------------------
9. Put the screenshots into a resource pack prepared now or earlier
    • The panorama is in `/assets/minecraft/textures/gui/title/background`
    • A handy trick is to go into the resource pack menu, click ‘Open resource pack folder’, and then go up a level by clicking on the name of your installation's directory (such as `.minecraft`) (Windows File Explorer)\
    
10. 
    a) Rename the files appropriately to match the format `panorama<n>.png` where \<n\> starts at 0 and increments to 5, and should be in the order of the creation fate of the screenshots
    
    b) Using PowerToys, select all the files, open the context menu (right-click), select PowerRename (the W key) and then in the `Replace with` field, input `panorama${}`
        • Verify that `Enumerate items` is enabled (the button on the rightmost side under ‘Text formatting’)
    
    • An error possible here is that in steps 7 and 8, the command `/tp @s ~ ~ ~ 0 ~90` was used instead of `/tp @s ~ ~ ~ 0 ~-90`, which means the top and bottom faces of the panorama's cube are swapped. The sky should be `panorama4.png` and the ground should be `panorama5.png`.
    
11. Edit the `pack.mcmeta` with the appropriate [pack_format](https://minecraft.wiki/w/Tutorials/Creating_a_resource_pack#%22pack_format%22) and your own `description`.
    • Optionally replace the `pack.png` file that's also in `/pack`.

12. Select all the files in `\pack`, open the context menu, cascade out `Send to…` and select `Zip archive`.

13. The `.zip` file created is a resource pack with your new menu panorama.

History:
========================

I made the inital version ages ago, but only rendered about ten chunks in b1.7.3 (there was a bug with Far until b1.8).
Today 2023-09-29, I had the idea of generating more by using an old version of OptiFine which allowed a higher render distance (idk the numbers. Fun fact, all the measurements are in metres in this version, rather than chunks). This resulted in the world which is available on the repo. I used a tool which also ended up in the repo to restore the biomes. This world was opened in 1.20.2, but if you need an older world format just let me know. (I had generated some ice in 1.6.4 and removed it in 1.20.2, which is why I didn't upload the version of the world from 1.6.4) // verify that this is the case by checking the commit history.