export interface QueryDataModel {
  moduleId: string,
  viewId: string,
  pageIndex: number,
  orderExpression: string,
  pageSize: number,
  totalRecord: number,
  columns: any,
  data: any
}

export class QueryDataModel {
  constructor() {

  }
}
