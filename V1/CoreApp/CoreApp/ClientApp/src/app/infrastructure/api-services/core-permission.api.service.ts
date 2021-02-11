import { Injectable } from '@angular/core';

import { ApiService } from '../../core/services';
import { BaseService } from '../../core/services/base.service';


@Injectable()
export class CorePermissionApiService extends BaseService {

    constructor(public apiService: ApiService) {
        super(apiService, 'CorePermissionApi');
    }

    getPermissions() {
        return this.apiService.get(`${this.apiName}/GetPermissions`);
    }

    getRoleByCurrentUser(){
        return this.apiService.get(`${this.apiName}/GetRoleByCurrentUser`);
    }
}
