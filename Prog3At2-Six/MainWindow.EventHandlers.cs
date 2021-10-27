/*
 *  File Name:   MainWindow.EventHandlers.cs
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
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Windows;
    using System.Windows.Input;

    using CsvHelper;

    using Microsoft.Win32;

    /// <summary>
    /// Defines the <see cref="MainWindow" />.
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// The AboutCommand_CanExecute.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="CanExecuteRoutedEventArgs"/>.</param>
        private void AboutCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        /// <summary>
        /// The AboutCommand_Executed.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="ExecutedRoutedEventArgs"/>.</param>
        private void AboutCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Display the About dialog
            AboutWindow aboutWindow = new();
            aboutWindow.Owner = this;
            aboutWindow.ShowDialog();

            e.Handled = true;
        }

        /// <summary>
        /// The AddCommand_CanExecute.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="CanExecuteRoutedEventArgs"/>.</param>
        private void AddCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = FileIsOpen;
        }

        /// <summary>
        /// The AddCommand_Executed.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="ExecutedRoutedEventArgs"/>.</param>
        private void AddCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Display the CensorRecordForm
            SensorReading = new();
            ShowSensorReadingForm();

            e.Handled = true;
        }

        /// <summary>
        /// The CloseCommand_CanExecute.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="CanExecuteRoutedEventArgs"/>.</param>
        private void CloseCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = FileIsOpen;
        }

        /// <summary>
        /// The CloseCommand_Executed.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="ExecutedRoutedEventArgs"/>.</param>
        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            bool cancel = false;

            if (DataIsDirty && MessageBox.Show(
                        $"Data has changed!\nDo you still want to Close the file without saving?",
                        "Closing File", MessageBoxButton.YesNo
                        , MessageBoxImage.Exclamation)
                        == MessageBoxResult.No)
            {
                cancel = true;
            }

            if (!cancel)
            {
                CentreFrame.Content = blankPage;
                FileIsOpen = DataIsDirty = FileIsNew = false;
                FileName = null;
                SetStatusText(null);
            }

            e.Handled = true;
        }

        /// <summary>
        /// The ExitCommand_CanExecute.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="CanExecuteRoutedEventArgs"/>.</param>
        private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        /// <summary>
        /// The ExitCommand_Executed.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="ExecutedRoutedEventArgs"/>.</param>
        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
            e.Handled = true;
        }

        /// <summary>
        /// The NewCommand_CanExecute.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="System.Windows.Input.CanExecuteRoutedEventArgs"/>.</param>
        private void NewCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !FileIsOpen;
        }

        /// <summary>
        /// The NewCommand_Executed.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="System.Windows.Input.ExecutedRoutedEventArgs"/>.</param>
        private void NewCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            SensorData = new();
            FileIsNew = FileIsOpen = true;
            FileName = NEW_FILE_NAME;
            SetStatusText(@"New memory file open");
            ShowDisplayFilePage();
        }

        /// <summary>
        /// The OpenCommand_CanExecute.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="CanExecuteRoutedEventArgs"/>.</param>
        private void OpenCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !FileIsOpen;
        }

        /// <summary>
        /// The OpenCommand_Executed.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="ExecutedRoutedEventArgs"/>.</param>
        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Display FileOpenDialog
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = @"CSV files (*.csv)|*.csv";
            openFileDialog.CheckFileExists = true;
            openFileDialog.Title = @"Open CSV File";

            if (openFileDialog.ShowDialog() == true)
            {
                string filename = openFileDialog.FileName;

                if (LoadData(filename))
                {
                    FileName = filename;
                    SetStatusText($"Opened file: {Path.GetFileName(filename)}");
                    ShowDisplayFilePage();
                }
            }

            e.Handled = true;
        }

        /// <summary>
        /// The SaveAsCommand_CanExecute.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="System.Windows.Input.CanExecuteRoutedEventArgs"/>.</param>
        private void SaveAsCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = FileIsOpen;
        }

        /// <summary>
        /// The SaveAsCommand_Executed.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="System.Windows.Input.ExecutedRoutedEventArgs"/>.</param>
        private void SaveAsCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            // Display SaveFileDialog
            SaveFileDialog saveFileDialog = new();
            saveFileDialog.Filter = @"CSV files (*.csv)|*.csv";
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.Title = @"Save CSV File As";
            saveFileDialog.DefaultExt = @".csv";
            saveFileDialog.AddExtension = true;
            saveFileDialog.FileName = FileIsNew ? DEFAULT_FILENAME : FileName;

            if (saveFileDialog.ShowDialog() == true)
            {
                string filename = saveFileDialog.FileName;

                // Save the data to the selected file
                using (var writer = new StreamWriter(filename))
                using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
                {
                    csv.WriteRecords(SensorData);
                    DataIsDirty = false;
                    FileIsNew = false;
                    FileName = filename;
                    SetStatusText($"Data saved to file: {Path.GetFileName(filename)}");
                }
            }

            e.Handled = true;
        }

        /// <summary>
        /// The SaveCommand_CanExecute.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="CanExecuteRoutedEventArgs"/>.</param>
        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = DataIsDirty && !FileIsNew;
        }

        /// <summary>
        /// The SaveCommand_Executed.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="ExecutedRoutedEventArgs"/>.</param>
        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Save the data to the original file
            using (var writer = new StreamWriter(FileName))
            using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
            {
                csv.WriteRecords(SensorData);
            }

            SetStatusText($"Data saved");
            DataIsDirty = false;
            SetTitle();

            e.Handled = true;
        }

        /// <summary>
        /// The Window_Closing.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="CancelEventArgs"/>.</param>
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (DataIsDirty && MessageBox.Show(
                        $"Data has changed!\nDo you still want to Exit without saving?",
                        "Exiting", MessageBoxButton.YesNo
                        , MessageBoxImage.Exclamation)
                        == MessageBoxResult.No)
            {
                e.Cancel = true;
            }

            if (!e.Cancel)
            {
                Application.Current.Shutdown();
            }
        }
    }
}