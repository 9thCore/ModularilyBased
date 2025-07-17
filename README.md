Subnautica library that expands on the base-building system with a faux base module system.

Base modules are stinky, by being heavily interwoven with the base system, so how about a library that offers the snapping possibilities of modules while using normal constructables?

Usage:  
- `ModuleSnapper.SetSnappingRules(Constructable, ModuleSnapper.RoomRule, PlacementRule, [TransformationRule = null])`
	- Adds snapping capability to the `Constructable` provided.  
	- Most `ConstructableFlags` will not change behaviour, once this is run. Recommended to omit `ConstructableFlags.Base` so the module doesn't stick to objects inside of the base, where it may not be placeable.  
	- `ModuleSnapper.RoomRule` is a `Flag` enumeration, such that multiple rooms can be provided to be built inside of.  
	- `PlacementRule` is a class that communicates with the library, to know if the module can be built on a specified face. `PlacementRule.SnapType` is a `Flag` enumeration, such that multiple faces can be provided to build on. The rule has a built-in derivative:  
		- `FilteredPlacementRule`, which, if the base `PlacementRule` passes its checks, then it checks each of its `filter`s.  
			- A filter must extend `BasePlacementFilter`.  
	- `TransformationRule` represents how the module will be placed in-world, after a valid snapping is found.  
		- It has two components: the `PositionRule` and `RotationRule`.  
		- `PositionRule` decides the module's position, while `RotationRule` decides the module's rotation.  
		- For most use-cases, `OffsetPositionRule` and either `OffsetRotationRule` or `SnappedRotationRule` will suffice.  
			- `OffsetPositionRule` describes a positional offset, *relative to the snap point*.  
			- `OffsetRotationRule` describes a rotational offset (*on the Y axis*), *relative to the snap point*.  
			- `SnappedRotationRule` will snap the player's rotation to the given `snap` (integer in the range of `[1-360]`), then apply the rotational offset provided.  
			- A rotation rule will not accept player rotation if the module does not have the flag `ConstructableFlags.Rotatable`.  
		- If a component rule isn't provided, it will use the default `NoOffset` for the given rule.  