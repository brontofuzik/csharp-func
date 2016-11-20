namespace ImmutableStack
{
    class Program
    {
        static void Main(string[] args)
        {
            IStack<int> s1 = Stack<int>.Empty;
            IStack<int> s2 = s1.Push(10);
            IStack<int> s3 = s2.Push(20);
            IStack<int> s4 = s2.Push(30); // shares its tail with s3.
        }
    }
}
