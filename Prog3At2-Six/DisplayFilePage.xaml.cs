/*
 *  File Name:   DisplayFilePage.xaml.cs
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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;

    /// <summary>
    /// Interaction logic for DisplayFilePage.xaml.
    /// </summary>
    public partial class DisplayFilePage : Page
    {
        /// <summary>
        /// Defines the censorData.
        /// </summary>
        private readonly ObservableCollection<CensorRecord> censorData;

        /// <summary>
        /// Defines the listViewSortAdorner.
        /// </summary>
        private SortAdorner listViewSortAdorner;

        /// <summary>
        /// Defines the listViewSortCol.
        /// </summary>
        private GridViewColumnHeader listViewSortCol;

        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayFilePage"/> class.
        /// </summary>
        /// <param name="censorData">The censorData<see cref="List{CensorRecord}"/>.</param>
        public DisplayFilePage(ObservableCollection<CensorRecord> censorData)
        {
            InitializeComponent();
            this.censorData = censorData;
            csvListView.ItemsSource = censorData;
        }

        /// <summary>
        /// The csvListViewColumnHeader_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private void csvListViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();

            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                csvListView.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;

            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
            {
                newDir = ListSortDirection.Descending;
            }

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            csvListView.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }
    }
}