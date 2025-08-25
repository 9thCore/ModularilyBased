namespace ModularilyBased.API.Register
{
    /// <summary>
    /// Represents a face's type
    /// </summary>
    public enum FaceType
    {
        /// <summary>
        /// Default value, no face
        /// </summary>
        None,
        /// <summary>
        /// Wall face
        /// </summary>
        Wall,
        /// <summary>
        /// Corridor cap face (where two corridors - or a corridor and room - may connect)
        /// </summary>
        CorridorCap,
        /// <summary>
        /// Center of a room (or multiple, if the room is large)
        /// </summary>
        Center,
        /// <summary>
        /// Where ladders may snap
        /// </summary>
        Ladder,
        /// <summary>
        /// ACU wall side
        /// </summary>
        WaterPark
    }
}
