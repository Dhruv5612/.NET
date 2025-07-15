using System;
using System.Collections.Generic;

namespace EmployeeAttendanceTracker
{
    // Class representing a single attendance record
    public class EmployeeAttendance
    {
        // Properties
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }

        // Constructor
        public EmployeeAttendance(int employeeId, string employeeName, DateTime date, bool isPresent)
        {
            EmployeeId = employeeId;
            EmployeeName = employeeName;
            Date = date;
            IsPresent = isPresent;
        }
    }

    // Business Logic and Data Storage
    public class AttendanceManager
    {
        private List<EmployeeAttendance> attendanceList = new List<EmployeeAttendance>();

        // Add new attendance record
        public void AddRecord(EmployeeAttendance attendance)
        {
            attendanceList.Add(attendance);
            Console.WriteLine("✅ Attendance record added successfully.\n");
        }

        // Display all records
        public void DisplayAllRecords()
        {
            Console.WriteLine("\n==== All Attendance Records ====\n");

            if (attendanceList.Count == 0)
            {
                Console.WriteLine("No records found.\n");
                return;
            }

            foreach (var record in attendanceList)
            {
                Console.WriteLine($"ID: {record.EmployeeId}, Name: {record.EmployeeName}, Date: {record.Date.ToShortDateString()}, Present: {(record.IsPresent ? "Yes" : "No")}");
            }
            Console.WriteLine();
        }

        // Get total present days for a specific employee
        public void GetTotalPresentDays(int employeeId)
        {
            int count = 0;
            foreach (var record in attendanceList)
            {
                if (record.EmployeeId == employeeId && record.IsPresent)
                {
                    count++;
                }
            }

            Console.WriteLine($"\n📅 Employee ID {employeeId} was present for {count} day(s).\n");
        }
    }

    // Main Application
    class Program
    {
        static void Main(string[] args)
        {
            AttendanceManager manager = new AttendanceManager();
            int choice;

            do
            {
                Console.WriteLine("===== Employee Attendance Tracker =====");
                Console.WriteLine("1. Add Attendance Record");
                Console.WriteLine("2. View All Attendance Records");
                Console.WriteLine("3. Get Total Present Days by Employee ID");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");

                bool validChoice = int.TryParse(Console.ReadLine(), out choice);
                if (!validChoice || choice < 1 || choice > 4)
                {
                    Console.WriteLine("❌ Invalid choice. Please enter a number between 1 and 4.\n");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddAttendance(manager);
                        break;
                    case 2:
                        manager.DisplayAllRecords();
                        break;
                    case 3:
                        GetTotalDays(manager);
                        break;
                    case 4:
                        Console.WriteLine("👋 Exiting the program. Goodbye!");
                        break;
                }

            } while (choice != 4);
        }

        // Add attendance logic
        static void AddAttendance(AttendanceManager manager)
        {
            try
            {
                Console.Write("Enter Employee ID (number): ");
                if (!int.TryParse(Console.ReadLine(), out int empId))
                {
                    Console.WriteLine("❌ Invalid Employee ID. Must be a number.\n");
                    return;
                }

                Console.Write("Enter Employee Name: ");
                string name = Console.ReadLine();

                Console.Write("Enter Date (yyyy-mm-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
                {
                    Console.WriteLine("❌ Invalid date format. Use yyyy-mm-dd.\n");
                    return;
                }

                Console.Write("Was the employee present? (Y/N): ");
                string input = Console.ReadLine().Trim().ToUpper();
                bool isPresent;

                if (input == "Y") isPresent = true;
                else if (input == "N") isPresent = false;
                else
                {
                    Console.WriteLine("❌ Invalid input for presence. Use Y or N.\n");
                    return;
                }

                EmployeeAttendance record = new EmployeeAttendance(empId, name, date, isPresent);
                manager.AddRecord(record);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error adding record: {ex.Message}\n");
            }
        }

        // Get total present days
        static void GetTotalDays(AttendanceManager manager)
        {
            Console.Write("Enter Employee ID: ");
            if (!int.TryParse(Console.ReadLine(), out int empId))
            {
                Console.WriteLine("❌ Invalid Employee ID. Must be a number.\n");
                return;
            }

            manager.GetTotalPresentDays(empId);
        }
    }
}
