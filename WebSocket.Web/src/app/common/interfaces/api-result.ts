export interface ApiResult<T = unknown> {
    success: boolean
    data: T | null
    errors: string[]
    successes: string[]
}

export interface ApiEmptyResult extends ApiResult<void>{

}

export function isApiResult(result : any) : result is ApiResult<any>{
  return 'success' in result && 'data' in result && 'errors' in result && 'successes' in result;
}
