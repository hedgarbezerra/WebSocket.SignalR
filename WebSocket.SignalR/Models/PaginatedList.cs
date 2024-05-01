﻿using WebSocket.SignalR.Interfaces;

namespace WebSocket.SignalR.Models
{
    public class PaginatedList<T>
    {
        public int PageIndex { get; private set; }
        public int PreviousPageIndex { get; private set; }
        public int NextPageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }
        public Uri? NextPage { get; private set; }
        public Uri? PreviousPage { get; private set; }
        public List<T> Data { get; private set; }

        public PaginatedList(IQueryable<T> source, IUriService uriService, string route, int pageIndex, int pageSize = 10)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = source.Count();
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            NextPageIndex = HasNextPage ? pageIndex + 1 : 0;
            PreviousPageIndex = HasPreviousPage ? pageIndex - 1 : 0;
            NextPage = HasNextPage ? uriService.GetPageUri(pageIndex + 1, PageSize, route) : null;
            PreviousPage = HasPreviousPage ? uriService.GetPageUri(pageIndex - 1, PageSize, route) : null;
            Data = source.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }
        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }
    }
}
