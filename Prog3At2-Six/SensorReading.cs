/*
 *  File Name:   SensorReading.cs
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
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="SensorReading" />.
    /// </summary>
    public class SensorReading : INotifyPropertyChanged
    {
        /// <summary>
        /// Defines the dateTime.
        /// </summary>
        private DateTime? dateTime;

        private string measurement;
        private double value;

        /// <summary>
        /// Initializes a new instance of the <see cref="SensorReading"/> class.
        /// </summary>
        public SensorReading()
        {
            DateTime = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SensorReading"/> class.
        /// </summary>
        /// <param name="dateTime">The dateTime<see cref="DateTime"/>.</param>
        /// <param name="measurement">The measurement<see cref="string"/>.</param>
        /// <param name="value">The value<see cref="double"/>.</param>
        public SensorReading(DateTime dateTime, string measurement, double value)
        {
            this.DateTime = dateTime;
            this.Measurement = measurement;
            this.Value = value;
        }

        /// <summary>
        /// Defines the PropertyChanged.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the DateTime.
        /// </summary>
        public DateTime? DateTime
        {
            get => dateTime;
            set
            {
                if (dateTime != value)
                {
                    dateTime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the Measurement
        /// </summary>
        public string Measurement
        {
            get => measurement;

            set
            {
                if (measurement != value)
                {
                    measurement = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the Value
        /// </summary>
        public double Value
        {
            get => value;
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// The ToString.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        override
        public string ToString()
        {
            StringBuilder sb = new();

            sb.Append(DateTime).Append("\t")
                .Append(Measurement).Append("\t")
                .Append(Value);

            return sb.ToString();
        }

        /// <summary>
        /// The NotifyPropertyChanged.
        /// </summary>
        /// <param name="propertyName">The propertyName<see cref="String"/>.</param>
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}