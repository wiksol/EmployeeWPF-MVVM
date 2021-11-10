using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Second
{
    public class MainViewModel : Base
    {
        //creating list of employees and list of tasks
        public ObservableCollection<Employee> EmployeesList { get; set; } = new ObservableCollection<Employee>();
        public ObservableCollection<Task> TasksList { get; set; } = new ObservableCollection<Task>();

        public string NewEmployeeFirstName { get; set; }
        public string NewEmployeeLastName { get; set; }
        public string FirstAndLastNameShowedEmployee { get; set; }
        public string IdShowedEmployee { get; set; }
        private int selectedEmployeeId;

        public ICommand AddNewEmployeeCommand { get; set; }
        public ICommand ShowSelectedEmployeeCommand { get; set; }
        public ICommand RemoveEmployeeCommand { get; set; }
        public ICommand DeleteEmployeeCommand { get; set; }


        public MainViewModel()
        {
            //Relay command 
            AddNewEmployeeCommand = new RelayCommand(AddNewEmployee);
            ShowSelectedEmployeeCommand = new RelayCommand(ShowSelectedEmployee);
            RemoveEmployeeCommand = new RelayCommand(RemoveEmployee);
            DeleteEmployeeCommand = new RelayCommand(DeleteEmployee);

            //add employee to showed list
            foreach (var employee in DatabaseLocator.Database.Employees.ToList())
            {
                EmployeesList.Add(new Employee
                {
                    EmployeeId = employee.EmployeeId,
                    FirstName = employee.FirstName,
                    NumberOfUnfinishedTasks = employee.Tasks.Count(x => x.EmployeeId == employee.EmployeeId),
                    IsChecked = false
                });
            }


        }


        private void AddNewEmployee()
        {
            //to add an employee insert only name
            if (string.IsNullOrEmpty(NewEmployeeFirstName))
            {
                MessageBox.Show("Wprowadź imię pracownika");
            }
            else
            {
                //add employee to database
                DatabaseLocator.Database.Employees.Add(new Second.Database.Employee
                {
                    FirstName = NewEmployeeFirstName,
                    LastName = NewEmployeeLastName
                });
                DatabaseLocator.Database.SaveChanges();

                //add employee to showed list
                var newEmployee = new Employee
                {
                    EmployeeId = DatabaseLocator.Database.Employees.Max(p => p.EmployeeId),
                    FirstName = NewEmployeeFirstName,
                };
                EmployeesList.Add(newEmployee);

                //clear text boxes with first and last name
                NewEmployeeFirstName = string.Empty;
                OnPropertyChanged(nameof(NewEmployeeFirstName));
                NewEmployeeLastName = string.Empty;
                OnPropertyChanged(nameof(NewEmployeeLastName));
            }
        }

        private void RemoveEmployee()
        {

        }

        private void DeleteEmployee()
        {
            //var selectedEmployee = EmployeesList.Where(x => x.IsChecked).FirstOrDefault();
            //var foundEntity = DatabaseLocator.Database.Employees.FirstOrDefault(x => x.EmployeeId == selectedEmployee.EmployeeId);
            //if (foundEntity != null)
            //{
            //    DatabaseLocator.Database.Employees.Remove(foundEntity);

            //    EmployeesList.Remove(selectedEmployee);
            //}
            //DatabaseLocator.Database.SaveChanges();
        }


        private void ShowSelectedEmployee()
        {
            
            //search checked employee and return his Id
            selectedEmployeeId = EmployeesList.FirstOrDefault(x => x.IsChecked == true).EmployeeId;
            //using employee Id find his tasks
            var SelectedTasks = DatabaseLocator.Database.Tasks.Where(x => x.EmployeeId == selectedEmployeeId).ToList();
            //find employee full name
            string firstName = DatabaseLocator.Database.Employees.Where(x => x.EmployeeId == selectedEmployeeId).FirstOrDefault().FirstName;
            string lastName = DatabaseLocator.Database.Employees.Where(x => x.EmployeeId == selectedEmployeeId).FirstOrDefault().LastName;
            FirstAndLastNameShowedEmployee = firstName + " " + lastName;
            OnPropertyChanged(nameof(FirstAndLastNameShowedEmployee));
            IdShowedEmployee = selectedEmployeeId.ToString();
            OnPropertyChanged(nameof(IdShowedEmployee));

            //clear showe task list before adding new data
            TasksList.Clear();
            foreach (var task in SelectedTasks)
            {
                TasksList.Add(new Task
                {
                    TaskId = task.TaskId,
                    Description = task.Description,
                    TaskCategoryDescription = DatabaseLocator.Database.TaskCategories.Where(x=>x.TaskCategoryId == task.TaskCategoryId).FirstOrDefault().Description
                });
            }
        }
    }
}
