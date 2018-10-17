using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net;

namespace CrudApiRest.Shared.Common.ViewModels
{
    public class PagingData
    {
        private string _filter;
        private string _orderBy;
        public int? PageSize { get; set; }
        public int? Page { get; set; }

        public string Filter
        {
            get => _filter;
            set => _filter = WebUtility.UrlDecode(value);

        }

        public string OrderBy
        {
            get => _orderBy;
            set => _orderBy = WebUtility.UrlDecode(value);
        }

        public Dictionary<string, string> ToDictionary()
        {
            return new Dictionary<string, string>()
            {
                { nameof(Filter), Filter },
                { nameof(OrderBy), OrderBy },
                { nameof(PageSize), PageSize?.ToString() },
                { nameof(Page), Page?.ToString() }
            };
        }

        public void TruncateQuery(HttpContext context, int pageSize = 1000)
        {
            if (!PageSize.HasValue || PageSize > pageSize || PageSize <= 0)
            {
                PageSize = 1000;
                context.Response.StatusCode = (int)HttpStatusCode.PartialContent;
            }

        }
    }
}
