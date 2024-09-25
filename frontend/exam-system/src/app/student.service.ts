import { Injectable } from "@angular/core";
import { Student } from "./models/student.model";
import { HttpClient } from "@angular/common/http";
import { BehaviorSubject} from "rxjs";

@Injectable({providedIn: 'root'})
export class StudentService {
    currentStudent$: BehaviorSubject<Student | null> = new BehaviorSubject<Student | null>(null);

    constructor(private http: HttpClient) {}

    findByEmail(email: string) {
       return this.http.get<Student>(`https://localhost:44304/api/students/findByEmail?email=${email}`)
    }
}