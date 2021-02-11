import { CalendarToolBarModel } from './calendar-toolbar.model';
import { CalendarTitleFormatModel } from './calendar-title-format.model';
import { CalendarButtonTextModel } from './calendar-button-text.model';
import { CalendarBusinessHoursModel } from './calendar-business-hours.model';

export interface CalendarOptionModel {
    header: CalendarToolBarModel,
    footer: CalendarToolBarModel,
    titleFormat: CalendarTitleFormatModel,
    // String, default: ' \u2013 ' (en dash)
    titleRangeSeparator?: string,
    buttonText: CalendarButtonTextModel,
    themeSystem: string,
    plugins: any[],
    defaultView: string,
    scrollTime: string,
    navLinks: boolean,
    weekNumbersWithinDays: boolean,
    timeFormat: string,
    businessHours: CalendarBusinessHoursModel,
    nowIndicator: boolean,
    selectMirror: boolean,
    selectable: boolean
}