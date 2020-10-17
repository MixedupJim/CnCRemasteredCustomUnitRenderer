Licence

You are free to use this software as you see fit. 

You may modify and distribute the code as long as credit is given. Please include the following in any forks or updates...

"Based on the original CnC Unit Renderer by JimmyArcade Limited"

This software is provided as is and no guarantee is made that it is virus free or that it won't delete the internet! 

Use at your own risk etc, etc, etc!

For more information or to give feedback you can follow me on facebook 
https://www.facebook.com/Jimmy-Arcade-Limited-106959157643124 

Thanks and I hope this helps people make even more exciting stuff for CnC Remastered.

HOW TO USE

The basic principle is quite simple. There is a camera in the scene that is set to rotate at 11.25 degree increments to produce the 32 directions of units in CnC. 

It is then angled at 45 degrees down to give the 2.5D view.

Each frame is rendered and then saved as a TGA file with the correct name for the direction.

GAME WINDOW SETUP
1. The GAME window defines what resolution the unit will be rendered at. Small units (e.g. APC) should be 120x120 while large units (E.g. Mamoth Tank) should be 256x256.
2. I have setup two Display resolutions "SmallUnit (120x120)" and "LargeUnit (256x256)" - choose which ever is most suitable for your unit.

SCENE SETUP
1. Import your 3D model to the scene and set the position to (0,0,0). 
2. Scale the model so that it fits comfortably in the GAME display similar to the DummyObject. You don't want it to clip at the edges when it is rotated at 45 degrees.
3. You can delete the DummyObject cube once you're happy. It's just there for reference.

SCRIPT SETUP
1. Click on the UnitCenterPoint object and you will see the "Rendered Controller Script" component
2. Enter the name of your unit into the "Unit Name" box. This should be the same as the unit name defined in the source code of the game.
3. Starting index sets the starting number for the image file. For most land units this should always be 0. However, turrets start from 32 onwards and air units can have even more.
4. With the "Unit Name" and "Starting Image Index" completeted simply click the "Play" button.
5. You should see your unit spin and then stop. Once it is stopped you can click the "Stop" button.

IMAGES
1. All the images can be found in the root directory.
2. The will be named UnitName-Index.TGA and should be ready for drop into the ART/TEXTURES/SRGB/TIBERIAN_DAWN/UNITS folder of your mod.
3. Add the image file names to the XML/TILESETS?TD_UNITS.XML file

IMPORTANT INFO
The main light is set to green so that the output image can be recoloured by CnC for different multiplayer sides. 
This means the entire unit will change colour depending on the team colour selected. It may not be the best effect but it works!
You could also do it by setting green textures etc to specific parts of the unit if you don't want the entire unit to change colour. 
If you do this then you can set the light to a white colour instead.

At the moment I haven't been able to add the drop shadows automatically so your units won't look quite as connected to the terrain as the original units.
I'm sure there must be a way of doing it using Unity's shadow system.
