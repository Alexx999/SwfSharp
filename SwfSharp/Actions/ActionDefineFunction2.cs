using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionDefineFunction2 : ActionBase
    {
        [XmlAttribute]
        public string FunctionName { get; set; }
        [XmlAttribute]
        public byte RegisterCount { get; set; }
        [XmlAttribute]
        public bool PreloadParentFlag { get; set; }
        [XmlAttribute]
        public bool PreloadRootFlag { get; set; }
        [XmlAttribute]
        public bool SuppressSuperFlag { get; set; }
        [XmlAttribute]
        public bool PreloadSuperFlag { get; set; }
        [XmlAttribute]
        public bool SuppressArgumentsFlag { get; set; }
        [XmlAttribute]
        public bool PreloadArgumentsFlag { get; set; }
        [XmlAttribute]
        public bool SuppressThisFlag { get; set; }
        [XmlAttribute]
        public bool PreloadThisFlag { get; set; }
        [XmlAttribute]
        public bool PreloadGlobalFlag { get; set; }
        [XmlArrayItem("Parameter")]
        public List<RegisterParam> Parameters { get; set; }

        [XmlArrayItem("ActionUnknown", typeof(ActionUnknown))]
        [XmlArrayItem("ActionNextFrame", typeof(ActionNextFrame))]
        [XmlArrayItem("ActionPreviousFrame", typeof(ActionPreviousFrame))]
        [XmlArrayItem("ActionPlay", typeof(ActionPlay))]
        [XmlArrayItem("ActionStop", typeof(ActionStop))]
        [XmlArrayItem("ActionToggleQuality", typeof(ActionToggleQuality))]
        [XmlArrayItem("ActionStopSounds", typeof(ActionStopSounds))]
        [XmlArrayItem("ActionAdd", typeof(ActionAdd))]
        [XmlArrayItem("ActionSubtract", typeof(ActionSubtract))]
        [XmlArrayItem("ActionMultiply", typeof(ActionMultiply))]
        [XmlArrayItem("ActionDivide", typeof(ActionDivide))]
        [XmlArrayItem("ActionEquals", typeof(ActionEquals))]
        [XmlArrayItem("ActionLess", typeof(ActionLess))]
        [XmlArrayItem("ActionAnd", typeof(ActionAnd))]
        [XmlArrayItem("ActionOr", typeof(ActionOr))]
        [XmlArrayItem("ActionNot", typeof(ActionNot))]
        [XmlArrayItem("ActionStringEquals", typeof(ActionStringEquals))]
        [XmlArrayItem("ActionStringLength", typeof(ActionStringLength))]
        [XmlArrayItem("ActionStringExtract", typeof(ActionStringExtract))]
        [XmlArrayItem("ActionPop", typeof(ActionPop))]
        [XmlArrayItem("ActionToInteger", typeof(ActionToInteger))]
        [XmlArrayItem("ActionGetVariable", typeof(ActionGetVariable))]
        [XmlArrayItem("ActionSetVariable", typeof(ActionSetVariable))]
        [XmlArrayItem("ActionSetTarget2", typeof(ActionSetTarget2))]
        [XmlArrayItem("ActionStringAdd", typeof(ActionStringAdd))]
        [XmlArrayItem("ActionGetProperty", typeof(ActionGetProperty))]
        [XmlArrayItem("ActionSetProperty", typeof(ActionSetProperty))]
        [XmlArrayItem("ActionCloneSprite", typeof(ActionCloneSprite))]
        [XmlArrayItem("ActionRemoveSprite", typeof(ActionRemoveSprite))]
        [XmlArrayItem("ActionTrace", typeof(ActionTrace))]
        [XmlArrayItem("ActionStartDrag", typeof(ActionStartDrag))]
        [XmlArrayItem("ActionEndDrag", typeof(ActionEndDrag))]
        [XmlArrayItem("ActionStringLess", typeof(ActionStringLess))]
        [XmlArrayItem("ActionThrow", typeof(ActionThrow))]
        [XmlArrayItem("ActionCastOp", typeof(ActionCastOp))]
        [XmlArrayItem("ActionImplementsOp", typeof(ActionImplementsOp))]
        [XmlArrayItem("ActionRandomNumber", typeof(ActionRandomNumber))]
        [XmlArrayItem("ActionMBStringLength", typeof(ActionMBStringLength))]
        [XmlArrayItem("ActionCharToAscii", typeof(ActionCharToAscii))]
        [XmlArrayItem("ActionAsciiToChar", typeof(ActionAsciiToChar))]
        [XmlArrayItem("ActionGetTime", typeof(ActionGetTime))]
        [XmlArrayItem("ActionMBStringExtract", typeof(ActionMBStringExtract))]
        [XmlArrayItem("ActionMBCharToAscii", typeof(ActionMBCharToAscii))]
        [XmlArrayItem("ActionMBAsciiToChar", typeof(ActionMBAsciiToChar))]
        [XmlArrayItem("ActionDelete", typeof(ActionDelete))]
        [XmlArrayItem("ActionDelete2", typeof(ActionDelete2))]
        [XmlArrayItem("ActionDefineLocal", typeof(ActionDefineLocal))]
        [XmlArrayItem("ActionCallFunction", typeof(ActionCallFunction))]
        [XmlArrayItem("ActionReturn", typeof(ActionReturn))]
        [XmlArrayItem("ActionModulo", typeof(ActionModulo))]
        [XmlArrayItem("ActionNewObject", typeof(ActionNewObject))]
        [XmlArrayItem("ActionDefineLocal2", typeof(ActionDefineLocal2))]
        [XmlArrayItem("ActionInitArray", typeof(ActionInitArray))]
        [XmlArrayItem("ActionInitObject", typeof(ActionInitObject))]
        [XmlArrayItem("ActionTypeOf", typeof(ActionTypeOf))]
        [XmlArrayItem("ActionTargetPath", typeof(ActionTargetPath))]
        [XmlArrayItem("ActionEnumerate", typeof(ActionEnumerate))]
        [XmlArrayItem("ActionAdd2", typeof(ActionAdd2))]
        [XmlArrayItem("ActionLess2", typeof(ActionLess2))]
        [XmlArrayItem("ActionEquals2", typeof(ActionEquals2))]
        [XmlArrayItem("ActionToNumber", typeof(ActionToNumber))]
        [XmlArrayItem("ActionToString", typeof(ActionToString))]
        [XmlArrayItem("ActionPushDuplicate", typeof(ActionPushDuplicate))]
        [XmlArrayItem("ActionStackSwap", typeof(ActionStackSwap))]
        [XmlArrayItem("ActionGetMember", typeof(ActionGetMember))]
        [XmlArrayItem("ActionSetMember", typeof(ActionSetMember))]
        [XmlArrayItem("ActionIncrement", typeof(ActionIncrement))]
        [XmlArrayItem("ActionDecrement", typeof(ActionDecrement))]
        [XmlArrayItem("ActionCallMethod", typeof(ActionCallMethod))]
        [XmlArrayItem("ActionNewMethod", typeof(ActionNewMethod))]
        [XmlArrayItem("ActionInstanceOf", typeof(ActionInstanceOf))]
        [XmlArrayItem("ActionEnumerate2", typeof(ActionEnumerate2))]
        [XmlArrayItem("ActionBitAnd", typeof(ActionBitAnd))]
        [XmlArrayItem("ActionBitOr", typeof(ActionBitOr))]
        [XmlArrayItem("ActionBitXor", typeof(ActionBitXor))]
        [XmlArrayItem("ActionBitLShift", typeof(ActionBitLShift))]
        [XmlArrayItem("ActionBitRShift", typeof(ActionBitRShift))]
        [XmlArrayItem("ActionBitURShift", typeof(ActionBitURShift))]
        [XmlArrayItem("ActionStrictEquals", typeof(ActionStrictEquals))]
        [XmlArrayItem("ActionGreater", typeof(ActionGreater))]
        [XmlArrayItem("ActionStringGreater", typeof(ActionStringGreater))]
        [XmlArrayItem("ActionExtends", typeof(ActionExtends))]
        [XmlArrayItem("ActionGotoFrame", typeof(ActionGotoFrame))]
        [XmlArrayItem("ActionGetURL", typeof(ActionGetURL))]
        [XmlArrayItem("ActionStoreRegister", typeof(ActionStoreRegister))]
        [XmlArrayItem("ActionConstantPool", typeof(ActionConstantPool))]
        [XmlArrayItem("ActionWaitForFrame", typeof(ActionWaitForFrame))]
        [XmlArrayItem("ActionSetTarget", typeof(ActionSetTarget))]
        [XmlArrayItem("ActionGoToLabel", typeof(ActionGoToLabel))]
        [XmlArrayItem("ActionWaitForFrame2", typeof(ActionWaitForFrame2))]
        [XmlArrayItem("ActionDefineFunction2", typeof(ActionDefineFunction2))]
        [XmlArrayItem("ActionTry", typeof(ActionTry))]
        [XmlArrayItem("ActionWith", typeof(ActionWith))]
        [XmlArrayItem("ActionPush", typeof(ActionPush))]
        [XmlArrayItem("ActionJump", typeof(ActionJump))]
        [XmlArrayItem("ActionGetURL2", typeof(ActionGetURL2))]
        [XmlArrayItem("ActionDefineFunction", typeof(ActionDefineFunction))]
        [XmlArrayItem("ActionIf", typeof(ActionIf))]
        [XmlArrayItem("ActionCall", typeof(ActionCall))]
        [XmlArrayItem("ActionGotoFrame2", typeof(ActionGotoFrame2))]
        public List<ActionBase> Body { get; set; } 

        public ActionDefineFunction2()
            : base(ActionType.DefineFunction2)
        { }

        internal override void FromStream(BitReader reader)
        {
            base.FromStream(reader);
            reader.ReadUI16();
            FunctionName = reader.ReadString();
            var numParams = reader.ReadUI16();
            RegisterCount = reader.ReadUI8();
            PreloadParentFlag = reader.ReadBoolBit();
            PreloadRootFlag = reader.ReadBoolBit();
            SuppressSuperFlag = reader.ReadBoolBit();
            PreloadSuperFlag = reader.ReadBoolBit();
            SuppressArgumentsFlag = reader.ReadBoolBit();
            PreloadArgumentsFlag = reader.ReadBoolBit();
            SuppressThisFlag = reader.ReadBoolBit();
            PreloadThisFlag = reader.ReadBoolBit();
            reader.ReadBits(7);
            PreloadGlobalFlag = reader.ReadBoolBit();

            Parameters = new List<RegisterParam>(numParams);
            for (int i = 0; i < numParams; i++)
            {
                Parameters.Add(new RegisterParam(reader.ReadUI8(), reader.ReadString()));
            }
            Body = new List<ActionBase>();
            var codeSize = reader.ReadUI16();
            var codeStartPos = reader.Position;
            while (reader.Position - codeStartPos < codeSize)
            {
                Body.Add(ActionFactory.ReadAction(reader));
            }
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            base.ToStream(writer, swfVersion);
            var ms = GetDataStream(swfVersion);
            writer.WriteUI16((ushort)(ms.Position + 2));
            writer.WriteBytes(ms.GetBuffer(), 0, (int)ms.Position);
            ms = GetCodeStream(swfVersion);
            writer.WriteUI16((ushort)ms.Position);
            writer.WriteBytes(ms.GetBuffer(), 0, (int)ms.Position);
        }

        private MemoryStream GetDataStream(byte swfVersion)
        {
            var result = new MemoryStream();
            using (var writer = new BitWriter(result, true))
            {
                writer.WriteString(FunctionName, swfVersion);
                writer.WriteUI16((ushort)Parameters.Count);

                writer.WriteUI8(RegisterCount);
                writer.WriteBoolBit(PreloadParentFlag);
                writer.WriteBoolBit(PreloadRootFlag);
                writer.WriteBoolBit(SuppressSuperFlag);
                writer.WriteBoolBit(PreloadSuperFlag);
                writer.WriteBoolBit(SuppressArgumentsFlag);
                writer.WriteBoolBit(PreloadArgumentsFlag);
                writer.WriteBoolBit(SuppressThisFlag);
                writer.WriteBoolBit(PreloadThisFlag);
                writer.WriteBits(7, 0);
                writer.WriteBoolBit(PreloadGlobalFlag);

                foreach (var param in Parameters)
                {
                    writer.WriteUI8(param.Register);
                    writer.WriteString(param.ParamName, swfVersion);
                }
            }
            return result;
        }

        private MemoryStream GetCodeStream(byte swfVersion)
        {
            var result = new MemoryStream();
            using (var writer = new BitWriter(result, true))
            {
                foreach (var action in Body)
                {
                    action.ToStream(writer, swfVersion);
                }
            }
            return result;
        }

        [Serializable]
        public class RegisterParam
        {
            [XmlAttribute]
            public byte Register { get; set; }
            [XmlAttribute]
            public string ParamName { get; set; }

            private RegisterParam()
            {}

            public RegisterParam(byte register, string paramName)
            {
                Register = register;
                ParamName = paramName;
            }
        }
    }
}