import { Component, OnInit, OnDestroy, Input, ViewChild, Output, EventEmitter } from '@angular/core';
import daygrid from '@fullcalendar/daygrid';
import interaction from '@fullcalendar/interaction';
import timegrid from '@fullcalendar/timegrid';
import list from '@fullcalendar/list';
import timeline from '@fullcalendar/timeline';
import bootstrap from '@fullcalendar/bootstrap';
import { FullCalendarComponent } from '@fullcalendar/angular';

import { CalendarEventModel, CalendarOptionModel, DateEventOutputModel, ViewRenderedOutputModel } from './models';
import { UtilityService } from '@core/utility';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.scss']
})
export class CalendarComponent implements OnInit, OnDestroy {
  // Input data
  @Input() events: CalendarEventModel[];
  // Output data
  @Output() dateSelect: EventEmitter<DateEventOutputModel>;
  @Output() eventClick: EventEmitter<CalendarEventModel>;
  @Output() eventDrop: EventEmitter<CalendarEventModel>;
  @Output() eventResize: EventEmitter<CalendarEventModel>;
  @Output() viewRendered: EventEmitter<ViewRenderedOutputModel>;

  @ViewChild('calendar')
  calendarComponent: FullCalendarComponent;
  // Private variables
  private formatDate = 'YYYY-MM-DD HH:mm:ss';
  // Public variables
  calendarOptions: CalendarOptionModel = {
    buttonText: {
      day: 'Day',
      list: 'List',
      month: 'Month',
      today: 'Today',
      week: 'Week'
    },
    defaultView: 'dayGridMonth',
    header: {
      center: ''
    },
    footer: {},
    plugins: [daygrid, interaction, timegrid, list, timeline, bootstrap],
    themeSystem: 'bootstrap',
    titleFormat: {
      year: 'numeric',
      month: 'long',
      day: 'numeric'
    },
    navLinks: true,
    weekNumbersWithinDays: true,
    scrollTime: '8:00:00', // 8am
    timeFormat: 'H(:mm)',
    businessHours: {
      daysOfWeek: [1, 2, 3, 4, 5],
      startTime: '8:00',
      endTime: '17:00'
    },
    nowIndicator: true,
    selectMirror: true,
    selectable: true
  };

  constructor(private utilityService: UtilityService) {
    this.dateSelect = new EventEmitter<DateEventOutputModel>();
    this.eventClick = new EventEmitter<CalendarEventModel>();
    this.eventDrop = new EventEmitter<CalendarEventModel>();
    this.eventResize = new EventEmitter<CalendarEventModel>();
    this.viewRendered = new EventEmitter<ViewRenderedOutputModel>();
  }

  // Life cycle hook
  ngOnInit() {

  }

  ngOnDestroy() {

  }

  // Private functions

  // Public functions
  public handleDateClick(arg) {
    const allDay: boolean = arg.allDay;
    const date: Date = arg.date;
    let end: Date;
    if (!allDay) {
      end = new Date(this.utilityService.moment(date).add(30, 'minutes').format(this.formatDate));
    } else {
      end = new Date(this.utilityService.moment(date).endOf('date').format(this.formatDate));
    }
    const eventOutput: DateEventOutputModel = {
      allDay: arg.allDay,
      start: arg.date,
      end: end,
      isClick: true,
      isSelect: false
    };
  }

  public handleDateSelect(arg) {
    console.log(this.utilityService.moment(arg.start).format(this.formatDate));
    const end = arg.allDay ? new Date(this.utilityService.moment(arg.end).subtract(1, 'seconds').format(this.formatDate)) : arg.end;
    const eventOutput: DateEventOutputModel = {
      allDay: arg.allDay,
      start: new Date(this.utilityService.moment(arg.start).format(this.formatDate)),
      end: end,
      isClick: false,
      isSelect: true
    };
    this.dateSelect.emit(eventOutput);
  }

  public handleEventClick(arg) {
    const eventSelected: CalendarEventModel = arg.event;
    this.eventClick.emit(eventSelected);
  }

  public handleEventDrop(arg) {
    const oldEvent: CalendarEventModel = arg.oldEvent;
    const newEvent: CalendarEventModel = arg.event;
    this.eventDrop.emit(newEvent);
  }

  public handleEventResize(arg) {
    const eventResized: CalendarEventModel = arg.event;
    this.eventResize.emit(eventResized);
  }

  public handleViewRender(arg) {
    const viewRenderedOutputData: ViewRenderedOutputModel = {
      start: new Date(this.utilityService.moment(arg.view.currentStart).format(this.formatDate)),
      end: new Date(this.utilityService.moment(arg.view.currentEnd).format(this.formatDate))
    };
    this.viewRendered.emit(viewRenderedOutputData);
  }

}
