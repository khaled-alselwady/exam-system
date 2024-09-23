import { RouterModule, Routes } from "@angular/router";
import { LoginComponent } from "./login/login.component";
import { QuestionsComponent } from "./questions/questions.component";
import { ResultComponent } from "./result/result.component";
import { NgModule } from "@angular/core";

const appRoutes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'questions', component: QuestionsComponent },
    { path: 'result', component: ResultComponent },
    { path: '', redirectTo: '/login', pathMatch: 'full' },
    {path: '**', redirectTo: '/login', pathMatch: 'full' }
]

@NgModule({
    imports: [RouterModule.forRoot(appRoutes)],
    exports: [RouterModule],
  })
  export class AppRoutingModule {}