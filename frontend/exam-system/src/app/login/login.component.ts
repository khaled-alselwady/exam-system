import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { LoginService } from './login.service';
import { StudentService } from '../student.service';
import { Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { SubjectService } from '../subject.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  constructor(private loginService:LoginService,
              private studentService: StudentService,
              private subjectService: SubjectService,
              private router: Router) { }

  emailExists: boolean = true;
  subjectNameExists: boolean = true;

  onSubmit(formData: NgForm) {
    const emailCheck$ = this.doesEmailExist(formData?.value.email);
    const subjectCheck$ = this.doesSubjectNameExist(formData?.value.subject);
    const student$ = this.findStudentByEmail(formData?.value.email);
    const subject$ = this.findSubjectByName(formData?.value.subject);
  
    forkJoin([emailCheck$, subjectCheck$, student$, subject$]).subscribe(([emailExists, subjectExists, student, subject]) => {
      this.emailExists = emailExists;
      this.subjectNameExists = subjectExists;
  
      if (this.emailExists && this.subjectNameExists && student && subject) {
        this.studentService.currentStudent$?.next(student);
        this.subjectService.currentSubject$?.next(subject);
        this.router.navigate(['/questions']);
      }
    });
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

  private findStudentByEmail(email: string) {
    return this.studentService.findByEmail(email).pipe(
      catchError(error => {
        this.studentService.currentStudent$.next(null);
        return of(null);
      })
    );
  }

  private findSubjectByName(subjectName: string) {
    return this.subjectService.findByName(subjectName).pipe(
      catchError(error => {
        this.subjectService.currentSubject$.next(null);
        return of(null);
      })
    )
  }

  private doesEmailExist(email: string) {
   return this.loginService.existsByEmail(email).pipe(
      catchError(error => {
        this.emailExists = false;  
        return of(false);
      })
    );
  }

  private doesSubjectNameExist(subjectName: string) {
   return this.loginService.existsBySubjectName(subjectName).pipe(
      catchError(error => {
        this.subjectNameExists = false;
        return of(false);
      })
    );
  }
}
