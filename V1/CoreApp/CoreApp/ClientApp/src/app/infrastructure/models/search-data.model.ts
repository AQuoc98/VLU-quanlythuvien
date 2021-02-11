import { QueryDataModel, ViewSearchConditionModel } from ".";

export interface SearchDataModel {
    queryDataModel: QueryDataModel,
    searchConditions: ViewSearchConditionModel[]
}

export class SearchDataModel {
    constructor() { }
}