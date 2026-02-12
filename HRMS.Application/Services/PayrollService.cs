using HRMS.Application.DTOs.Payroll;
using HRMS.Application.Interfaces;
using HRMS.Domain.Entities;
using HRMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Services
{
    public class PayrollService : IPayrollService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ISalaryRepository _salaryRepository;
        private readonly IPayrollRepository _payrollRepository;

        public PayrollService(
            IEmployeeRepository employeeRepository,
            ISalaryRepository salaryRepository,
            IPayrollRepository payrollRepository)
        {
            _employeeRepository = employeeRepository;
            _salaryRepository = salaryRepository;
            _payrollRepository = payrollRepository;
        }

        public async Task<string> GenerateMonthlyPayrollAsync(int month, int year)
        {
            var startOfMonth = new DateTime(year, month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
            var totalDaysInMonth = DateTime.DaysInMonth(year, month);

            var query = new DTOs.Employee.EmployeeQueryParams();
            //var (employees, _) = await _employeeRepository.GetAllAsync(query);
            var employees = await _employeeRepository.GetAllActiveAsync();


            foreach (var employee in employees)
            {
                // Skip if payroll already generated
                if (await _payrollRepository.ExistsAsync(employee.Id, month, year))
                    continue;

                // Skip if employee joined after this month
                if (employee.JoiningDate > endOfMonth)
                    continue;

                // Get salary valid for that month
                var salary = await _salaryRepository
                    .GetSalaryForMonthAsync(employee.Id, month, year);

                if (salary == null)
                    continue;

                // Determine actual working start date
                var actualWorkStart = employee.JoiningDate > startOfMonth
                    ? employee.JoiningDate
                    : startOfMonth;

                var workedDays = (endOfMonth - actualWorkStart).Days + 1;

                // Calculate gross salary
                var grossMonthly = salary.BasicSalary + salary.Allowance + salary.Bonus;

                // Pro-rated gross
                var dailyGross = grossMonthly / totalDaysInMonth;
                var proRatedGross = dailyGross * workedDays;

                // Pro-rated deduction
                var dailyDeduction = salary.Deduction / totalDaysInMonth;
                var proRatedDeduction = dailyDeduction * workedDays;

                // Tax based on pro-rated gross
                var tax = CalculateTax(proRatedGross);

                var netSalary = proRatedGross - proRatedDeduction - tax;

                var payroll = new Payroll
                {
                    EmployeeId = employee.Id,
                    Month = month,
                    Year = year,
                    BasicSalary = salary.BasicSalary,
                    Allowance = salary.Allowance,
                    Bonus = salary.Bonus,
                    Deduction = salary.Deduction,
                    Tax = tax,
                    NetSalary = netSalary,
                    GeneratedDate = DateTime.UtcNow,
                    Status = PayrollStatus.Generated,
                    CreatedAt = DateTime.UtcNow
                };

                await _payrollRepository.AddAsync(payroll);
            }

            await _payrollRepository.SaveChangesAsync();

            return "Payroll generated successfully.";
        }


        private decimal CalculateTax(decimal basicSalary)
        {
            // Example simple tax logic
            if (basicSalary > 50000)
                return basicSalary * 0.10m;

            return basicSalary * 0.05m;
        }

        public async Task<object> GetMonthlyReportAsync(int month, int year)
        {
            var payrolls = await _payrollRepository.GetByMonthAsync(month, year);

            if (!payrolls.Any())
                return "No payroll found for this month.";

            var totalNetSalary = payrolls.Sum(p => p.NetSalary);
            var totalTax = payrolls.Sum(p => p.Tax);

            var result = payrolls.Select(p => new PayrollDto
            {
                EmployeeId = p.EmployeeId,
                EmployeeName = $"{p.Employee.FirstName} {p.Employee.LastName}",
                BasicSalary = p.BasicSalary,
                Allowance = p.Allowance,
                Bonus = p.Bonus,
                Deduction = p.Deduction,
                Tax = p.Tax,
                NetSalary = p.NetSalary,
                Status = p.Status
            }).ToList();

            return new
            {
                Month = month,
                Year = year,
                TotalEmployees = payrolls.Count,
                TotalNetSalary = totalNetSalary,
                TotalTax = totalTax,
                Payrolls = result
            };
        }

        public async Task<object?> GetEmployeePayrollAsync(int employeeId, int month, int year)
        {
            var payrolls = await _payrollRepository.GetByMonthAsync(month, year);

            var payroll = payrolls.FirstOrDefault(p => p.EmployeeId == employeeId);

            if (payroll == null)
                return null;

            return new
            {
                Employee = $"{payroll.Employee.FirstName} {payroll.Employee.LastName}",
                Month = payroll.Month,
                Year = payroll.Year,
                GrossSalary = payroll.BasicSalary + payroll.Allowance + payroll.Bonus,
                Deduction = payroll.Deduction,
                Tax = payroll.Tax,
                NetSalary = payroll.NetSalary,
                Status = payroll.Status
            };
        }


    }
}
