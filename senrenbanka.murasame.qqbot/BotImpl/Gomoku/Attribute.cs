using System;
using System.Reflection;

namespace senrenbanka.murasame.qqbot.BotImpl.Gomoku
{
    public class LeftStep : Attribute
    {
        public int StepX { get; }
        public int StepY { get; }

        public LeftStep(int x, int y)
        {
            StepX = x;
            StepY = y;
        }
    }

    public class RightStep : Attribute
    {
        public int StepX { get; }
        public int StepY { get; }

        public RightStep(int x, int y)
        {
            StepX = x;
            StepY = y;
        }
    }

    public class Validator
    {
        public static TResult Validate<T, TInvoker, TResult>(string method, Func<T, TResult> mapper) where T : Attribute
        {
            var methodInfo = typeof(TInvoker).GetMethod(method, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.CreateInstance)?.GetCustomAttribute<T>();
            // ReSharper disable once RedundantTypeSpecificationInDefaultExpression
            return methodInfo != null ? mapper(methodInfo) : default(TResult);
        }
    }
}