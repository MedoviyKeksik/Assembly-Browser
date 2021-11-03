using System;

namespace SampleAssemblies
{
    public static class SampleExtensions
    {
        static void ExtensionMethodA(this ClassB classB)
        {
            Console.WriteLine("Extension A");
        }
        
        static void ExtensionMethodB(this ClassB classB)
        {
            Console.WriteLine("Extension B");
        }

        static void SystemStringExt(this string str)
        {
            Console.WriteLine(str);
        }
    }
}