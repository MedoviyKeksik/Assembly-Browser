using System;

namespace SampleAssemblies
{
    public class ClassA
    {
        class NestedClassC
        {
            void MethodC()
            {
                Console.WriteLine("C");
            }
        }
        
        void MethodA()
        {
            Console.WriteLine("A");
        }
    }
    
    public class ClassB
    {
        public int TestInt { get; set; }
    }
}