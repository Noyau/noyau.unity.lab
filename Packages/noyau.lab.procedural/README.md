# Noyau's Laboratory #

## Procedural Package ##

Implementing procedural tools.

### Procedural Planet ###

**version: 0.0.1-preview**

For now, I'm just following tutorials on from [here](https://www.youtube.com/watch?v=QN39W020LqU&list=PLFt_AvWsXl0cONs3T0By4puYy6GM22ko8). I'll see later what I can do with these tools...

**Change-log**

* implemented noise filter tools and shape generator
* implemented settings tools
* implemented planet/sphere generation tools

**Todo**

* add a "Create Procedural Planet" when "right-clicking" in scene hierarchy

**Known Issues**

* visible seams between "TerrainFaces" for lower resolutions (below ~60)
* calling *Initialize* during *OnValidate* can throw warning messages
> SendMessage cannot be called during Awake, CheckConsistency, or OnValidate
> UnityEngine.GameObject:.ctor(String)
> Noyau.Lab.Procedural.Planet:Initialize() (at Packages/noyau.lab.procedural/Procedural/Scripts/Planet.cs:45)
> Noyau.Lab.Procedural.Planet:OnValidate() (at Packages/noyau.lab.procedural/Procedural/Scripts/Planet.cs:29)
> UnityEngine.GameObject:AddComponent()
> Noyau.Lab.Procedural.Editor.PlanetEditor:CreatePlanet(MenuCommand) (at Packages/noyau.lab.procedural/Procedural.Editor/Scripts/PlanetEditor.cs:14)