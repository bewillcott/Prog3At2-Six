/*
 *  File Name:   MainWindow.xaml.cs
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
 * Date: 21/10/2021
 * ****************************************************************
 */

namespace Prog3At2_Six
{
    using System.Collections.ObjectModel;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Defines the DEFAULT_FILENAME.
        /// </summary>
        private const string DEFAULT_FILENAME = @"CensorData.csv";

        /// <summary>
        /// Defines the NEW_FILE_NAME.
        /// </summary>
        private const string NEW_FILE_NAME = @"Untitled";

        /// <summary>
        /// Defines the TITLE.
        /// </summary>
        private const string TITLE = @"Prog3 AT2 Six";

        /// <summary>
        /// Defines the blankPage.
        /// </summary>
        private static readonly BlankPage blankPage = new BlankPage();

        /// <summary>
        /// Defines the fileName.
        /// </summary>
        private string fileName;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            FileName = null;
            CentreFrame.Content = blankPage;
        }

        /// <summary>
        /// Gets the CensorData.
        /// </summary>
        public ObservableCollection<CensorRecord> CensorData { get; private set; }

        /// <summary>
        /// Gets the CensorRecord.
        /// </summary>
        public CensorRecord CensorRecord { get; private set; }

        /// <summary>
        /// Gets the CensorRecordForm.
        /// </summary>
        public CensorRecordForm CensorRecordForm { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether DataIsDirty.
        /// </summary>
        public bool DataIsDirty { get; set; }

        /// <summary>
        /// Gets the DisplayFilePage.
        /// </summary>
        public DisplayFilePage DisplayFilePage { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether FileIsNew.
        /// </summary>
        public bool FileIsNew { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether FileIsOpen.
        /// </summary>
        public bool FileIsOpen { get; set; }

        /// <summary>
        /// Gets the FileName.
        /// </summary>
        public string FileName
        {
            get => fileName;

            private set
            {
                fileName = value;
                SetTitle();
            }
        }
    }
}