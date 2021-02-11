import { Injectable } from '@angular/core';

import { ApiService } from '../../core/services';
import { BaseService } from '../../core/services/base.service';
import { QueryDataModel, SearchDataModel } from '../models';

@Injectable()
export class CoreViewApiService extends BaseService {

    constructor(public apiService: ApiService) {
        super(apiService, 'CoreViewApi');
    }

    queryData(model: QueryDataModel) {
        return this.apiService.post(`${this.apiName}/QueryData`, model);
    }

    searchData(model: SearchDataModel) {
        return this.apiService.post(`${this.apiName}/SearchData`, model);
    }

    getViewDetails(moduleId: string) {
        return this.apiService.get(`${this.apiName}/GetViewDetails?moduleId=${moduleId}`);
    }

    getViewsByModule(moduleId: string) {
        return this.apiService.get(`${this.apiName}/GetViewsByModule?moduleId=${moduleId}`);
    }

    getGridViewFilterSelectData(moduleId: string) {
        return this.apiService.get(`${this.apiName}/GetGridViewFilterSelectData?moduleId=${moduleId}`);
    }
}