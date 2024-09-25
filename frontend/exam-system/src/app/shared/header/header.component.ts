import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { StudentService } from 'src/app/student.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit,OnDestroy {
studentName: string = 'Student Name';
currentStudentSub: Subscription;
currentTime: string;
currentDate: string;
timeInterval: any;

constructor(private studentService: StudentService) {}

ngOnInit(): void {
 this.currentStudentSub = this.studentService.currentStudent$?.subscribe(student => {
    if (student) { 
      this.studentName = student.Name;
    }
  });

  // to show the time and date initially
  this.updateTimeAndDate(); 

  this.timeInterval = setInterval(() => {
    this.updateTimeAndDate();
  }, 1000); 
 }

 ngOnDestroy(): void {
  this.currentStudentSub?.unsubscribe();
  clearInterval(this.timeInterval);
}

// Method to update time and date
private updateTimeAndDate(): void {
  const now = new Date();
  this.currentTime = now.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
  this.currentDate = now.toLocaleDateString('en-US', { day: 'numeric', month: 'long', year: 'numeric' });
}
}
