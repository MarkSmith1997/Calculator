using System;
using System.Collections.Generic;
using System.Linq;


namespace Calculator1
{
    class Program
    {
        private string[] operators;
        Program()
        {
            operators = new string[]{ "+", "-", "*", "/", "^" };        
        }
        static void Main()
        {
            Program instance = new Program();
            while (true)
            {
                Console.WriteLine("Enter expression:");
                string input = Console.ReadLine();
                Queue<string> outputQueue = instance.ConvertNotation(input);
                Console.WriteLine(instance.Calculate(outputQueue));
            }
        }
        double Calculate(Queue<string> outputQueue)
        {
            Stack<double> valueStack = new Stack<double>();
            while (outputQueue.Count > 0)
            {
                if (!operators.Contains(outputQueue.Peek()))
                {
                    valueStack.Push(double.Parse(outputQueue.Dequeue()));
                }
                else
                {
                    string currentOperator = outputQueue.Dequeue();
                    double leftNumber;
                    double rightNumber;
                    switch (currentOperator)
                    {
                        case "+":
                            valueStack.Push(valueStack.Pop() + valueStack.Pop());
                            break;
                        case "-":
                            rightNumber = valueStack.Pop();
                            leftNumber = valueStack.Pop();
                            valueStack.Push(leftNumber - rightNumber);
                            break;
                        case "/":
                            rightNumber = valueStack.Pop();
                            leftNumber = valueStack.Pop();
                            valueStack.Push(leftNumber / rightNumber);
                            break;
                        case "*":
                            valueStack.Push(valueStack.Pop() * valueStack.Pop());
                            break;
                        case "^":
                            rightNumber = valueStack.Pop();
                            leftNumber = valueStack.Pop();
                            valueStack.Push(Math.Pow(leftNumber, rightNumber));
                            break;
                    }
                }
            }
            return valueStack.Pop();
        }
        Queue<string> ConvertNotation(string input)
        {
            Dictionary<string, int> precedence = new Dictionary<string, int>() { { "+", 2 }, { "-", 2 }, { "*", 3 }, { "/", 3 }, { "^", 4 }, { "(", 0 }, { ")", 5 } };
            Queue<string> outputQueue = new Queue<string>();
            Stack<string> operatorStack = new Stack<string>();
            string[] inputQueue = input.Split();
            bool parenthesisFound;
            foreach (string s in inputQueue)
            {
                if (s == "(")
                {
                    operatorStack.Push(s);
                }
                else if (s == ")")
                {
                    parenthesisFound = true;
                    while (parenthesisFound)
                    {
                        if (operatorStack.Count == 0)
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
                if (operatorStack.Peek() == "(")
                {
                    Console.WriteLine("Error: Parenthesis mismatch, revise input.");
                    break;
                }
                outputQueue.Enqueue(operatorStack.Pop());
            }
            return outputQueue;
        }
    }
}
