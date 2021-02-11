import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { takeUntil, finalize } from 'rxjs/operators';
import { Router } from '@angular/router';

import { AuthService } from '@infrastructure/auth/auth.service';
import { Subject } from 'rxjs';
import { Toast } from '@core/services/toast';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  //#region Fields
  loginForm: FormGroup;
  private destroyed$ = new Subject();
  private isFormValid: boolean = false;

  //#endregion

  constructor(private authService: AuthService, private formBuilder: FormBuilder, private router: Router, private toast: Toast) {
    this.createLoginForm();
  }

  //#region Page Cycles Functions

  ngOnInit() {
    this.authService.logout();
  }

  ngOnDestroy() {
    this.destroyed$.next();
    this.destroyed$.complete();
  }

  //#endregion

  //#region Public Functions

  login() {
    this.authService.login(this.loginForm.value)
      .pipe(
        // To manage unsubscribe
        takeUntil(this.destroyed$),
        finalize(() => {
          this.loginForm.markAsPristine();
        })
      )
      .subscribe((isSuccess: boolean) => {
        if (isSuccess) {

          this.gotoHomePage();
        } else {
          // kết quả fail show alert
          this.toast.error('Sai Tên đăng nhập hoặc Mật khẩu xin vui lòng thử lại');
        }

      });
  }

  //#endregion

  //#region Private Functions

  private gotoHomePage() {
    this.router.navigate(['/admin'], { replaceUrl: true });
  }

  private createLoginForm() {
    this.loginForm = this.formBuilder.group({
      // username: ['', Validators.compose([Validators.required, Validators.email])],
      username: ['', Validators.required],
      password: ['', Validators.required],
      remember: [true],
    });

    this.loginForm.valueChanges
      .pipe(
        // To manage unsubscribe
        takeUntil(this.destroyed$)
      )
      .subscribe((v) => {
        this.isFormValid = this.loginForm.valid;
      });
  }

  //#endregion
}
