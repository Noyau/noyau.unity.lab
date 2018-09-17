namespace Noyau.Lab.Procedural
{
    [System.Flags]
    public enum FaceRenderMask : int
    {
        None = 0,
        Top = 1 << 0,
        Bottom = 1 << 1,
        Right = 1 << 2,
        Left = 1 << 3,
        Front = 1 << 4,
        Back = 1 << 5,
        All = Front | Back | Right | Left | Top | Bottom,
    } // enum: FaceRenderMask
} // namespace