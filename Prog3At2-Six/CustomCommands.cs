/*
 *  File Name:   CustomCommands.cs
 *
 *  Project:     Prog3At2-Six
 *
 *  Copyright (c) 2021 Bradley Willcott
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 *
 * ****************************************************************
 * Name: Bradley Willcott
 * ID:   M198449
 * Date: 22/10/2021
 * ****************************************************************
 */

namespace Prog3At2_Six
{
    using System.Windows.Input;

    /// <summary>
    /// Defines the <see cref="CustomCommands" />.
    /// </summary>
    public static class CustomCommands
    {
        /// <summary>
        /// Defines the _AboutCommand.
        /// </summary>
        private static RoutedUICommand _AboutCommand;

        /// <summary>
        /// Defines the _AddCommand.
        /// </summary>
        private static RoutedUICommand _AddCommand;

        /// <summary>
        /// Gets the About command.
        /// </summary>
        public static RoutedUICommand About
        {
            get
            {
                if (_AboutCommand == null)
                {
                    _AboutCommand = new RoutedUICommand(@"_About", nameof(About), typeof(CustomCommands));
                }

                return _AboutCommand;
            }
        }

        /// <summary>
        /// Gets the Add command.
        /// </summary>
        public static RoutedUICommand Add
        {
            get
            {
                if (_AddCommand == null)
                {
                    InputGestureCollection input = new();
                    input.Add(new KeyGesture(Key.A, ModifierKeys.Control, "Ctrl+A"));

                    _AddCommand = new RoutedUICommand(@"_Add", nameof(Add), typeof(CustomCommands), input);
                }

                return _AddCommand;
            }
        }
    }
}