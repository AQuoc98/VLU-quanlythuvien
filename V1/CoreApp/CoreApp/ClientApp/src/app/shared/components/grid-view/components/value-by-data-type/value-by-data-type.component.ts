import { Component, OnInit, Input } from '@angular/core';
import { Constants } from '@core/constants';


@Component({
  selector: 'app-value-by-data-type',
  templateUrl: 'value-by-data-type.component.html'
})
export class ValueByDataTypeComponent implements OnInit {
  @Input() dataTypeCode: string = '';
  @Input() value: string = '';
  dataTypes: any = Constants.DataTypes;

  constructor() { }

  ngOnInit() {

  }





}
