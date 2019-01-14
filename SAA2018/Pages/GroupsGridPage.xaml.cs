using System;
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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using SAA2018.Models;

namespace SAA2018.Pages
{
    /// <summary>
    /// Логика взаимодействия для GroupsGridPage.xaml
    /// </summary>
    public partial class GroupsGridPage : Page
    {
        ObservableCollection<Group> Groups;
        DBL dbl;
        public GroupsGridPage(DBL d)
        {
            InitializeComponent();
            dbl = d;
            Groups = new ObservableCollection<Group>();
            if (dbl.accounttype == DBL.AccountType.Dean)
            {
                foreach (Group g in dbl.GiveGroups())
                    if (g.University == dbl.LoggedPerson.University && g.Faculty == ((Dean)dbl.LoggedPerson).Faculty)
                        Groups.Add(g);
            }
            else
            {
                foreach (Group g in dbl.GiveGroups())
                    if (g.University == dbl.LoggedPerson.University)
                        Groups.Add(g);
            }
            GroupsGrid.ItemsSource = Groups;
        }

        private void GroupsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GroupsGrid.SelectedItem != null)
            {
                RemoveGroup.IsEnabled = true;
                EditGroup.IsEnabled = true;
            }
            else
            {
                RemoveGroup.IsEnabled = false;
                EditGroup.IsEnabled = false;
            }
        }

        private void AddGroup_Click(object sender, RoutedEventArgs e)
        {
            Group group = null;
            GroupEdite_Event(group);
        }

        private void RemoveGroup_Click(object sender, RoutedEventArgs e)
        {
            int SelectedGroupId;
            SelectedGroupId = GroupsGrid.SelectedIndex;
            Group SelectedGroup;
            SelectedGroup = (Group)GroupsGrid.Items[SelectedGroupId];
            dbl.RemoveGroup(SelectedGroup);
            Groups.Remove(SelectedGroup);
        }

        private void EditGroup_Click(object sender, RoutedEventArgs e)
        {
            int SelectedGroupId;
            SelectedGroupId = GroupsGrid.SelectedIndex;
            Group SelectedGroup;
            SelectedGroup = (Group)GroupsGrid.Items[SelectedGroupId];
            GroupEdite_Event(SelectedGroup);
        }

        public delegate void GroupDataEventHandler(Group group);
        public event GroupDataEventHandler GroupEdite_Event;
    }
}
