﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LMS.Pages.AdminPages
{
    /// <summary>
    /// Interaction logic for ViewMemberPage.xaml
    /// </summary>
    /// 
    public partial class ViewBookPage : Page
    {
        private bool isEditing { get; set; } = false;
        private bool isConfirmed { get; set; } = false;
        public Book bookInfo { get; set; }
        public ViewBookPage(Book book)
        {
            InitializeComponent();

            bookInfo = book;
            ID.Text = bookInfo.id;
            Title.Text = bookInfo.title;
            AuthorFirstName.Text = bookInfo.authorFirstName;
            AuthorLastName.Text = bookInfo.authorLastName;
            Subject.Text = bookInfo.subject;
            Summary.Text = bookInfo.summary;
        }
        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            if (NavigationService != null && NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void EditCancelButtonClick(object sender, RoutedEventArgs e)
        {
            if (!isEditing)
            {
                isEditing = true;
                isConfirmed = false;
                SaveButton.Visibility = Visibility.Visible;
                DeleteButton.Visibility = Visibility.Visible;
                EditCancelButton.Content = "Cancel";

                ID.Background = (Brush)new BrushConverter().ConvertFrom("#e7ead4");
                ID.Padding = new Thickness(20, 0, 0, 0);
                ID.IsReadOnly = false;
                Title.Background = (Brush)new BrushConverter().ConvertFrom("#e7ead4");
                Title.Padding = new Thickness(20, 0, 0, 0);
                Title.IsReadOnly = false;
                AuthorFirstName.Background = (Brush)new BrushConverter().ConvertFrom("#e7ead4");
                AuthorFirstName.Padding = new Thickness(20, 0, 0, 0);
                AuthorFirstName.IsReadOnly = false;
                AuthorLastName.Background = (Brush)new BrushConverter().ConvertFrom("#e7ead4");
                AuthorLastName.Padding = new Thickness(20, 0, 0, 0);
                AuthorLastName.IsReadOnly = false;
                Subject.Background = (Brush)new BrushConverter().ConvertFrom("#e7ead4");
                Subject.Padding = new Thickness(20, 0, 0, 0);
                Subject.IsReadOnly = false;
                Summary.Background = (Brush)new BrushConverter().ConvertFrom("#e7ead4");
                Summary.Padding = new Thickness(20);
                Summary.IsReadOnly = false;
            }
            else
            {
                isEditing = false;
                SaveButton.Visibility = Visibility.Hidden;
                DeleteButton.Visibility = Visibility.Hidden;
                EditCancelButton.Content = "Edit";

                ID.Background = Brushes.Transparent;
                ID.Padding = new Thickness(0);
                ID.IsReadOnly = true;
                Title.Background = Brushes.Transparent;
                Title.Padding = new Thickness(0);
                Title.IsReadOnly = true;
                AuthorFirstName.Background = Brushes.Transparent;
                AuthorFirstName.Padding = new Thickness(0);
                AuthorFirstName.IsReadOnly = true;
                AuthorLastName.Background = Brushes.Transparent;
                AuthorLastName.Padding = new Thickness(0);
                AuthorLastName.IsReadOnly = true;
                Subject.Background = Brushes.Transparent;
                Subject.Padding = new Thickness(0);
                Subject.IsReadOnly = true;
                Summary.Background = Brushes.Transparent;
                Summary.Padding = new Thickness(0, 20, 0, 0);
                Summary.IsReadOnly = true;
            }
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {

        }
        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            if (!isConfirmed)
            {
                isConfirmed = true;
                DeleteButton.Content = "Confirm?";
            }
            else
            {
                //delete user
                isConfirmed = false;
            }
        }
    }

}
