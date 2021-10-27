/*
 *  File Name:   SensorReadingForm.xaml.cs
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
    using System.Windows;

    /// <summary>
    /// Defines the <see cref="SensorReadingForm" />.
    /// </summary>
    public partial class SensorReadingForm : Window
    {
        /// <summary>
        /// Defines the censorRecord.
        /// </summary>
        private SensorReading sensorRecord;

        /// <summary>
        /// Initializes a new instance of the <see cref="SensorReadingForm"/> class.
        /// </summary>
        /// <param name="sensorReading">The censorRecord<see cref="SensorReading"/>.</param>
        public SensorReadingForm(SensorReading sensorReading)
        {
            InitializeComponent();
            this.sensorRecord = sensorReading;

            // Load the form's fields
            if (sensorReading.DateTime.HasValue)
            {
                dateField.Text = sensorReading.DateTime.Value.Date.ToString();
                dateField.Tag = dateField.Text;
                timeTextBox.Text = sensorReading.DateTime.Value.TimeOfDay.ToString();
                timeTextBox.Tag = timeTextBox.Text;
                measurementTextBox.Text = sensorReading.Measurement;
                measurementTextBox.Tag = measurementTextBox.Text;
                valueTextBox.Text = sensorReading.Value.ToString();
                valueTextBox.Tag = valueTextBox.Text;
            }
        }

        /// <summary>
        /// The SubmitButton_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            bool processFields = false;

            if (sensorRecord.DateTime.HasValue)
            {
                if (!dateField.Tag.Equals(dateField.Text) || !timeTextBox.Tag.Equals(timeTextBox.Text)
                    || !measurementTextBox.Tag.Equals(measurementTextBox.Text)
                    || !valueTextBox.Tag.Equals(valueTextBox.Text))
                {
                    processFields = true;
                }
                else
                {
                    this.DialogResult = false;
                }
            }
            else
            {
                processFields = true;
            }

            if (processFields)
            {
                SensorReading result = ValidateFields();

                if (result != null)
                {
                    sensorRecord.DateTime = result.DateTime;
                    sensorRecord.Measurement = result.Measurement;
                    sensorRecord.Value = result.Value;

                    this.DialogResult = true;
                }
            }

            e.Handled = true;
        }

        /// <summary>
        /// The ValidateFields.
        /// </summary>
        /// <returns>The <see cref="SensorReading?"/>.</returns>
        private SensorReading ValidateFields()
        {
            bool result = true;
            string message = string.Empty;

            SensorReading rtn = new();

            // Process the Date and Time fields
            string datetime = dateField.Text.Trim() + @" " + timeTextBox.Text.Trim();
            DateTime dateTime;

            if (DateTime.TryParse(datetime, out dateTime))
            {
                rtn.DateTime = dateTime;
            }
            else
            {
                message = @"- Invalid date and/or time format(s)";
                result = false;
            }

            // Process the Measurement field
            if (measurementTextBox.Text.Length > 0)
            {
                rtn.Measurement = measurementTextBox.Text;
            }
            else
            {
                message += (message.Length > 0 ? "\n" : @"") + @"- Measurement must be labeled";
                result = false;
            }

            // Process the Value field
            double value;

            if (double.TryParse(valueTextBox.Text, out value))
            {
                rtn.Value = value;
            }
            else
            {
                message += (message.Length > 0 ? "\n" : @"") + @"- Invalid value";
                result = false;
            }

            if (!result)
            {
                MessageBox.Show(message, "Error(s) processing form");
                rtn = null;
            }

            return rtn;
        }
    }
}