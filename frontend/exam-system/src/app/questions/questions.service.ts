import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { Question } from "../models/question.model";

@Injectable({providedIn: 'root'})
export class QuestionsService{
    questions$: BehaviorSubject<Question[]> = new BehaviorSubject<Question[]>([]);
}