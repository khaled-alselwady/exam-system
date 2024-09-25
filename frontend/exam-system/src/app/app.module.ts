import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http'; 

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { QuestionsComponent } from './questions/questions.component';
import { HeaderComponent } from './shared/header/header.component';
import { ResultComponent } from './result/result.component';
import { ErrorMessageComponent } from './shared/error-message/error-message.component';
import { QuestionComponent } from './questions/question/question.component';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    QuestionsComponent,
    HeaderComponent,
    ResultComponent,
    ErrorMessageComponent,
    QuestionComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    CommonModule
  ],
  exports: [
    FormsModule,
    ErrorMessageComponent,
    HeaderComponent,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
