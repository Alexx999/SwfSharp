using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using SwfSharp.Utils;

namespace SwfSharp.Actions
{
    [Serializable]
    public class ActionTry : ActionBase
    {
        [XmlAttribute]
        public string CatchName { get; set; }
        [XmlAttribute]
        public byte CatchRegister { get; set; }

        [XmlIgnore]
        public bool CatchRegisterSpecified
        {
            get { return string.IsNullOrEmpty(CatchName); }
        }

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
        public List<ActionBase> TryBody { get; set; }
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
        public List<ActionBase> CatchBody { get; set; }
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
        public List<ActionBase> FinallyBody { get; set; } 

        public ActionTry()
            : base(ActionType.Try)
        { }

        internal override void FromStream(BitReader reader)
        {
            base.FromStream(reader);
            reader.ReadUI16();
            reader.ReadBits(5);
            var catchInRegisterFlag = reader.ReadBoolBit();
            var finallyBlockFlag = reader.ReadBoolBit();
            var catchBlockFlag = reader.ReadBoolBit();

            var trySize = reader.ReadUI16();
            var catchSize = reader.ReadUI16();
            var finallySize = reader.ReadUI16();

            if (catchInRegisterFlag)
            {
                CatchRegister = reader.ReadUI8();
            }
            else
            {
                CatchName = reader.ReadString();
            }

            TryBody = new List<ActionBase>();
            var tryStartPos = reader.Position;
            while (reader.Position - tryStartPos < trySize)
            {
                TryBody.Add(ActionFactory.ReadAction(reader));
            }

            CatchBody = new List<ActionBase>();
            var catchStartPos = reader.Position;
            while (reader.Position - catchStartPos < catchSize)
            {
                CatchBody.Add(ActionFactory.ReadAction(reader));
            }

            FinallyBody = new List<ActionBase>();
            var finallyStartPos = reader.Position;
            while (reader.Position - finallyStartPos < finallySize)
            {
                FinallyBody.Add(ActionFactory.ReadAction(reader));
            }
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            base.ToStream(writer, swfVersion);
            var ms = GetDataStream(swfVersion);
            writer.WriteUI16((ushort)ms.Position);
            writer.WriteBytes(ms.GetBuffer(), 0, (int)ms.Position);
        }

        private MemoryStream GetDataStream(byte swfVersion)
        {
            var result = new MemoryStream();
            using (var writer = new BitWriter(result, true))
            {
                var catchInRegisterFlag = !string.IsNullOrEmpty(CatchName);
                var finallyBlockFlag = FinallyBody != null && FinallyBody.Count > 0;
                var catchBlockFlag = CatchBody != null && CatchBody.Count > 0;

                var tryMs = GetCodeStream(TryBody, swfVersion);
                MemoryStream catchMs = null;
                if (catchBlockFlag)
                {
                    catchMs = GetCodeStream(CatchBody, swfVersion);
                }
                MemoryStream finallyMs = null;
                if (finallyBlockFlag)
                {
                    finallyMs = GetCodeStream(FinallyBody, swfVersion);
                }

                writer.WriteUI16((ushort) tryMs.Position);
                writer.WriteUI16((ushort) (catchMs != null ?catchMs.Position:0));
                writer.WriteUI16((ushort)(finallyMs != null ? finallyMs.Position : 0));

                if (catchInRegisterFlag)
                {
                    writer.WriteUI8(CatchRegister);
                }
                else
                {
                    writer.WriteString(CatchName, swfVersion);
                }


                writer.WriteBytes(tryMs.GetBuffer(), 0, (int)tryMs.Position);
                if (catchMs != null)
                {
                    writer.WriteBytes(catchMs.GetBuffer(), 0, (int)catchMs.Position);
                }
                if (finallyMs != null)
                {
                    writer.WriteBytes(finallyMs.GetBuffer(), 0, (int)finallyMs.Position);
                }
            }
            return result;
        }

        private MemoryStream GetCodeStream(List<ActionBase> actions, byte swfVersion)
        {
            var result = new MemoryStream();
            using (var writer = new BitWriter(result, true))
            {
                foreach (var action in actions)
                {
                    action.ToStream(writer, swfVersion);
                }
            }
            return result;
        }
    }
}