import { Injectable } from '@angular/core';
import { ApiService } from '../../core/services';
import { BaseService } from '../../core/services/base.service';

@Injectable()
export class CoreMemberApiServices extends BaseService {
  private baseApiUrl: string;
  private apiUploadUrl: string;
  constructor(public apiService: ApiService) {
    super(apiService, 'DoMemberApi');
    this.baseApiUrl = 'http://171.244.34.24:808/api/DoMemberApi/';
    this.apiUploadUrl = this.baseApiUrl + 'UploadImageMember';
  }


  Insert(memberModel) {
    return this.apiService.post(`${this.apiName}/Insert`, memberModel);
  }

  Update(memberModel) {
    return this.apiService.post(`${this.apiName}/Update`, memberModel);
  }

  public get UploadImageMember() {
    return this.apiUploadUrl;
  }

}
