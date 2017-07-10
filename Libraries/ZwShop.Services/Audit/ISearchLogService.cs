using System;
using System.Collections.Generic;

namespace ZwShop.Services.Audit
{
    /// <summary>
    /// �����ؼ��ʷ���
    /// </summary>
    public partial interface ISearchLogService
    {
        List<SearchTermReportLine> SearchTermReport(DateTime? startTime,
            DateTime? endTime, int count);

        List<SearchLog> GetAllSearchLogs();

        SearchLog GetSearchLogById(int searchLogId);

        void InsertSearchLog(SearchLog searchLog);

        void ClearSearchLog();
    }
}
