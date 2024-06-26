﻿using Interpritator;
using Interpritator.output;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Анализатор_лексем;
using Анализатор_лексем.Exceptions;

namespace GoidScriptTests
{
    public class InterpeterTests
    {
        [SetUp]
        public void Setup()
        {
            output = new OutputMock("");
            interpreter = new Interpreter(output);
        }

        private Interpreter interpreter;
        private OutputMock output;

        [Test]
        public void TestCorrectAriphmetic()
        {
            string input = "int a = ((3 + 2) * 2 + 12 * 2) / 2; print(a) ";

            interpreter.Interpret(input);

            Assert.That(output.Output, Is.EqualTo("17\r\n"));
        }

        [Test]
        public void TestCorrectWhile()
        {
            string input = "int a = 5; while(a > 0){print(a) a = a - 1}";

            interpreter.Interpret(input);

            Assert.That(output.Output, Is.EqualTo("5\r\n4\r\n3\r\n2\r\n1\r\n"));
        }

        [Test]
        public void TestCorrectIfAndWrite()
        {
            string input = "print('угадай число') int a = input(); if (a > 5) {print('угадал') } else { print('не угадал') }";
            output.Input = "2";
            interpreter.Interpret(input);

            Assert.That(output.Output, Is.EqualTo("угадай число\r\nне угадал\r\n"));
        }

        [Test]
        public void SortArrayCorrect()
        {// int n = input(); int i = 0; int temp = 0; int j = 0; int arr[n] = {1, 4, 3, 2, 5, 25, 7, 76, 34, 345, 345, 3452, 2342, 235, 23511 }; while (i < n - 1) { j = 0 temp = 0  while (j < n - i - 1) { if (arr[j] > arr[j + 1]) { temp = arr[j] arr[j] = arr[j + 1] arr[j + 1] = temp } else { } j = j + 1 } i = i + 1 } i = 0 while ( i < n ) { print(arr[i]) print(' ') i = i + 1 }
            string input = "int n = input(); int i = 0; int temp = 0; int j = 0; int arr[n] = {1, 4, 3, 2, 5 }; while (i < n - 1) { j = 0 temp = 0  while (j < n - i - 1) { if (arr[j] > arr[j + 1]) { temp = arr[j] arr[j] = arr[j + 1] arr[j + 1] = temp } else { } j = j + 1 } i = i + 1 } i = 0 while ( i < n ) { print(arr[i]) i = i + 1 } ";
            output.Input = "5";
            interpreter.Interpret(input);

            Assert.That(output.Output, Is.EqualTo("1\r\n2\r\n3\r\n4\r\n5\r\n"));
        }

        [Test]
        public void GoalTest()
        {
            string input = "int n = input();  print('г')  print('о' * n) print('л')  ";
            output.Input = "5";
            interpreter.Interpret(input);

            Assert.That(output.Output, Is.EqualTo("г\r\nооооо\r\nл\r\n"));
        }

        [Test]
        public void ErrorsTest()
        {
            string input = "dcas! ERd223 4=23 f-dw23 42341243cas";
            interpreter.Interpret(input);
            Assert.Throws<LLException>(() => interpreter.Interpret(input));
        }
    }
}
