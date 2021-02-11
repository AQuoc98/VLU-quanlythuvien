import { Injectable } from '@angular/core';

import { ApiService } from '../../core/services';
import { BaseService } from '../../core/services/base.service';


@Injectable()
export class CoreUserApiService extends BaseService {

    constructor(public apiService: ApiService) {
        super(apiService, 'CoreUserApi');
    }

    EditProfile(profileModel) {
        return this.apiService.post(`${this.apiName}/EditProfile`, profileModel);
    }
    Insert(profileModel){
        return this.apiService.post(`${this.apiName}/Insert`, profileModel);
    }

}
