import { Injectable } from '@angular/core';

import { ApiService } from '../../core/services';
import { BaseService } from '../../core/services/base.service';

@Injectable()
export class CorePolicyApiServices extends BaseService {
    constructor(public apiService: ApiService) {
        super(apiService, 'DoPolicyApi');
    }
    Insert(PolicyModel) {
        return this.apiService.post(`${this.apiName}/Insert`, PolicyModel);
    }

    Update(PolicyModel) {
        return this.apiService.post(`${this.apiName}/Update`, PolicyModel);
    }

}
