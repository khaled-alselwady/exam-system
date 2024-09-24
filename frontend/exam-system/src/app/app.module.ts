import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { BrowserModule } from '@angular/platform-browser';
import { LoginComponent } from './login/login.component';
import { QuestionsComponent } from './questions/questions.component';
import { HeaderComponent } from './shared/header/header.component';
import { ResultComponent } from './result/result.component';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule } from '@angular/forms';
import { ErrorMessageComponent } from './shared/error-message/error-message.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    QuestionsComponent,
    HeaderComponent,
    ResultComponent,
    ErrorMessageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
