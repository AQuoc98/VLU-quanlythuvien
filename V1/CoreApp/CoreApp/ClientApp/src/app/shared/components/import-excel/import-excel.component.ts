import { CoreImportExportExcelApiServices } from './../../../infrastructure/api-services/core-importexcel.api.service';
import { Component, OnInit } from '@angular/core';
import { RouteService } from '@core/services';
import { Logger } from '@core/logger';
import { Toast } from '@core/services';


export const logger = new Logger('ImportExcel')

@Component({
  selector: 'app-import-excel',
  templateUrl: './import-excel.component.html',
  styleUrls: ['./import-excel.component.scss']
})
export class ImportExcelComponent implements OnInit {

  p: number = 1;
  isShowPaginationControl: Boolean = false;

  fileToUpload: File = null;
  totalRecord: ''
  totalRecordSuccess: ''
  dataExcelInvalid: []
  isShowTableInvalid: Boolean = false



  constructor(private routerService: RouteService, private importExportExcel: CoreImportExportExcelApiServices, private toast: Toast) { }

  ngOnInit() {

  }

  return() {
    this.routerService.navigate(['/admin/bookitem/'], { replaceUrl: true })
  }

  importExcel(evt) {
    this.fileToUpload = evt.target.files[0]
    this.importExportExcel.ImportExcel(this.fileToUpload).subscribe((res) => {
      console.log(res)
      let indexExtenData = 3
      let key = Object.keys(res)[indexExtenData]
      let value = res[key]
      // Show total record + total record success
      this.totalRecord = value.toTalCount
      this.totalRecordSuccess = value.successCount
      //  Show table with data invalid
      this.dataExcelInvalid = value.invalidData
      console.log(this.dataExcelInvalid)

      if (this.dataExcelInvalid.length > 0) {
        this.isShowTableInvalid = true
        this.isShowPaginationControl = true

      }
      // Show message complete import
      let keyMessage = Object.keys(res)[2]
      let message = res[keyMessage].map(item => this.toast.success(item))
    }
    )
  }
}
