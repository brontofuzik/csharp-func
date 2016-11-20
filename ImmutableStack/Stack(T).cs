using System;
using System.Collections.Generic;
using System.Collections;
namespace ImmutableStack
{
    public sealed class Stack<T> : IStack<T>
    {
        #region Fields

        private static readonly EmptyStack empty = new EmptyStack();

        private readonly T head;
        
        private readonly IStack<T> tail;
        
        #endregion // Fields

        #region Constructors

        private Stack(T head, IStack<T> tail)
        {
            this.head = head;
            this.tail = tail;
        }

        #endregion // Constructors

        #region Properties

        public static IStack<T> Empty
        {
            get
            {
                return empty;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return false;
            }
        }

        #endregion // Properties

        #region Methods

        public IStack<T> Push(T value)
        {
            return new Stack<T>(value, this);
        }

        public IStack<T> Pop()
        {
            return tail;
        }

        public T Peek()
        {
            return head;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (IStack<T> stack = this; !stack.IsEmpty; stack = stack.Pop())
            {
                yield return stack.Peek();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion // Methods

        #region Classes

        private sealed class EmptyStack : IStack<T>
        {
            public IStack<T> Push(T value)
            {
                return new Stack<T>(value, this);
            }

            public IStack<T> Pop()
            {
                throw new Exception("Empty stack");
            }

            public T Peek()
            {
                throw new Exception("Empty stack");
            }

            public bool IsEmpty
            {
                get
                {
                    return true;
                }
            }

            public IEnumerator<T> GetEnumerator()
            {
                yield break;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }

        #endregion // Classes
    }
}
