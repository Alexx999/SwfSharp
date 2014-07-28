using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    public class DefineSceneAndFrameLabelDataTag : SwfTag
    {
        public List<SceneData> Scenes { get; set; }
        public List<FrameData> Frames { get; set; }

        public DefineSceneAndFrameLabelDataTag() : this(0)
        {
        }

        public DefineSceneAndFrameLabelDataTag(int size)
            : base(TagType.DefineSceneAndFrameLabelData, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            var sceneCount = reader.ReadEncodedU32();
            Scenes = new List<SceneData>((int) sceneCount);
            for (int i = 0; i < sceneCount; i++)
            {
                Scenes.Add(new SceneData(reader.ReadEncodedU32(), reader.ReadString()));
            }
            var frameLabelCount = reader.ReadEncodedU32();
            Frames = new List<FrameData>((int)frameLabelCount);
            for (int i = 0; i < frameLabelCount; i++)
            {
                Frames.Add(new FrameData(reader.ReadEncodedU32(), reader.ReadString()));
            }
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            writer.WriteEncodedU32((uint)Scenes.Count);
            foreach (var scene in Scenes)
            {
                writer.WriteEncodedU32(scene.Offset);
                writer.WriteString(scene.Name, swfVersion);
            }
            writer.WriteEncodedU32((uint)Frames.Count);
            foreach (var frame in Frames)
            {
                writer.WriteEncodedU32(frame.FrameNum);
                writer.WriteString(frame.FrameLabel, swfVersion);
            }
        }

        public class SceneData
        {
            public SceneData(uint offset, string name)
            {
                Offset = offset;
                Name = name;
            }

            public uint Offset { get; set; }
            public string Name { get; set; }
        }

        public class FrameData
        {
            public FrameData(uint frameNum, string frameLabel)
            {
                FrameNum = frameNum;
                FrameLabel = frameLabel;
            }

            public uint FrameNum { get; set; }
            public string FrameLabel { get; set; }
        }
    }
}
