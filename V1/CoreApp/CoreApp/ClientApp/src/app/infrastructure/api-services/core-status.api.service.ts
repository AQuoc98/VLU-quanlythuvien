import { Injectable } from '@angular/core';
import { ApiService } from '../../core/services';
import { BaseService } from '../../core/services/base.service';

@Injectable()
export class CoreStatusApiServices extends BaseService {
    constructor(public apiService : ApiService) {
        super(apiService, 'DoStatusApi');
    }


}
