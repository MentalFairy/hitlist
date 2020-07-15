namespace Skrptr
{
    public enum SlideDirection { Left, Right, Up, Down };
    public enum RotateType { Absolute, Relative };
    [System.Flags]
    public enum SkrptrEvent {

        None = 0,
        Click = 1,
        Select = 2,
        Deselect = 4,
        Enable = 8,
        Disable = 16,
        Hide = 32,
        Show = 64,
        Lock = 128,
        Unlock = 256,
        HoverEnter = 512,
        HoverExit = 1024,
        Check = 2048,
        Uncheck = 4096,
        LongPress = 8192,
        Loop = 16384,
    };
    [System.Flags]
    public enum SkrptrDirection
    {
        None = 0,
        Up = 1,
        Down= 2,
        Left= 4,
        Right = 8,
        Back = 16,
        Click = 32,
    };

    public enum SkrptrInputType
    {
        None,
        Mouse,
        Keyboard,
        Touch
    }
}
