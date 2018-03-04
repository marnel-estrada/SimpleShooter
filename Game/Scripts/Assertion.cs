using System;
using System.Collections.Generic;

using Godot;

namespace Common {
    public static class Assertion {

        /// <summary>
        /// Asserts that a certain pointer is not null
        /// </summary>
        /// <param name="pointer"></param>
        /// <param name="message"></param>
        public static void AssertNotNull(object pointer, string message = null) {
            Assert(pointer != null, message);
        }

        /// <summary>
        /// Asserts a certain condition
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="message"></param> 
        public static void Assert(bool condition, string message = null) {
            if(!condition) {
                if(string.IsNullOrEmpty(message)) {
                    GD.Printerr("Assertion failed");
                    throw new Exception("Assertion failed");
                }

                GD.Printerr(message);
                throw new Exception("Assertion failed: " + message);
            }
        }

    }
}
