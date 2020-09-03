using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Calculator1
{
    class Program
    {
        static void Main()
        {
            string[] operators = { "+", "-", "*", "/", "^"};
            Dictionary<string, int> precedence = new Dictionary<string, int>(){ {"+", 2}, {"-", 2}, {"*", 3}, {"/", 3}, {"^", 4} };
            Queue<string> outputQueue = new Queue<string>();
            Stack<string> operatorStack = new Stack<string>();
            string input = Console.ReadLine();
            string[] inputQueue = input.Split();

            foreach(string s in inputQueue)
            {
                if (operators.Contains(s))
                {
                    while(operatorStack.Count > 0 && precedence[operatorStack.Peek()] > precedence[s])
                    {
                        outputQueue.Enqueue(operatorStack.Pop());
                    }
                    operatorStack.Push(s);
                }
                else
                {
                    outputQueue.Enqueue(s);
                }
            }
            while (operatorStack.Count > 0)
            {
                outputQueue.Enqueue(operatorStack.Pop());
            }

            string printString = "";
            while (outputQueue.Count > 0)
            {
                printString += outputQueue.Dequeue() + " ";
            }
            Console.WriteLine(printString);

        }
    }
}
