export interface Pagination
{
    currentPage:number;
    itemsPerPage : number;
    totalItems:number;
    totalPages:number;

}

export class PaginationResult<T>
{
    Result:T;
    Pagination:Pagination;
}