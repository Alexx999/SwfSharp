using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class DefineSceneAndFrameLabelDataTag : SwfTag
    {
        [XmlArrayItem("Scene")]
        public List<SceneData> Scenes { get; set; }
        [XmlArrayItem("Frame")]
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

        [Serializable]
        public class SceneData
        {
            [XmlAttribute]
            public uint Offset { get; set; }
            [XmlAttribute]
            public string Name { get; set; }

            private SceneData()
            {}

            public SceneData(uint offset, string name)
            {
                Offset = offset;
                Name = name;
            }
        }

        [Serializable]
        public class FrameData
        {
            [XmlAttribute]
            public uint FrameNum { get; set; }
            [XmlAttribute]
            public string FrameLabel { get; set; }

            private FrameData()
            {}

            public FrameData(uint frameNum, string frameLabel)
            {
                FrameNum = frameNum;
                FrameLabel = frameLabel;
            }
        }
    }
}
