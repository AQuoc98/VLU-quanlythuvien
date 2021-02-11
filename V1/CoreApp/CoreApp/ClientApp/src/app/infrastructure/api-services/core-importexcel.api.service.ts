import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { ApiService } from '../../core/services';
import { BaseService } from '../../core/services/base.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';



@Injectable()
export class CoreImportExportExcelApiServices extends BaseService {
  constructor(public apiService: ApiService, private httpClient: HttpClient) {
    super(apiService, 'DoBookItemApi');

  }




  ImportExcel(fileToUpload: File): Observable<object> {
    const endpoint = 'http://171.244.34.24:808/api/DoBookItemApi/StartImport/';
    const formData: FormData = new FormData();
    formData.append('file', fileToUpload);
    return this.httpClient.post(endpoint, formData);
  }





}
