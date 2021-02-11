import { Component, forwardRef, OnInit, Input } from '@angular/core';
import { noop } from 'rxjs';
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from '@angular/forms';

import { Logger } from '../../../core';
import { FileUploader } from 'ng2-file-upload';
import { FileManagerApiService } from '@infrastructure/api-services';
import { AuthService } from '@infrastructure/auth';
import { FileHelper } from '@infrastructure/helpers';


export const CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR: any = {
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => AvatarChooserComponent),
    multi: true
};

@Component({
    selector: 'app-avatar-chooser',
    templateUrl: './avatar-chooser.component.html',
    styleUrls: ['./avatar-chooser.component.scss'],
    providers: [CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR]
})
export class AvatarChooserComponent implements ControlValueAccessor, OnInit {
    @Input('idUpload') idUpload: string;

    uploader: FileUploader;
    fileUploadQueue: any = [];

    private uploaderOptions = {
        url: this.fileManagerApiService.uploadFileManager,
        itemAlias: 'file',
        autoUpload: true,
        authToken: `Bearer ${this.authService.token}`,
        removeAfterUpload: true,
        maxFileSize: 2000000, // 2mb
        allowedMimeType: ['image/png', 'image/jpg', 'image/jpeg', 'image/svg+xml','application/pdf','application/x-pdf']
    };
    private innerValue: string = '';
    //Placeholders for the callbacks which are later provided
    //by the Control Value Accessor
    private onTouchedCallback: () => void = noop;
    private onChangeCallback: (_: any) => void = noop;

    constructor(private fileManagerApiService: FileManagerApiService, private authService: AuthService) {
        this.uploader = new FileUploader(this.uploaderOptions);
        console.log(this.idUpload);
    }

    ngOnInit() {
        this.uploadFileEvents();
    }

    //get accessor
    get value(): any {
        return this.innerValue;
    };

    //set accessor including call the onchange callback
    set value(v: any) {
        if (v !== this.innerValue) {
            this.innerValue = v;
            this.onChangeCallback(v);
        }
    }

    get id(): any {
        return this.idUpload;
    }

        //Set touched on blur
        onBlur() {
            this.onTouchedCallback();
        }

//From ControlValueAccessor interface
writeValue(value: any) {
    if (value !== this.innerValue) {
        this.innerValue = value;
    }
}

//From ControlValueAccessor interface
registerOnChange(fn: any) {
    this.onChangeCallback = fn;
}

//From ControlValueAccessor interface
registerOnTouched(fn: any) {
    this.onTouchedCallback = fn;
}

delete () {
    this.value = '';
}

    // Private functions 
    private uploadFileEvents() {
    this.uploader.onAfterAddingFile = (file) => {
        file.withCredentials = false;
        this.fileUploadQueue.push({ name: file.file.name, size: FileHelper.bytesToSize(file.file.size), progress: file.progress, errorMsg: '' });
    };
    this.uploader.onCompleteItem = (item: any, response: any) => {
        // Upload complete
        response = JSON.parse(response);
        const fileUri = response.filePath.replace('wwwroot', '').replace('\\', '/');
        this.value = fileUri;
    };

    this.uploader.onCompleteAll = () => {
        this.fileUploadQueue = [];
    }
}
}
