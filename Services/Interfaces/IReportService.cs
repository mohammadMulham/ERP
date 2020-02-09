using ERPAPI.ViewModels.Reports;
using ERPAPI.ViewModels.Reports.CustomerAccountStatement;
using ERPAPI.ViewModels.Reports.Journal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Services
{
    public interface IReportService
    {
        JournalViewModel GetjournalEntry(Guid? accountId, Guid? costCenterId, DateTimeOffset? from, DateTimeOffset? fromReleaseDate, DateTimeOffset? toReleaseDate, DateTimeOffset? to, JournalOptions options);
        CustomerAccountStatementViewModel GetCustomerAccountStatment(Guid? customerId, Guid? cusAccountId, Guid? costCenterId, DateTimeOffset? from, DateTimeOffset? fromReleaseDate, DateTimeOffset? toReleaseDate, DateTimeOffset? to);
    }
}
