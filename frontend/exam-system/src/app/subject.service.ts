import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { Subject } from "../app/models/subject.model";
import { Question } from "./models/question.model";

@Injectable({providedIn:'root'})
export class SubjectService{
    baseURL = 'https://localhost:44304/api/subjects';
    currentSubject$: BehaviorSubject<Subject | null> = new BehaviorSubject<Subject | null>(null);

    constructor(private http: HttpClient){}

    getQuestionCountBySpecificSubject(subjectId: number) {
        return this.http.get<number>(`${this.baseURL}/count-questions/${subjectId}`);
    }

    findByName(name: string) {
        return this.http.get<Subject>(`${this.baseURL}/findByName?name=${name}`);
    }
    
    getQuestionsBySubjectId(subjectId: number) {
        return this.http.get<Question[]>(`${this.baseURL}/questions/${subjectId}`);
    }
}