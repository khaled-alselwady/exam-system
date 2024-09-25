import { Component, Input, OnInit } from '@angular/core';
import { Question } from 'src/app/models/question.model';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.css']
})
export class QuestionComponent implements OnInit {
  @Input() questionNumber: number;
  @Input() question: Question;

  constructor() { }

  ngOnInit(): void {
    console.log(this.question);
  }

}
