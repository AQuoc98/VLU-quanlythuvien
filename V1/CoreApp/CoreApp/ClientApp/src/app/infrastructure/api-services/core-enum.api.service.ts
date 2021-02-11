import { Injectable } from '@angular/core';

import { ApiService } from '../../core/services';
import { BaseService } from '../../core/services/base.service';


@Injectable()
export class CoreEnumApiService extends BaseService {

    constructor(public apiService: ApiService) {
        super(apiService, 'CoreEnumApi');
    }

    getEnumValuesByEnum(enumCode: string) {
        return this.apiService.get(`${this.apiName}/getEnumValuesByEnum?enumCode=${enumCode}`);
    }

    getEnumValuesByEnumId(enumId: string) {
        return this.apiService.get(`${this.apiName}/getEnumValuesByEnumId?enumId=${enumId}`);
    }
}
