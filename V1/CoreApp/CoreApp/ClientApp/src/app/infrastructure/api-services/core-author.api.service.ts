import { Injectable } from '@angular/core';

import { ApiService } from '../../core/services';
import { BaseService } from '../../core/services/base.service';

@Injectable()
export class CoreAuthorApiServices extends BaseService {
    constructor(public apiService : ApiService) {
        super(apiService, 'DoAuthorApi');
    }


}
