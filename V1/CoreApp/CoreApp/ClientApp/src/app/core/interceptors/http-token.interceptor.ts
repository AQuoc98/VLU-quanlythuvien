import { Injectable, Injector } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';

import { AuthService } from '../../infrastructure/auth/auth.service';

@Injectable()
export class HttpTokenInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // const headersConfig = {
    //     'Content-Type': 'application/json',
    //     'Accept': 'application/json'
    // };

    // const token = this.authService.token;

    // if (token) {
    //     headersConfig['Authorization'] = `Bearer ${token}`;
    // }

    // const request = req.clone({ setHeaders: headersConfig });
    // return next.handle(request);



    const headersConfig = {
      'Access-Control-Allow-Origin': '*'

    };

    const token = this.authService.token;

    if (token) {
      headersConfig['Authorization'] = `Bearer ${token}`;
    }

    const request = req.clone({ setHeaders: headersConfig });
    return next.handle(request);

    // const token = this.authService.token;

    // const request = req.clone({ headers: req.headers.append('Authorization', `Bearer ${token}`) });
    // return next.handle(request);
  }
}
