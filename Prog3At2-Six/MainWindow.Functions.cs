/*
 *  File Name:   MainWindow.Functions.cs
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
 * Date: 25/10/2021
 * ****************************************************************
 */

namespace Prog3At2_Six
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Windows;

    using CsvHelper;

    /// <summary>
    /// Defines the <see cref="MainWindow" />.
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// Set the Status line text.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public void SetStatusText(string message)
        {
            statusTextBlock.Text = message != null ? message : string.Empty;
        }

        /// <summary>
        /// LoadData for development/testing of UI.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool LoadData()
        {
            // Prepare data
            SensorData = new();

            // Test data
            SensorData.Add(new(new(2021, 1, 12, 9, 0, 0), "Temperature", 25.0));
            SensorData.Add(new(new(2021, 5, 13), "Temperature", 27.3));
            SensorData.Add(new(new(2021, 3, 14), "Temperature", 22.4));
            SensorData.Add(new(new(2021, 10, 15), "Temperature", 26.1));
            SensorData.Add(new(new(2021, 7, 16), "Temperature", 28.3));

            return FileIsOpen = SensorData.Count > 0;
        }

        /// <summary>
        /// LoadData from a CSV file.
        /// </summary>
        /// <param name="filename">The filename<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool LoadData(string filename)
        {
            // Prepare data
            SensorData = new();

            // Open file and load the data
            using (StreamReader reader = new(filename))
            using (CsvReader csv = new(reader, CultureInfo.CurrentCulture))
            {
                try
                {
                    foreach (SensorReading record in csv.GetRecords<SensorReading>())
                    {
                        SensorData.Add(record);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Loading Data", MessageBoxButton.OK, MessageBoxImage.Error);
                    SensorData.Clear();
                }
            }

            return FileIsOpen = SensorData.Count > 0;
        }

        /// <summary>
        /// Set the Window Title.
        /// </summary>
        private void SetTitle()
        {
            // Set the Window Title
            string fnText = "";

            if (fileName != null)
            {
                fnText = Path.GetFileName(fileName);

                if (DataIsDirty)
                {
                    fnText += @"*";
                }

                fnText += @" - ";
            }

            this.Title = fnText + TITLE;
        }

        /// <summary>
        /// Show the DisplayFilePage.
        /// </summary>
        private void ShowDisplayFilePage()
        {
            // Open DisplayFilePage
            DisplayFilePage = new DisplayFilePage(SensorData, this);
            CentreFrame.Content = DisplayFilePage;
        }

        /// <summary>
        /// Show the SensorRecordForm.
        /// </summary>
        private void ShowSensorReadingForm()
        {
            SetStatusText(null);

            SensorReadingForm = new(SensorReading);
            SensorReadingForm.Owner = this;

            if ((bool)SensorReadingForm.ShowDialog())
            {
                SensorData.Add(SensorReading);
                DataIsDirty = true;
                SetTitle();
                SetStatusText(@"New sensor reading added");
            }
        }
    }
}