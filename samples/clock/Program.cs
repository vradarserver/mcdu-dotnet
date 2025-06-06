﻿// Copyright © 2025 onwards, Andrew Whewell
// All rights reserved.
//
// Redistribution and use of this software in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
//    * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//    * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
//    * Neither the name of the author nor the names of the program's contributors may be used to endorse or promote products derived from this software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE AUTHORS OF THE SOFTWARE BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Threading;
using McduDotNet;

namespace Clock
{
    class Program
    {
        static void Main(string[] _)
        {
            using(var mcdu = McduFactory.ConnectLocal()) {
                Console.WriteLine($"Using {mcdu.ProductId} MCDU");

                Console.WriteLine($"Press Q to quit");
                while(!Console.KeyAvailable || Console.ReadKey(intercept: true).Key != ConsoleKey.Q) {
                    var now = DateTime.Now;

                    mcdu.Output
                        .Clear()
                        .MiddleLine().CentreFor("00:00:00")
                        .White().Write(now.Hour, "00").Yellow().Write(':')
                        .White().Write(now.Minute, "00").Yellow().Write(':')
                        .White().Write(now.Second, "00");
                    mcdu.RefreshDisplay();

                    Thread.Sleep(100);
                }

                mcdu.Cleanup();
            }
        }
    }
}
