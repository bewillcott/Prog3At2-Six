/*
 *  File Name:   CensorRecord.cs
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
 * Date: 23/10/2021
 * ****************************************************************
 */

namespace Prog3At2_Six
{
    using System;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="CensorRecord" />.
    /// </summary>
    public class CensorRecord
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CensorRecord"/> class.
        /// </summary>
        /// <param name="dateTime">The dateTime<see cref="DateTime"/>.</param>
        /// <param name="measurement">The measurement<see cref="string"/>.</param>
        /// <param name="value">The value<see cref="double"/>.</param>
        public CensorRecord(DateTime dateTime, string measurement, double value)
        {
            this.DateTime = dateTime;
            this.Measurement = measurement;
            this.Value = value;
        }

        /// <summary>
        /// Gets the DateTime.
        /// </summary>
        public DateTime DateTime { get; private set; }

        /// <summary>
        /// Gets the Measurement.
        /// </summary>
        public string Measurement { get; private set; }

        /// <summary>
        /// Gets the Value.
        /// </summary>
        public double Value { get; private set; }

        override
        public string ToString()
        {
            StringBuilder sb = new();

            sb.Append(DateTime).Append(", ")
                .Append(Measurement).Append(", ")
                .Append(Value);

            return sb.ToString();
        }
    }
}