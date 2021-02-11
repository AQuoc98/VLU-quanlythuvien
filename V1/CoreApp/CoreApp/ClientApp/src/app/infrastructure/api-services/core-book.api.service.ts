import { Injectable } from '@angular/core';
import { ApiService } from '../../core/services';
import { BaseService } from '../../core/services/base.service';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class CoreBookApiServices extends BaseService {
  private baseApiUrl: string;
  private apiUploadUrl: string;
  constructor(public apiService: ApiService, private httpClient: HttpClient) {
    super(apiService, 'DoBookApi');
    this.baseApiUrl = 'http://171.244.34.24:808/api/DoBookApi/';
    this.apiUploadUrl = this.baseApiUrl + 'UploadFile';
  }
  UploadBook(bookModel) {
    return this.apiService.post(`${this.apiName}/UploadBook`, bookModel);
  }

  UpdateBook(bookModel) {
    return this.apiService.post(`${this.apiName}/UpdateBook`, bookModel);
  }

  public get uploadFileApi() {
    return this.apiUploadUrl;
  }

}
