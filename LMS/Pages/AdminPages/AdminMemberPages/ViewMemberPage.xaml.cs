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
    public partial class ViewMemberPage : Page
    {
        private bool isEditing = false;
        private bool isConfirmed = false;
        private Member memberInfo;

        //Member info from login and member class's are passed through parameters and displayed in each text example.
        public ViewMemberPage(Member member)
        {
            memberInfo = member;
            InitializeComponent();

            ID.Text = memberInfo.id;
            PIN.Text = memberInfo.pin;
            FirstName.Text = memberInfo.firstName;
            LastName.Text = memberInfo.lastName;
            Email.Text = memberInfo.email;
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
                SaveButton.Visibility = Visibility.Visible;
                DeleteButton.Visibility = Visibility.Visible;
                EditCancelButton.Content = "Cancel";
            }
            else
            {
                isEditing = false;
                SaveButton.Visibility = Visibility.Hidden;
                DeleteButton.Visibility = Visibility.Hidden;
                EditCancelButton.Content = "Edit";
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
