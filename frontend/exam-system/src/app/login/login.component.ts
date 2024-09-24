import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { LoginService } from './login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  constructor(private loginService:LoginService) { }
  emailExists: boolean = true;

  onSubmit(formData: NgForm) {
    console.log(formData);
    this.doesEmailExist(formData.value.email);
  }

  onEmailChanged() {
    this.emailExists = true;
    }

  isControlInvalid(formData: NgForm, controlName: string) {
    return formData?.form?.get(controlName)?.invalid && formData?.form?.get(controlName)?.touched
  }

  private doesEmailExist(email: string) {
    this.loginService.existsByEmail(email).subscribe( {
     next: exists => this.emailExists = exists,
     error: error => this.emailExists = false,
    })
  }
}
