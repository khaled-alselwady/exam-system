import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Student } from "../models/student.model";

@Injectable({providedIn: 'root'})
export class LoginService {
    constructor(private http: HttpClient) {}

    existsByEmail(email: string) {
        return this.http.get<boolean>(`https://localhost:44304/api/students/existsByEmail?email=${email}`);
    }

    existsBySubjectName(subjectName: string) {
        console.log(subjectName);
        return this.http.get<boolean>(`https://localhost:44304/api/subjects/existsByName?name=${subjectName}`);
    }
}