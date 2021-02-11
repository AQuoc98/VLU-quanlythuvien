import { Injectable } from '@angular/core';

import { ApiService } from '../../core/services';
import { BaseService } from '../../core/services/base.service';

@Injectable()
export class CoreTableApiServcice extends BaseService {

    constructor(public apiService: ApiService) {
        super(apiService, 'CoreTableApi');
    }

    getDataType(){
        return this.apiService.get(`${this.apiName}/GetDataType`);
    }
}