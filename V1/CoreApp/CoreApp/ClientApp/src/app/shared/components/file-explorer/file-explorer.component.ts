import { Component, EventEmitter, Output, Input, OnInit, ViewChild } from '@angular/core';
import * as _ from 'lodash';
import { FileUploader, FileSelectDirective } from 'ng2-file-upload';
import { TranslateService } from '@ngx-translate/core';
import { takeUntil } from 'rxjs/operators';
import { Subject, combineLatest } from 'rxjs';
import { TabsetComponent } from 'ngx-bootstrap';

import { Logger } from '../../../core';
import { FileManagerApiService } from '../../../infrastructure/api-services';
import { AuthService } from '../../../infrastructure/auth/auth.service';
import { FileHelper } from '../../../infrastructure/helpers';

const logger = new Logger('FileExplorer');

@Component({
  selector: 'app-file-explorer',
  templateUrl: './file-explorer.component.html',
  styleUrls: ['./file-explorer.component.scss']
})
export class FileExplorerComponent implements OnInit {

  @ViewChild('staticTabs') staticTabs: TabsetComponent;
  result = new EventEmitter();

  fileManager: any = {
    files: [],
    directories: []
  };

  fileBeforeUpload: any = {
    files: [],
    directories: []
  };

  uploader: FileUploader;
  fileUploadQueue: any = [];
  paged = {
    pageIndex: 1,
    total: 0
  };

  isAllowSelectMultiple: boolean = false;
  i: number = 0;
  isSelectToDelete: boolean = false;

  private destroyed$ = new Subject();
  private uploaderOptions = {
    url: this.fileManagerApiService.uploadFileManager,
    itemAlias: 'file',
    autoUpload: true,
    authToken: `Bearer ${this.authService.token}`,
    removeAfterUpload: true,
    maxFileSize: 2000000, // 2mb
    // allowedMimeType: ['image/png', 'image/jpg', 'image/jpeg', 'image/svg+xml']
  };
  private errorMsg: string = '';
  private currentPath: string = ''; // Default is root

  constructor(private fileManagerApiService: FileManagerApiService, private authService: AuthService, private translate: TranslateService) {
    this.uploader = new FileUploader(this.uploaderOptions);
    this.translate.get('FILE_MANAGER.ERROR_FILE_TOO_BIG').pipe(
      takeUntil(this.destroyed$)
    ).subscribe((msg) => this.errorMsg = msg);
  }

  ngOnInit(): void {
    this.loadFiles('');
    this.uploadFileEvents();
  }

  ngOnDestroy() {
    this.destroyed$.next();
    this.destroyed$.complete();
  }

  accept() {
    const seletedFiles = _.filter(this.fileManager.files, (item) => item.isSelected === true);
    if (!seletedFiles || seletedFiles.length === 0) {
      this.result.emit('');
      return;
    }

    if (this.isAllowSelectMultiple) {
      const fileSelectedPaths = seletedFiles.map(file => file.websitePath);
      this.result.emit(fileSelectedPaths);
      return;
    }

    const fileSelectedPath = seletedFiles.length > 0 ? seletedFiles[0].websitePath : '';
    this.result.emit(fileSelectedPath);
  }

  decline() {
    this.result.error('Decline');
  }

  checkSelect() {
    let otherFiles = _.filter(this.fileManager.files, (item) => item.isSelected == true);
    let otherDirectories = _.filter(this.fileManager.directories, (item) => item.isSelected == true);
    return otherFiles.length != 0 || otherDirectories.length != 0;
  }

  checkSelectOne() {
    let otherFiles = _.filter(this.fileManager.files, (item) => item.isSelected == true);
    return otherFiles.length == 1;
  }

  directoryClick(directory) {
    let dPath = directory.path;
    if (this.isSelectToDelete) {
      directory.isSelected = !directory.isSelected;
    } else {
      const uOptions = { headers: [{ name: 'UploadRootPath', value: dPath }] };
      this.uploader.setOptions(uOptions);

      this.currentPath = dPath;
      this.paged.pageIndex = 1;
      this.loadFiles(dPath);
    }

  }

  fileClick(file) {
    if (this.isAllowSelectMultiple || this.isSelectToDelete) {
      // Choose multiple file or Choose to Delete
      file.isSelected = !file.isSelected;
    } else {
      // Choose single file
      let otherFiles = _.filter(this.fileManager.files, (item) => item.name != file.name);
      otherFiles.forEach(item => item.isSelected = false);
      file.isSelected = !file.isSelected;
    }
  }

