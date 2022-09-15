using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Arch.MultiTenancy.HostDashboard.Dto;

namespace Arch.MultiTenancy.HostDashboard
{
    public interface IIncomeStatisticsService
    {
        Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval);
    }
}