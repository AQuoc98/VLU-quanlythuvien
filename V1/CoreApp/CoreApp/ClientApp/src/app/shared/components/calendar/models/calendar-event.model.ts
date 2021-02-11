export interface CalendarEventModel {
    id: string,
    allDay: boolean,
    start: Date,
    title: string,
    textColor?: string,
    backgroundColor?: string
}