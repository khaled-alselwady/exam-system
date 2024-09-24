import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  onSubmit(formData: NgForm) {
    console.log(formData);
    }

  isControlInvalid(formData: NgForm, controlName: string) {
    return formData?.form?.get(controlName)?.invalid && formData?.form?.get(controlName)?.touched
  }

}
