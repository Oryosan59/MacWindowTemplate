using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MacWindowTemplate
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private readonly ObservableCollection<NoteItem> _allNotes;
        
        public ObservableCollection<NoteItem> CurrentNotes { get; private set; }

        private string _currentSection = "All Notes";

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string _sectionTitleText;
        public string SectionTitleText
        {
            get { return _sectionTitleText; }
            set { _sectionTitleText = value; OnPropertyChanged(nameof(SectionTitleText)); }
        }

        private string _sectionSubtitleText;
        public string SectionSubtitleText
        {
            get { return _sectionSubtitleText; }
            set { _sectionSubtitleText = value; OnPropertyChanged(nameof(SectionSubtitleText)); }
        }

        private Visibility _emptyStateVisibility = Visibility.Collapsed;
        public Visibility EmptyStateVisibility
        {
            get { return _emptyStateVisibility; }
            set { _emptyStateVisibility = value; OnPropertyChanged(nameof(EmptyStateVisibility)); }
        }

        private string _emptyStateIcon;
        public string EmptyStateIcon
        {
            get { return _emptyStateIcon; }
            set { _emptyStateIcon = value; OnPropertyChanged(nameof(EmptyStateIcon)); }
        }

        private string _emptyStateTitle;
        public string EmptyStateTitle
        {
            get { return _emptyStateTitle; }
            set { _emptyStateTitle = value; OnPropertyChanged(nameof(EmptyStateTitle)); }
        }

        private string _emptyStateSubtitle;
        public string EmptyStateSubtitle
        {
            get { return _emptyStateSubtitle; }
            set { _emptyStateSubtitle = value; OnPropertyChanged(nameof(EmptyStateSubtitle)); }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            _allNotes = new ObservableCollection<NoteItem>
            {
                new NoteItem { Title = "Welcome to Mac Notes", Content = "This is your first note! You can create, edit, and organize your notes using this beautiful Mac-style interface. Click on any note to edit it.", Category = "Personal", Date = "Today", IsFavorite = true },
                new NoteItem { Title = "Project Planning", Content = "Remember to review the project requirements and create a detailed timeline. Schedule meetings with the team and stakeholders.", Category = "Work", Date = "Yesterday", IsFavorite = false },
                new NoteItem { Title = "Shopping List", Content = "Grocery items needed: apples, bananas, milk, bread, eggs, cheese, yogurt, and some vegetables for the week.", Category = "Personal", Date = "2 days ago", IsFavorite = false },
                new NoteItem { Title = "App Ideas", Content = "1. Weather app with beautiful animations\n2. Task manager with time tracking\n3. Recipe organizer with shopping integration", Category = "Ideas", Date = "3 days ago", IsFavorite = true },
                new NoteItem { Title = "Meeting Notes", Content = "Discussed the new product launch strategy. Key points: target audience, marketing channels, budget allocation, and timeline.", Category = "Work", Date = "1 week ago", IsFavorite = false },
                new NoteItem { Title = "Book Recommendations", Content = "Must read: 'The Design of Everyday Things', 'Atomic Habits', 'Clean Code', and 'The Pragmatic Programmer'.", Category = "Personal", Date = "1 week ago", IsFavorite = true },
                new NoteItem { Title = "Travel Plans", Content = "Summer vacation ideas: Tokyo, Kyoto, Osaka. Research flights, hotels, and local attractions. Don\'t forget to check visa requirements.", Category = "Personal", Date = "2 weeks ago", IsFavorite = false },
                new NoteItem { Title = "Code Snippets", Content = "Useful CSS animations and JavaScript utilities. Store frequently used code patterns here for quick reference.", Category = "Work", Date = "2 weeks ago", IsFavorite = false },
                new NoteItem { Title = "Health Goals", Content = "Exercise 3 times per week, drink more water, get 8 hours of sleep, and maintain a balanced diet with more vegetables.", Category = "Personal", Date = "3 weeks ago", IsFavorite = true },
                new NoteItem { Title = "UI Design Inspiration", Content = "Collect beautiful interface designs, color palettes, and typography examples. Focus on clean, minimalist approaches.", Category = "Ideas", Date = "1 month ago", IsFavorite = false }
            };

            CurrentNotes = new ObservableCollection<NoteItem>(_allNotes);
            LoadNotes();
        }

        private void AllNotesButton_Click(object sender, RoutedEventArgs e)
        {
            LoadNotesForSection("All Notes", _allNotes);
        }

        private void FavoritesButton_Click(object sender, RoutedEventArgs e)
        {
            var favoriteNotes = _allNotes.Where(n => n.IsFavorite).ToList();
            LoadNotesForSection("Favorites", favoriteNotes);
        }

        private void WorkButton_Click(object sender, RoutedEventArgs e)
        {
            var workNotes = _allNotes.Where(n => n.Category == "Work").ToList();
            LoadNotesForSection("Work", workNotes);
        }

        private void PersonalButton_Click(object sender, RoutedEventArgs e)
        {
            var personalNotes = _allNotes.Where(n => n.Category == "Personal").ToList();
            LoadNotesForSection("Personal", personalNotes);
        }

        private void IdeasButton_Click(object sender, RoutedEventArgs e)
        {
            var ideaNotes = _allNotes.Where(n => n.Category == "Ideas").ToList();
            LoadNotesForSection("Ideas", ideaNotes);
        }

        private void TrashButton_Click(object sender, RoutedEventArgs e)
        {
            LoadNotesForSection("Trash", new List<NoteItem>());
            ShowEmptyState("🗑️", "Trash is empty", "Deleted notes will appear here");
        }

        private void LoadNotesForSection(string sectionName, IEnumerable<NoteItem> notes)
        {
            _currentSection = sectionName;
            SectionTitleText = sectionName;

            var notesList = notes.ToList();
            CurrentNotes.Clear();
            foreach (var note in notesList)
            {
                CurrentNotes.Add(note);
            }

            UpdateSectionSubtitle();

            if (CurrentNotes.Count == 0 && sectionName != "Trash")
            {
                ShowEmptyState("📝", $"No {sectionName.ToLower()} notes", "Create your first note to get started");
            }
            else
            {
                HideEmptyState();
            }
        }

        private void LoadNotes()
        {
            SectionTitleText = _currentSection;
            UpdateSectionSubtitle();
            HideEmptyState();
        }

        private void CreateNoteButton_Click(object sender, RoutedEventArgs e)
        {
            var newNote = new NoteItem
            {
                Title = "New Note",
                Content = "Start typing your note here...",
                Category = "Personal",
                Date = "Now",
                IsFavorite = false
            };

            _allNotes.Insert(0, newNote);

            if (_currentSection == "All Notes" || _currentSection == "Personal")
            {
                CurrentNotes.Insert(0, newNote);
            }

            UpdateSectionSubtitle();
            HideEmptyState();
        }

        private void NoteCard_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border { DataContext: NoteItem note })
            {
                var editDialog = new NoteEditDialog(note) { Owner = this };
                if (editDialog.ShowDialog() == true)
                {
                    UpdateSectionSubtitle();
                }
            }
        }

        private void FavoriteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button { DataContext: NoteItem note })
            {
                note.IsFavorite = !note.IsFavorite;
                // This requires the NoteItem to implement INotifyPropertyChanged to update the UI automatically
                // For now, we will just refresh the list
                RefreshNotes();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button { DataContext: NoteItem note })
            {
                var result = MessageBox.Show($"Are you sure you want to delete '{note.Title}'?", "Delete Note", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _allNotes.Remove(note);
                    CurrentNotes.Remove(note);
                    UpdateSectionSubtitle();

                    if (CurrentNotes.Count == 0)
                    {
                        ShowEmptyState("📝", $"No {_currentSection.ToLower()} notes", "Create your first note to get started");
                    }
                }
            }
        }
        
        private void RefreshNotes()
        {
            var currentNotesList = CurrentNotes.ToList();
            LoadNotesForSection(_currentSection, _allNotes.Where(n => currentNotesList.Contains(n)));
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            new SearchDialog { Owner = this }.ShowDialog();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            new SettingsDialog { Owner = this }.ShowDialog();
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Import feature coming soon!", "Import Notes", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ShowEmptyState(string icon, string title, string subtitle)
        {
            EmptyStateVisibility = Visibility.Visible;
            EmptyStateIcon = icon;
            EmptyStateTitle = title;
            EmptyStateSubtitle = subtitle;
        }

        private void HideEmptyState()
        {
            EmptyStateVisibility = Visibility.Collapsed;
        }

        private void UpdateSectionSubtitle()
        {
            var noteCount = CurrentNotes.Count;
            SectionSubtitleText = $"{noteCount} note{(noteCount != 1 ? "s" : "")}";
        }
    }

    public class NoteItem
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public string Date { get; set; }
        public bool IsFavorite { get; set; }
    }

    public class NoteEditDialog : Window
    {
        public NoteEditDialog(NoteItem note)
        {
            Title = "Edit Note";
            Width = 500;
            Height = 400;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Content = new TextBlock { Text = $"Note editing dialog for: {note.Title}", HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, FontSize = 16 };
        }
    }

    public class SearchDialog : Window
    {
        public SearchDialog()
        {
            Title = "Search Notes";
            Width = 400;
            Height = 300;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Content = new TextBlock { Text = "Search functionality coming soon!", HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, FontSize = 16 };
        }
    }

    public class SettingsDialog : Window
    {
        public SettingsDialog()
        {
            Title = "Settings";
            Width = 450;
            Height = 350;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Content = new TextBlock { Text = "Settings panel coming soon!", HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, FontSize = 16 };
        }
    }
}