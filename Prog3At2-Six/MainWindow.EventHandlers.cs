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
    using System.Windows;
    using System.Windows.Input;

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
            // TODO: Display the About dialog
            MessageBox.Show("Help/About selected");
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
            // TODO: Display the Add dialog
            MessageBox.Show("Edit/Add selected or Ctrl+A pressed!");
            DataIsDirty = true;
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
                        $"Data is dirty!\nDo you still want to Close the file?",
                        "Closing File", MessageBoxButton.YesNo
                        , MessageBoxImage.Exclamation)
                        == MessageBoxResult.No)
            {
                cancel = true;
            }

            if (!cancel)
            {
                CentreFrame.Content = blankPage;
                FileIsOpen = false;
                DataIsDirty = false;
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
            // TODO: Display FileOpenDialog
            // TODO: Open/Read in the file
            // Prepare data
            CensorData = new();

            // Test data
            CensorData.Add(new(new(2021, 1, 12), "Temperature", 25.0));
            CensorData.Add(new(new(2021, 1, 13), "Temperature", 27.3));
            CensorData.Add(new(new(2021, 1, 14), "Temperature", 22.4));
            CensorData.Add(new(new(2021, 1, 15), "Temperature", 26.1));
            CensorData.Add(new(new(2021, 1, 16), "Temperature", 28.3));

            // Open DisplayFilePage
            DisplayFilePage = new DisplayFilePage(CensorData);
            CentreFrame.Content = DisplayFilePage;

            //MessageBox.Show("File/Open selected or Ctrl+O pressed!");
            FileIsOpen = true;
            e.Handled = true;
        }

        /// <summary>
        /// The SaveCommand_CanExecute.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="CanExecuteRoutedEventArgs"/>.</param>
        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = DataIsDirty;
        }

        /// <summary>
        /// The SaveCommand_Executed.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="ExecutedRoutedEventArgs"/>.</param>
        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // TODO: Save the data to the original file
            MessageBox.Show("File/Save selected or Ctrl+S pressed!");
            DataIsDirty = false;
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
                        $"Data is dirty!\nDo you still want to Exit?",
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