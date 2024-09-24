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
  subjectNameExists: boolean = true;

  onSubmit(formData: NgForm) {
    console.log(formData);
    this.doesEmailExist(formData.value.email);
    this.doesSubjectNameExist(formData.value.subject);
  }

  onEmailChanged() {
    this.emailExists = true;
    }

    onSubjectNameChanged() {
      this.subjectNameExists = true;
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

  private doesSubjectNameExist(subjectName: string) {
    this.loginService.existsBySubjectName(subjectName).subscribe( {
     next: exists => this.subjectNameExists = exists,
     error: error => this.subjectNameExists = false,
    })
  }
}
