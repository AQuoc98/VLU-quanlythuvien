
import { Injectable } from '@angular/core';

import { ApiService } from '../../core/services';
import { environment } from 'environments/environment';


@Injectable()
export class FileManagerApiService {

  private apiName: string = 'FileManagerApi';

  constructor(private apiService: ApiService) {

  }

  getFilesFromDirectory(directoryPath: string, pageIndex: number) {
    return this.apiService.get(`${this.apiName}/GetFilesFromDirectory?directoryPath=${directoryPath}&&pageIndex=${pageIndex}`);
  }

  createDirectory(createModel) {
    return this.apiService.post(`${this.apiName}/CreateDirectory`, createModel);
  }

  deleteDirectory(directoryPath: string[]) {
    return this.apiService.post(`${this.apiName}/DeleteFileOrDirectory`, directoryPath);
  }

  get uploadFileManager() {
    return `${environment.serverUrl}/${this.apiName}/UploadFileManager`;
  }

  get uploadTmpFile() {
      return `${environment.serverUrl}/${this.apiName}/UploadTempFile`;
  }
}


