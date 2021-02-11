import { Component, EventEmitter, Output, Input, forwardRef, ViewChild, ElementRef, Renderer } from '@angular/core';
import { noop } from 'rxjs';
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from '@angular/forms';

import { Logger } from '../../../core';
import { FileExplorerService } from '../file-explorer/file-explorer.service';

const logger = new Logger('RTE');

export const CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR: any = {
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => RTEComponent),
    multi: true
};

@Component({
    selector: 'app-rte',
    templateUrl: './rte.component.html',
    styleUrls: ['./rte.component.scss'],
    providers: [CUSTOM_INPUT_CONTROL_VALUE_ACCESSOR]
})
export class RTEComponent implements ControlValueAccessor {

    // Types: Basic, Full
    // Default is Basic
    @Input() type: string = 'Basic';

    @ViewChild('btnTrick') btnTrick: ElementRef;

    apiKey: string = '8d0dab9dnr2li1r8uivul56m3h5ohovlmqux2p0p5wlh0mlk';
    configs: any = {
        'Basic': {
            menubar: true,
            plugins: [
                'advlist autolink lists link charmap print preview anchor textcolor',
                'searchreplace visualblocks code fullscreen',
                'insertdatetime paste code help wordcount'
            ],
            toolbar: 'undo redo | formatselect | bold italic backcolor | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | removeformat | help',
            language: 'vi_VN',
            language_url: '../../../assets/i18n/tinymce-languages/vi_VN.js',
            height: 300
        },
        'Full': {
            plugins: 'print preview fullpage searchreplace autolink directionality visualblocks visualchars fullscreen image link media codesample table charmap hr pagebreak nonbreaking anchor toc insertdatetime advlist lists wordcount imagetools textpattern help',
            toolbar: 'formatselect | bold italic strikethrough forecolor backcolor | link | alignleft aligncenter alignright alignjustify  | numlist bullist outdent indent  | removeformat',
            image_advtab: true,
            // link_list: [
            //     { title: 'My page 1', value: 'http://www.tinymce.com' },
            //     { title: 'My page 2', value: 'http://www.moxiecode.com' }
            // ],
            image_class_list: [
                { title: 'None', value: '' },
                { title: 'Responsive', value: 'img-fluid' },
                { title: 'Thumbnails', value: 'img-thumbnail' }
            ],
            importcss_append: true,
            file_picker_callback: (callback, value, meta) => this.filePickerAction(callback, value, meta),
            template_cdate_format: '[CDATE: %m/%d/%Y : %H:%M:%S]',
            template_mdate_format: '[MDATE: %m/%d/%Y : %H:%M:%S]',
            image_caption: true,
            height: 400,
            spellchecker_dialog: true,
            spellchecker_whitelist: ['Ephox', 'Moxiecode'],
            language: 'vi_VN',
            language_url: '../../../assets/i18n/tinymce-languages/vi_VN.js',
            relative_urls: false,
            convert_urls: false
        }
    };

    private filePickerCallback: any = noop;
    private innerValue: string = '';

    //Placeholders for the callbacks which are later provided
    //by the Control Value Accessor
    private onTouchedCallback: () => void = noop;
    private onChangeCallback: (_: any) => {};

    constructor(private fileExplorerService: FileExplorerService, private element: ElementRef, private renderer: Renderer) {

    }

    //get accessor
    get value(): any {
        return this.innerValue;
    };

    //set accessor including call the onchange callback
    set value(v: any) {
        if (v !== this.innerValue) {
            this.innerValue = v;
        }
    }

    //Set touched on blur
    onBlur() {
        this.onTouchedCallback();
    }

    // Set on change
    onChange($event) {
        this.onChangeCallback(this.innerValue);
        this.triggerChanged();
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

    showFileExplorer() {
        this.fileExplorerService.chooseFile().then((filePath: string) => {
            this.filePickerCallback(filePath, {});
        }, (err) => {

        });
    }

    private filePickerAction(callback, value, meta) {
        // This is trick to by pass problem, when call fileExporer directly here binding 2 way of angular will not working
        this.btnTrick.nativeElement.click();
        this.filePickerCallback = callback;
    }

    private triggerChanged() {
        let event = new CustomEvent('change', { bubbles: true });
        this.renderer.invokeElementMethod(this.element.nativeElement, 'dispatchEvent', [event]);
    }

}
