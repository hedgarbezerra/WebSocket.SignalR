export interface PaginatedResult<T> {
  pageIndex: number
  previousPageIndex: number
  nextPageIndex: number
  pageSize: number
  totalCount: number
  totalPages: number
  data: Array<T>
  hasPreviousPage: boolean
  hasNextPage: boolean
}

export interface Pagination
{
  size: number,
  index: number,
  count: number,
  searchTerm: string
}