  pageChanged($event) {
    this.paged.pageIndex = $event.page;
    this.loadFiles(this.currentPath);
  }

  selectToDelete() {
    if (this.isSelectToDelete) {
      this.isSelectToDelete = false;
      let otherFiles = _.filter(this.fileManager.files, (item) => item.isSelected == true);
      otherFiles.forEach(item => item.isSelected = false);
      let otherDir = _.filter(this.fileManager.directories, (item) => item.isSelected == true);
      otherDir.forEach(item => item.isSelected = false);
    } else this.isSelectToDelete = true;
  }

  addDirectory() {
    this.fileManager.directories.push({
      name: 'New Folder',
      isCreating: true
    });
  }

  createDirectory() {
    const directoriesCreate = this.fileManager.directories.filter(f => f.isCreating === true);
    let allCreateDiSubs = [];
    directoriesCreate.forEach(di => {
      allCreateDiSubs.push(this.fileManagerApiService.createDirectory({ directoryPath: this.currentPath, newDirectoryName: di.name }));
    });
    combineLatest(
      allCreateDiSubs
    ).subscribe(res => {
      this.paged.pageIndex = 1;
      this.loadFiles(this.currentPath);
    });
  }

  deleteFileOrDirectory() {
    let arrPath: string[] = [];
    let DirDelete = _.filter(this.fileManager.directories, (item) => item.isSelected == true);
    let FileDelete = _.filter(this.fileManager.files, (item) => item.isSelected == true);

    DirDelete.forEach(item => arrPath.push(item.path));
    FileDelete.forEach(item => arrPath.push(item.path));

    this.fileManagerApiService.deleteDirectory(arrPath)
      .subscribe(res => {
        this.paged.pageIndex = 1;
        this.loadFiles(this.currentPath);
        this.isSelectToDelete = false;
      });

  }

  getSrcImage(src: string) {
    if (src.indexOf('.docx') > -1 || src.indexOf('.doc') > -1) {
      return 'assets/admin/images/word.png';
    }
    return src;
  }

  private loadFiles(directoryPath: string) {
    this.fileManagerApiService.getFilesFromDirectory(directoryPath, this.paged.pageIndex).subscribe((res) => {
      this.fileManager = res;
      this.paged.total = this.fileManager.total;
      if (this.fileUploadQueue !== null) { this.selectFileUpload(this.fileManager, this.fileUploadQueue.length); }
    });
  }

  private uploadFileEvents() {
    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
      this.fileUploadQueue.push({ name: file.file.name, size: FileHelper.bytesToSize(file.file.size), progress: file.progress, errorMsg: '' });
    };
    this.uploader.onCompleteItem = (item: any, response: any, status: any, headers: any) => {
      // Upload complete
    };
    this.uploader.onWhenAddingFileFailed = (file: any, filter: any, options: any) => {
      switch (filter.name) {
        case 'fileSize':
          let errorMsg = this.errorMsg.replace('[FILE_SIZE]', FileHelper.bytesToSize(file.size))
            .replace('[MAX_SIZE]', FileHelper.bytesToSize(this.uploaderOptions.maxFileSize));
          this.fileUploadQueue.push({ name: file.name, size: FileHelper.bytesToSize(file.size), progress: 0, errorMsg: errorMsg });
          break;
        case 'fileType' || 'mimeType':
          this.fileUploadQueue.push({ name: file.name, size: FileHelper.bytesToSize(file.size), progress: 0, errorMsg: 'Định dạng file không được hộ trợ!' });
          break;
        default:
          this.fileUploadQueue.push({ name: file.name, size: FileHelper.bytesToSize(file.size), progress: 0, errorMsg: 'Đã xảy ra lỗi không thể upload!' });
          break;
      }

    };
    this.uploader.onProgressItem = (fileItem, progress) => {
      let file = _.find(this.fileUploadQueue, (item) => item.name === fileItem.file.name);
      file.progress = progress;
    };
    this.uploader.onCompleteAll = () => {
      this.paged.pageIndex = 1;
      this.loadFiles(this.currentPath);
    }
  }
  selectFileUpload(data, countFileUpload) {
    // Uncheck file select
    _.filter(data.files, (item) => item.isSelected === true).forEach(item => item.isSelected = false);
    // Select file upload
    const y = this.i;
    for (let x = 0; x < (countFileUpload - y); x++) {
      data.files[x].isSelected = true;
      this.i++;
    }
    this.staticTabs.tabs[0].active = true;
  }
}
