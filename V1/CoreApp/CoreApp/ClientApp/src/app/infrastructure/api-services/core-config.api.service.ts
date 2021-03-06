import { Injectable } from '@angular/core';

import { ApiService } from '@core/services';
import { BaseService } from '@core/services/base.service';


@Injectable()
export class CoreConfigApiService extends BaseService {

    constructor(public apiService: ApiService) {
        super(apiService, 'CoreConfigApi');
    }

    getConfigGroup() {
        return this.apiService.get(`${this.apiName}/GetConfigGroup`);
    }
}
