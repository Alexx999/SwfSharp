using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using SwfSharp.Actions;
using SwfSharp.Structs;
using SwfSharp.Utils;

namespace SwfSharp.Tags
{
    [Serializable]
    public class DoActionTag : SwfTag
    {
        [XmlElement("ActionUnknown", typeof(ActionUnknown))]
        [XmlElement("ActionNextFrame", typeof(ActionNextFrame))]
        [XmlElement("ActionPreviousFrame", typeof(ActionPreviousFrame))]
        [XmlElement("ActionPlay", typeof(ActionPlay))]
        [XmlElement("ActionStop", typeof(ActionStop))]
        [XmlElement("ActionToggleQuality", typeof(ActionToggleQuality))]
        [XmlElement("ActionStopSounds", typeof(ActionStopSounds))]
        [XmlElement("ActionAdd", typeof(ActionAdd))]
        [XmlElement("ActionSubtract", typeof(ActionSubtract))]
        [XmlElement("ActionMultiply", typeof(ActionMultiply))]
        [XmlElement("ActionDivide", typeof(ActionDivide))]
        [XmlElement("ActionEquals", typeof(ActionEquals))]
        [XmlElement("ActionLess", typeof(ActionLess))]
        [XmlElement("ActionAnd", typeof(ActionAnd))]
        [XmlElement("ActionOr", typeof(ActionOr))]
        [XmlElement("ActionNot", typeof(ActionNot))]
        [XmlElement("ActionStringEquals", typeof(ActionStringEquals))]
        [XmlElement("ActionStringLength", typeof(ActionStringLength))]
        [XmlElement("ActionStringExtract", typeof(ActionStringExtract))]
        [XmlElement("ActionPop", typeof(ActionPop))]
        [XmlElement("ActionToInteger", typeof(ActionToInteger))]
        [XmlElement("ActionGetVariable", typeof(ActionGetVariable))]
        [XmlElement("ActionSetVariable", typeof(ActionSetVariable))]
        [XmlElement("ActionSetTarget2", typeof(ActionSetTarget2))]
        [XmlElement("ActionStringAdd", typeof(ActionStringAdd))]
        [XmlElement("ActionGetProperty", typeof(ActionGetProperty))]
        [XmlElement("ActionSetProperty", typeof(ActionSetProperty))]
        [XmlElement("ActionCloneSprite", typeof(ActionCloneSprite))]
        [XmlElement("ActionRemoveSprite", typeof(ActionRemoveSprite))]
        [XmlElement("ActionTrace", typeof(ActionTrace))]
        [XmlElement("ActionStartDrag", typeof(ActionStartDrag))]
        [XmlElement("ActionEndDrag", typeof(ActionEndDrag))]
        [XmlElement("ActionStringLess", typeof(ActionStringLess))]
        [XmlElement("ActionThrow", typeof(ActionThrow))]
        [XmlElement("ActionCastOp", typeof(ActionCastOp))]
        [XmlElement("ActionImplementsOp", typeof(ActionImplementsOp))]
        [XmlElement("ActionRandomNumber", typeof(ActionRandomNumber))]
        [XmlElement("ActionMBStringLength", typeof(ActionMBStringLength))]
        [XmlElement("ActionCharToAscii", typeof(ActionCharToAscii))]
        [XmlElement("ActionAsciiToChar", typeof(ActionAsciiToChar))]
        [XmlElement("ActionGetTime", typeof(ActionGetTime))]
        [XmlElement("ActionMBStringExtract", typeof(ActionMBStringExtract))]
        [XmlElement("ActionMBCharToAscii", typeof(ActionMBCharToAscii))]
        [XmlElement("ActionMBAsciiToChar", typeof(ActionMBAsciiToChar))]
        [XmlElement("ActionDelete", typeof(ActionDelete))]
        [XmlElement("ActionDelete2", typeof(ActionDelete2))]
        [XmlElement("ActionDefineLocal", typeof(ActionDefineLocal))]
        [XmlElement("ActionCallFunction", typeof(ActionCallFunction))]
        [XmlElement("ActionReturn", typeof(ActionReturn))]
        [XmlElement("ActionModulo", typeof(ActionModulo))]
        [XmlElement("ActionNewObject", typeof(ActionNewObject))]
        [XmlElement("ActionDefineLocal2", typeof(ActionDefineLocal2))]
        [XmlElement("ActionInitArray", typeof(ActionInitArray))]
        [XmlElement("ActionInitObject", typeof(ActionInitObject))]
        [XmlElement("ActionTypeOf", typeof(ActionTypeOf))]
        [XmlElement("ActionTargetPath", typeof(ActionTargetPath))]
        [XmlElement("ActionEnumerate", typeof(ActionEnumerate))]
        [XmlElement("ActionAdd2", typeof(ActionAdd2))]
        [XmlElement("ActionLess2", typeof(ActionLess2))]
        [XmlElement("ActionEquals2", typeof(ActionEquals2))]
        [XmlElement("ActionToNumber", typeof(ActionToNumber))]
        [XmlElement("ActionToString", typeof(ActionToString))]
        [XmlElement("ActionPushDuplicate", typeof(ActionPushDuplicate))]
        [XmlElement("ActionStackSwap", typeof(ActionStackSwap))]
        [XmlElement("ActionGetMember", typeof(ActionGetMember))]
        [XmlElement("ActionSetMember", typeof(ActionSetMember))]
        [XmlElement("ActionIncrement", typeof(ActionIncrement))]
        [XmlElement("ActionDecrement", typeof(ActionDecrement))]
        [XmlElement("ActionCallMethod", typeof(ActionCallMethod))]
        [XmlElement("ActionNewMethod", typeof(ActionNewMethod))]
        [XmlElement("ActionInstanceOf", typeof(ActionInstanceOf))]
        [XmlElement("ActionEnumerate2", typeof(ActionEnumerate2))]
        [XmlElement("ActionBitAnd", typeof(ActionBitAnd))]
        [XmlElement("ActionBitOr", typeof(ActionBitOr))]
        [XmlElement("ActionBitXor", typeof(ActionBitXor))]
        [XmlElement("ActionBitLShift", typeof(ActionBitLShift))]
        [XmlElement("ActionBitRShift", typeof(ActionBitRShift))]
        [XmlElement("ActionBitURShift", typeof(ActionBitURShift))]
        [XmlElement("ActionStrictEquals", typeof(ActionStrictEquals))]
        [XmlElement("ActionGreater", typeof(ActionGreater))]
        [XmlElement("ActionStringGreater", typeof(ActionStringGreater))]
        [XmlElement("ActionExtends", typeof(ActionExtends))]
        [XmlElement("ActionGotoFrame", typeof(ActionGotoFrame))]
        [XmlElement("ActionGetURL", typeof(ActionGetURL))]
        [XmlElement("ActionStoreRegister", typeof(ActionStoreRegister))]
        [XmlElement("ActionConstantPool", typeof(ActionConstantPool))]
        [XmlElement("ActionWaitForFrame", typeof(ActionWaitForFrame))]
        [XmlElement("ActionSetTarget", typeof(ActionSetTarget))]
        [XmlElement("ActionGoToLabel", typeof(ActionGoToLabel))]
        [XmlElement("ActionWaitForFrame2", typeof(ActionWaitForFrame2))]
        [XmlElement("ActionDefineFunction2", typeof(ActionDefineFunction2))]
        [XmlElement("ActionTry", typeof(ActionTry))]
        [XmlElement("ActionWith", typeof(ActionWith))]
        [XmlElement("ActionPush", typeof(ActionPush))]
        [XmlElement("ActionJump", typeof(ActionJump))]
        [XmlElement("ActionGetURL2", typeof(ActionGetURL2))]
        [XmlElement("ActionDefineFunction", typeof(ActionDefineFunction))]
        [XmlElement("ActionIf", typeof(ActionIf))]
        [XmlElement("ActionCall", typeof(ActionCall))]
        [XmlElement("ActionGotoFrame2", typeof(ActionGotoFrame2))]
        public List<ActionBase> Actions { get; set; }

        public DoActionTag() : this(0)
        {
        }

        public DoActionTag(int size)
            : base(TagType.DoAction, size)
        {
        }

        protected DoActionTag(TagType tagType, int size)
            : base(tagType, size)
        {
        }

        internal override void FromStream(BitReader reader, byte swfVersion)
        {
            Actions = new List<ActionBase>();
            var nextFlag = reader.ReadUI8();
            while (nextFlag != 0)
            {
                reader.Seek(-1, SeekOrigin.Current);
                Actions.Add(ActionFactory.ReadAction(reader));
                nextFlag = reader.ReadUI8();
            }
        }

        internal override void ToStream(BitWriter writer, byte swfVersion)
        {
            foreach (var action in Actions)
            {
                action.ToStream(writer, swfVersion);
            }
            writer.WriteUI8(0);
        }
    }
}
