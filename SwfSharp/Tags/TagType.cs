using System;

namespace SwfSharp.Tags
{
    public enum TagType
    {
        End = 0,
        ShowFrame = 1,
        DefineShape = 2,
        [Obsolete]
        FreeCharacter = 3,
        PlaceObject = 4,
        RemoveObject = 5,
        DefineBits = 6,
        DefineButton = 7,
        JPEGTables = 8,
        SetBackgroundColor = 9,
        DefineFont = 10,
        DefineText = 11,
        DoAction = 12,
        DefineFontInfo = 13,
        DefineSound = 14,
        StartSound = 15,
        [Obsolete]
        StopSound = 16,
        DefineButtonSound = 17,
        SoundStreamHead = 18,
        SoundStreamBlock = 19,
        DefineBitsLossless = 20,
        DefineBitsJPEG2 = 21,
        DefineShape2 = 22,
        DefineButtonCxform = 23,
        Protect = 24,
        [Obsolete]
        PathsArePostScript = 25,
        PlaceObject2 = 26,
        RemoveObject2 = 28,
        [Obsolete]
        SyncFrame = 29,
        [Obsolete]
        FreeAll = 31,
        DefineShape3 = 32,
        DefineText2 = 33,
        DefineButton2 = 34,
        /*[Obsolete]
        MoveObject = 34,*/
        DefineBitsJPEG3 = 35,
        DefineBitsLossless2 = 36,
        DefineEditText = 37,
        /*[Obsolete]
        DefineButtonCxform2 = 37,*/
        [Obsolete]
        DefineMouseTarget = 38,
        [Obsolete]
        DefineVideo = 38,
        DefineSprite = 39,
        [Obsolete]
        NameCharacter = 40,
        ProductInfo = 41,
        /*[Obsolete]
        NameObject = 41,*/
        [Obsolete]
        DefineTextFormat = 42,
        FrameLabel = 43,
        [Obsolete]
        DefineButton2Obsolete = 44,
        [Obsolete]
        DefineBehavior = 44,
        SoundStreamHead2 = 45,
        DefineMorphShape = 46,
        [Obsolete]
        FrameTag = 47,
        DefineFont2 = 48,
        [Obsolete]
        GenCommand = 49,
        [Obsolete]
        DefineCommandObj = 50,
        [Obsolete]
        CharacterSet = 51,
        [Obsolete]
        FontRef = 52,
        [Obsolete]
        DefineFunction = 53,
        [Obsolete]
        PlaceFunction = 54,
        [Obsolete]
        GenTagObject = 55,
        ExportAssets = 56,
        ImportAssets = 57,
        EnableDebugger = 58,
        DoInitAction = 59,
        DefineVideoStream = 60,
        VideoFrame = 61,
        DefineFontInfo2 = 62,
        DebugID = 63,
        EnableDebugger2 = 64,
        ScriptLimits = 65,
        SetTabIndex = 66,
        [Obsolete]
        DefineShape4Obsolete = 67,
        FileAttributes = 69,
        PlaceObject3 = 70,
        ImportAssets2 = 71,
        [Obsolete]
        DoABC = 72,
        DefineFontAlignZones = 73,
        CSMTextSetting = 74,
        DefineFont3 = 75,
        SymbolClass = 76,
        Metadata = 77,
        DefineScalingGrid = 78,
        DoABC2 = 82,
        DefineShape4 = 83,
        DefineMorphShape2 = 84,
        DefineSceneAndFrameLabelData = 86,
        DefineBinaryData = 87,
        DefineFontName = 88,
        StartSound2 = 89,
        DefineBitsJPEG4 = 90,
        DefineFont4 = 91,
        EnableTelemetry = 93,
    }
}