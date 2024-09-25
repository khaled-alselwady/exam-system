import { Component, OnDestroy, OnInit } from '@angular/core';
import { SubjectService } from '../subject.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-questions',
  templateUrl: './questions.component.html',
  styleUrls: ['./questions.component.css']
})
export class QuestionsComponent implements OnInit, OnDestroy {
  private timer: any;
  timeLeft = '00:15:00'; 
  totalSeconds = 900; // Total seconds (15 * 60)
  questionCount = 0;
  subjectServiceSub: Subscription;

  constructor(private subjectService: SubjectService) { }

  ngOnInit(): void {
    this.startTimer();
   this.subjectServiceSub = this.subjectService.currentSubject$.subscribe(subject => {
      this.getQuestionCountBySpecificSubject(subject?.Id);
    })
    
  }

  ngOnDestroy(): void {
    this.subjectServiceSub?.unsubscribe();
    if (this.timer) {
      clearInterval(this.timer);
    }
  }

  startTimer(): void {
    this.timer = setInterval(() => {
      if (this.totalSeconds > 0) {
        this.totalSeconds--; 
        this.updateTimeLeft();
      } else {
        clearInterval(this.timer);
      }
    }, 1000); 
  }

  updateTimeLeft(): void {
    const hours = Math.floor(this.totalSeconds / 3600);
    const minutes = Math.floor((this.totalSeconds % 3600) / 60);
    const seconds = this.totalSeconds % 60;

    // Format time as HH:MM:SS
    this.timeLeft = `${this.padZero(hours)}:${this.padZero(minutes)}:${this.padZero(seconds)}`;
  }

  padZero(num: number): string {
    return num < 10 ? '0' + num : num.toString();
  }

  private getQuestionCountBySpecificSubject(subjectId: number) {
    this.subjectService.getQuestionCountBySpecificSubject(subjectId).subscribe(count => {
      this.questionCount = count;
    });
  }
}
