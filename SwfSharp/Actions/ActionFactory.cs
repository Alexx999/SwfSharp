using System.Linq;
using System.Text;
using SwfSharp.Utils;

namespace SwfSharp.Actions
{
    internal static class ActionFactory
    {
        public static ActionBase ReadAction(BitReader reader)
        {
            var actionCode = (ActionType)reader.ReadUI8();
            var result = GetAction(actionCode);
            result.FromStream(reader);
            return result;
        }

        private static ActionBase GetAction(ActionType actionCode)
        {
            switch (actionCode)
            {
                case ActionType.NextFrame:
                {
                    return new ActionNextFrame();
                }
                case ActionType.PreviousFrame:
                {
                    return new ActionPreviousFrame();
                }
                case ActionType.Play:
                {
                    return new ActionPlay();
                }
                case ActionType.Stop:
                {
                    return new ActionStop();
                }
                case ActionType.ToggleQuality:
                {
                    return new ActionToggleQuality();
                }
                case ActionType.StopSounds:
                {
                    return new ActionStopSounds();
                }
                case ActionType.Add:
                {
                    return new ActionAdd();
                }
                case ActionType.Subtract:
                {
                    return new ActionSubtract();
                }
                case ActionType.Multiply:
                {
                    return new ActionMultiply();
                }
                case ActionType.Divide:
                {
                    return new ActionDivide();
                }
                case ActionType.Equals:
                {
                    return new ActionEquals();
                }
                case ActionType.Less:
                {
                    return new ActionLess();
                }
                case ActionType.And:
                {
                    return new ActionAnd();
                }
                case ActionType.Or:
                {
                    return new ActionOr();
                }
                case ActionType.Not:
                {
                    return new ActionNot();
                }
                case ActionType.StringEquals:
                {
                    return new ActionStringEquals();
                }
                case ActionType.StringLength:
                {
                    return new ActionStringLength();
                }
                case ActionType.StringExtract:
                {
                    return new ActionStringExtract();
                }
                case ActionType.Pop:
                {
                    return new ActionPop();
                }
                case ActionType.ToInteger:
                {
                    return new ActionToInteger();
                }
                case ActionType.GetVariable:
                {
                    return new ActionGetVariable();
                }
                case ActionType.SetVariable:
                {
                    return new ActionSetVariable();
                }
                case ActionType.SetTarget2:
                {
                    return new ActionSetTarget2();
                }
                case ActionType.StringAdd:
                {
                    return new ActionStringAdd();
                }
                case ActionType.GetProperty:
                {
                    return new ActionGetProperty();
                }
                case ActionType.SetProperty:
                {
                    return new ActionSetProperty();
                }
                case ActionType.CloneSprite:
                {
                    return new ActionCloneSprite();
                }
                case ActionType.RemoveSprite:
                {
                    return new ActionRemoveSprite();
                }
                case ActionType.Trace:
                {
                    return new ActionTrace();
                }
                case ActionType.StartDrag:
                {
                    return new ActionStartDrag();
                }
                case ActionType.EndDrag:
                {
                    return new ActionEndDrag();
                }
                case ActionType.StringLess:
                {
                    return new ActionStringLess();
                }
                case ActionType.Throw:
                {
                    return new ActionThrow();
                }
                case ActionType.CastOp:
                {
                    return new ActionCastOp();
                }
                case ActionType.ImplementsOp:
                {
                    return new ActionImplementsOp();
                }
                case ActionType.RandomNumber:
                {
                    return new ActionRandomNumber();
                }
                case ActionType.MBStringLength:
                {
                    return new ActionMBStringLength();
                }
                case ActionType.CharToAscii:
                {
                    return new ActionCharToAscii();
                }
                case ActionType.AsciiToChar:
                {
                    return new ActionAsciiToChar();
                }
                case ActionType.GetTime:
                {
                    return new ActionGetTime();
                }
                case ActionType.MBStringExtract:
                {
                    return new ActionMBStringExtract();
                }
                case ActionType.MBCharToAscii:
                {
                    return new ActionMBCharToAscii();
                }
                case ActionType.MBAsciiToChar:
                {
                    return new ActionMBAsciiToChar();
                }
                case ActionType.Delete:
                {
                    return new ActionDelete();
                }
                case ActionType.Delete2:
                {
                    return new ActionDelete2();
                }
                case ActionType.DefineLocal:
                {
                    return new ActionDefineLocal();
                }
                case ActionType.CallFunction:
                {
                    return new ActionCallFunction();
                }
                case ActionType.Return:
                {
                    return new ActionReturn();
                }
                case ActionType.Modulo:
                {
                    return new ActionModulo();
                }
                case ActionType.NewObject:
                {
                    return new ActionNewObject();
                }
                case ActionType.DefineLocal2:
                {
                    return new ActionDefineLocal2();
                }
                case ActionType.InitArray:
                {
                    return new ActionInitArray();
                }
                case ActionType.InitObject:
                {
                    return new ActionInitObject();
                }
                case ActionType.TypeOf:
                {
                    return new ActionTypeOf();
                }
                case ActionType.TargetPath:
                {
                    return new ActionTargetPath();
                }
                case ActionType.Enumerate:
                {
                    return new ActionEnumerate();
                }
                case ActionType.Add2:
                {
                    return new ActionAdd2();
                }
                case ActionType.Less2:
                {
                    return new ActionLess2();
                }
                case ActionType.Equals2:
                {
                    return new ActionEquals2();
                }
                case ActionType.ToNumber:
                {
                    return new ActionToNumber();
                }
                case ActionType.ToString:
                {
                    return new ActionToString();
                }
                case ActionType.PushDuplicate:
                {
                    return new ActionPushDuplicate();
                }
                case ActionType.StackSwap:
                {
                    return new ActionStackSwap();
                }
                case ActionType.GetMember:
                {
                    return new ActionGetMember();
                }
                case ActionType.SetMember:
                {
                    return new ActionSetMember();
                }
                case ActionType.Increment:
                {
                    return new ActionIncrement();
                }
                case ActionType.Decrement:
                {
                    return new ActionDecrement();
                }
                case ActionType.CallMethod:
                {
                    return new ActionCallMethod();
                }
                case ActionType.NewMethod:
                {
                    return new ActionNewMethod();
                }
                case ActionType.InstanceOf:
                {
                    return new ActionInstanceOf();
                }
                case ActionType.Enumerate2:
                {
                    return new ActionEnumerate2();
                }
                case ActionType.BitAnd:
                {
                    return new ActionBitAnd();
                }
                case ActionType.BitOr:
                {
                    return new ActionBitOr();
                }
                case ActionType.BitXor:
                {
                    return new ActionBitXor();
                }
                case ActionType.BitLShift:
                {
                    return new ActionBitLShift();
                }
                case ActionType.BitRShift:
                {
                    return new ActionBitRShift();
                }
                case ActionType.BitURShift:
                {
                    return new ActionBitURShift();
                }
                case ActionType.StrictEquals:
                {
                    return new ActionStrictEquals();
                }
                case ActionType.Greater:
                {
                    return new ActionGreater();
                }
                case ActionType.StringGreater:
                {
                    return new ActionStringGreater();
                }
                case ActionType.Extends:
                {
                    return new ActionExtends();
                }
                case ActionType.GotoFrame:
                {
                    return new ActionGotoFrame();
                }
                case ActionType.GetURL:
                {
                    return new ActionGetURL();
                }
                case ActionType.StoreRegister:
                {
                    return new ActionStoreRegister();
                }
                case ActionType.ConstantPool:
                {
                    return new ActionConstantPool();
                }
                case ActionType.WaitForFrame:
                {
                    return new ActionWaitForFrame();
                }
                case ActionType.SetTarget:
                {
                    return new ActionSetTarget();
                }
                case ActionType.GoToLabel:
                {
                    return new ActionGoToLabel();
                }
                case ActionType.WaitForFrame2:
                {
                    return new ActionWaitForFrame2();
                }
                case ActionType.DefineFunction2:
                {
                    return new ActionDefineFunction2();
                }
                case ActionType.Try:
                {
                    return new ActionTry();
                }
                case ActionType.With:
                {
                    return new ActionWith();
                }
                case ActionType.Push:
                {
                    return new ActionPush();
                }
                case ActionType.Jump:
                {
                    return new ActionJump();
                }
                case ActionType.GetURL2:
                {
                    return new ActionGetURL2();
                }
                case ActionType.DefineFunction:
                {
                    return new ActionDefineFunction();
                }
                case ActionType.If:
                {
                    return new ActionIf();
                }
                case ActionType.Call:
                {
                    return new ActionCall();
                }
                case ActionType.GotoFrame2:
                {
                    return new ActionGotoFrame2();
                }
                default:
                {
                    return new ActionUnknown(actionCode);
                }
            }
        }
    }
}
