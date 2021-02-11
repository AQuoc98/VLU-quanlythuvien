import { Injectable } from '@angular/core';

import { ApiService } from '../../core/services';
import { BaseService } from '../../core/services/base.service';


@Injectable()
export class CoreMenuApiService extends BaseService {

    constructor(public apiService: ApiService) {
        super(apiService, 'CoreMenuApi');
    }

    getMenus() {
        return this.apiService.get(`${this.apiName}/GetMenus`);
    }

    getParentMenus() {
        return this.apiService.get(`${this.apiName}/GetParentMenus`);
    }
}
