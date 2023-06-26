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

namespace LMS.Pages.MemberPages
{
    /// <summary>
    /// Contains methods for interacting with search bar, logout button and home button.
    /// </summary>
    public partial class MemberMainPage : Page
    {
        public event EventHandler<RoutedEventArgs> NavigateToLoginPage;
        private MemberHomePage memberHomePage { get; set; }
        private BookResultsPage bookResultsPage { get; set; }
        private BookInfoPage bookInfoPage { get; set; }
        public Member member { get; set; }

        public MemberMainPage(Member loggedInMember)
        {
            InitializeComponent();
            member = loggedInMember;
            memberHomePage = new MemberHomePage(member);
            PageContent.Content = memberHomePage;
            memberHomePage.RenewLoan += RenewLoan;
            memberHomePage.CancelReserve += CancelReserve;
        }

        private void NavigateToBookInfoPage(object sender, Book _book)
        {
            bookInfoPage = new BookInfoPage(_book);
            bookInfoPage.NavigateBackToResults += SearchBooks;
            bookInfoPage.PlaceReserve += PlaceReserve;
            PageContent.Content = bookInfoPage;
        }
        //initializes a new 'Reserve' object with the clicked 'book' and 'member'
        //loads every book and checks which book matches the i.d of the selected book, updates the selected one with '_book.isReserved = true;' and saves changes using 'FileManagement'.
        private void PlaceReserve(object sender, Book _book)
        {
            Reserve newReserve = new Reserve(_book, member);
            List<Book> books = FileManagement.LoadBooks();
            foreach (Book book in books)
            {
                if (book.id == newReserve.bookId)
                {
                    book.isReserved = true;
                    if (book.isLoaned)
                    {
                        List<Loan> loans = FileManagement.LoadLoans();
                        foreach (Loan loan in loans)
                        {
                            if (book.id == loan.bookId)
                            {
                                newReserve.dateDueBack = loan.dateDue;
                            }
                        }
                    }
                    else
                    {
                        newReserve.dateDueBack = "Now";
                    }
                }
            }
            member.reservedBooks.Add(newReserve);
            FileManagement.SaveNewReserve(newReserve);
            FileManagement.WriteAllBooks(books);
        }

        //Searches for the book associated with the passed 'book' object
        //updates the 'isReserved' status in 'bookInformation.csv' and Removes book from 'member.reservedBooks'
        private void CancelReserve(object sender, Reserve reserve)
        {
            List<Book> books = FileManagement.LoadBooks();
            foreach (Book _book in books)
            {
                if (_book.id == reserve.bookId)
                {
                    _book.isReserved = false;
                }
            }
            FileManagement.WriteAllBooks(books);
            member.reservedBooks.Remove(reserve);
            FileManagement.RemoveReserve(reserve);
        }

        private void RenewLoan(object sender, Loan loan)
        {
            member.loanedBooks.Remove(loan);
            FileManagement.RemoveLoan(loan);

            DateTime dateDue = DateTime.Parse($"{MainWindow.currentDate.Year}/" + loan.dateDue);
            loan.dateDue = dateDue.AddDays(7).ToString("yyyy/MM/dd"); //adds 7 days to loan
            FileManagement.SaveNewLoan(loan);


            loan.dateDue = DateTime.Parse(loan.dateDue).ToString("MM/dd"); //immediately changes format for viewing in program.
            member.loanedBooks.Add(loan);

            MessageBox.Show("Book renewed for 7 days");
        }

        private void SearchbarKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SearchBooks(sender, e);
            }
        }

        private void SearchIconButtonClick(object sender, RoutedEventArgs e)
        {
            SearchBooks(sender, e);
        }

        private void SearchBooks(object sender, RoutedEventArgs e)
        {
            string searchInput = SearchBar.Text.Trim();

            if (!string.IsNullOrEmpty(searchInput))
            {
                List<Book> searchResults = FileManagement.LoadBooks().Where(book =>
                    book.title.IndexOf(searchInput, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    book.authorFirstName.IndexOf(searchInput, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    book.authorLastName.IndexOf(searchInput, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    book.subject.IndexOf(searchInput, StringComparison.OrdinalIgnoreCase) >= 0
                ).ToList();

                bookResultsPage = new BookResultsPage(searchResults, searchInput);
                bookResultsPage.PlaceReserve += PlaceReserve;
                bookResultsPage.NavigateToBookInfoPage += NavigateToBookInfoPage;
                PageContent.Content = bookResultsPage;
            }
        }

        private void SearchBarGotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBar.Text == "Search...")
            {
                SearchBar.Text = string.Empty;
                SearchBar.FontWeight = FontWeights.Normal;
                SearchBar.Foreground = Brushes.Black;
            }
        }

        private void SearchBarLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchBar.Text))
            {
                SearchBar.Text = "Search...";
                SearchBar.Foreground = Brushes.Gray;
            }
        }
        private void LogoutButtonClick(object sender, RoutedEventArgs e)
        {
            NavigateToLoginPage?.Invoke(sender, e);
        }

        private void HomeButtonClick(object send, RoutedEventArgs e)
        {
            memberHomePage = new MemberHomePage(member);
            memberHomePage.CancelReserve += CancelReserve;
            memberHomePage.RenewLoan += RenewLoan;
            PageContent.Content = memberHomePage;
        }
    }
}
