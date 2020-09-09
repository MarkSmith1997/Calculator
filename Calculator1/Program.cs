using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace Calculator1
{
    class Program
    {
        static void Main()
        {
            string[] operators = { "+", "-", "*", "/", "^"};
            Dictionary<string, int> precedence = new Dictionary<string, int>(){ {"+", 2}, {"-", 2}, {"*", 3}, {"/", 3}, {"^", 4}, {"(", 0}, {")", 5} };
            Queue<string> outputQueue = new Queue<string>();
            Stack<string> operatorStack = new Stack<string>();
            Console.WriteLine("Enter expression:");
            string input = Console.ReadLine();
            string[] inputQueue = input.Split();
            bool parenthesisFound;

            foreach(string s in inputQueue)
            {
                if(s == "(")
                {
                    operatorStack.Push(s);
                }
                else if (s == ")")
                {
                    parenthesisFound = true;
                    while (parenthesisFound)
                    {
                        if(operatorStack.Count == 0)
                        {
                            parenthesisFound = false;
                            Console.WriteLine("Error: Parenthesis mismatch, revise input.");
                            break;
                        }
                        if (operatorStack.Peek() == "(")
                        {
                            operatorStack.Pop();
                            parenthesisFound = false;
                        }
                        else
                        {
                            outputQueue.Enqueue(operatorStack.Pop());
                        }
                    }
                }
                else if (operators.Contains(s))
                {
                    while (operatorStack.Count > 0 && precedence[operatorStack.Peek()] > precedence[s])
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
                if(operatorStack.Peek() == "(")
                {
                    Console.WriteLine("Error: Parenthesis mismatch, revise input.");
                    break;
                }                   
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
