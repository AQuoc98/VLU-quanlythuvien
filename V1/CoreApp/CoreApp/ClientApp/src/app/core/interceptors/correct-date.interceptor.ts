import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import * as moment from 'moment-timezone';

@Injectable()
export class CorrectDateInterceptor implements HttpInterceptor {

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (req.method === 'POST' || req.method === 'PUT') {
            this.correctDate(req.body);
        }
        return next.handle(req);
    }

    correctDate(body) {
        if (body === null || body === undefined) {
            return body;
        }

        if (typeof body !== 'object') {
            return body;
        }

        for (const key of Object.keys(body)) {
            const value = body[key];
            if (value instanceof Date) {
                let timex = moment.tz(value.toJSON(), "Asia/Ho_Chi_Minh");
                body[key] = timex.format();
            } else if (typeof value === 'object') {
                this.correctDate(value);
            }
        }
    }
}
