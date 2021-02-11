import { Injectable } from '@angular/core';

import { ApiService } from '../../core/services';
import { BaseService } from '../../core/services/base.service';


@Injectable()
export class CoreModuleApiService extends BaseService {

    constructor(public apiService: ApiService) {
        super(apiService, 'CoreModuleApi');
    }

    getModulesByCurrentUser() {
        return this.apiService.get(`${this.apiName}/GetModulesByCurrentUser`);
    }
}
