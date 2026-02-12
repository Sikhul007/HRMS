using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Interfaces
{
    public interface IPayrollService
    {
        Task<string> GenerateMonthlyPayrollAsync(int month, int year);
        Task<object> GetMonthlyReportAsync(int month, int year);
    }
}
