import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.css']
})
export class QuestionComponent implements OnInit {
 @Input() questionTitle: string;
 @Input() choices: string[] = [];
 @Input() questionId: number;
 @Input() selectedAnswers = {};

  constructor() { }

  ngOnInit(): void {
    console.log('Question Title: ', this.questionTitle);
    console.log('Choices: ', this.choices);
    console.log('Question ID: ', this.questionId);
  }

}
