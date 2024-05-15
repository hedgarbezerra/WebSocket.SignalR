export interface ApiResult<T = unknown> {
  success: boolean
    data: T | null
    errors: string[]
    successes: string[]
}

export interface ApiEmptyResult extends ApiResult<void>{

}
