import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import * as _ from 'lodash';
import { formatDate } from '@angular/common';
import { Constants } from '@core/constants';
import { CoreViewApiService } from '@infrastructure/api-services';
import { ModuleViewManagementService } from '@infrastructure/services/module-view-management.service';


@Component({
  selector: '[app-filter-control]',
  templateUrl: 'filter-control.component.html'
})
export class FilterControlComponent implements OnInit {
  @Input() dataColumns: any = [];

  @Output() onFilter = new EventEmitter();

  dataTypes: any = Constants.DataTypes;
  filterSelectData: any = {};
  filterModel: any = {};
  isShowFilter: boolean = true;

  constructor(private coreViewApiService: CoreViewApiService,
    private moduleViewManagementService: ModuleViewManagementService) {
  }

  ngOnInit() {
    this.loadFilterSelectData();
  }

  filterViewTrackByFn(index, item) {
    return item.id;
  }



  filter() {
    const conditions = this.generateConditions();
    this.onFilter.emit(conditions);
  }

  clear() {
    this.filterModel = {};
  }

  private generateConditions() {
    let conditions: any = [];
    _.forEach(this.filterModel, (value, key) => {
      if (value) {
        const column = _.find(this.dataColumns, item => item.sqlName === key);
        const condition = this.generateConditionItem(column, value);
        conditions.push(condition);
      }
    });
    return conditions;
  }

  private generateConditionItem(column, value): any {
    const currentView = this.moduleViewManagementService.currentView;
    let conditionItem = {
      viewId: currentView.id,
      columnId: column.id,
      value: value,
      operator: ''
    };

    if (column.dataTypeCode === this.dataTypes.Text ||
      column.dataTypeCode === this.dataTypes.MultiText ||
      column.dataTypeCode === this.dataTypes.Email ||
      column.dataTypeCode === this.dataTypes.Number ||
      column.dataTypeCode === this.dataTypes.Currency) {
      conditionItem.operator = 'like';
      conditionItem.value = "N'%" + value + "%'";
    }

    if (column.dataTypeCode === this.dataTypes.Select) {
      conditionItem.operator = '=';
      conditionItem.value = `'${value}'`;
    }

    if (column.dataTypeCode === this.dataTypes.MultiSelect) {
      conditionItem.operator = 'in';
      conditionItem.value = `(${value.join(',')})`;
    }

    if (column.dataTypeCode === this.dataTypes.Checkbox) {
      conditionItem.operator = '=';
      conditionItem.value = value == true ? 1 : 0;
    }

    if (column.dataTypeCode === this.dataTypes.DateTime) {
      conditionItem.operator = '=';
      conditionItem.value = formatDate(value, 'yyyy/MM/dd', 'en');
    }

    if (column.dataTypeCode === this.dataTypes.DateRange) {
      const fromDate = value[0];
      const toDate = value[1];
      conditionItem.operator = '>=';
      conditionItem.value = `'${formatDate(fromDate, 'yyyy/MM/dd', 'en')}' AND ${column.tableAlias}.${column.sqlName} <= '${formatDate(toDate, 'yyyy/MM/dd', 'en')}'`;
    }

    if (column.dataTypeCode === this.dataTypes.Time) {
      // Todo
    }

    return conditionItem;
  }

  private loadFilterSelectData() {
    // Load filter data for select
    const currentModule = this.moduleViewManagementService.currentModule
    this.coreViewApiService.getGridViewFilterSelectData(currentModule.id).subscribe(res => this.buildFilterSelectData(res));

    // Check have any column allow search
    const anyHaveFilterColumn = _.filter(this.dataColumns, col => col.searchable === true);
    this.isShowFilter = anyHaveFilterColumn.length > 0;
  }

  private buildFilterSelectData(data) {
    const columnHaveEnumValues = _.filter(this.dataColumns, item => item.dataTypeCode === this.dataTypes.Select || item.dataTypeCode === this.dataTypes.MultiSelect);
    _.forEach(columnHaveEnumValues, column => {
      this.filterSelectData[column.sqlName] = _.filter(data.selectData, item => item.columnId === column.id);

    });
  }



}
