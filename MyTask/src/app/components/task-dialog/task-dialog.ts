import { Component, Injectable, Input, Output, EventEmitter, OnInit, inject } from '@angular/core';
import { MAT_DIALOG_DATA,
  MatDialog,
  MatDialogActions,
  MatDialogClose,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle } from '@angular/material/dialog'; //Import Dialog
import { MyTask } from '../../models/task.model';
import { Status } from '../../models/task.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select'
import {MatButtonModule} from '@angular/material/button'
import {FormsModule} from '@angular/forms';
import {MatFormFieldModule} from '@angular/material/form-field';
import { TaskService } from '../../services/task.service';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatCheckbox } from "@angular/material/checkbox";
import {MatCheckboxModule} from '@angular/material/checkbox';


@Component({
  selector: 'app-task-dialog',
  templateUrl: './task-dialog.html',
  styleUrl: './task-dialog.scss',
  imports: [
    CommonModule,
    MatIconModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatFormFieldModule,
    FormsModule,
    MatDialogTitle,
    MatDialogContent,
    MatDialogActions,
    MatDatepickerModule,
    MatNativeDateModule,
    MatCheckbox,
    MatCheckboxModule
]
})

export class TaskDialogComponent implements OnInit {



  constructor(private dialogRef: MatDialogRef<TaskDialogComponent>,
              public taskService: TaskService
  )
  {

  }

  @Output() taskChangeEvent = new EventEmitter<MyTask>();

  data = inject<MyTask>(MAT_DIALOG_DATA);

  planed: number =0;

  ngOnInit(): void {
    if(this.data==null){
      this.data = { id: 0, title: '', description:'', statusId: 1, orderNumber: 0, isFixed: false };
    }
    else{
      this.planed = this.data.plannedTime ? this.data.plannedTime/60 : 0;
    }
  }

  delete(archive: boolean){
    if(this.data.id != 0){

      this.taskService.deleteTask(this.data.id, archive).subscribe(
        (data)=>{
          var tasks = this.taskService.tasksSubject.getValue().filter(i=>i.id !== this.data.id)
          this.taskService.tasksSubject.next(tasks);
          this.taskChangeEvent.emit(undefined);
          this.dialogRef.close();
        },
        error=>{

        }


      );


    }
  }

  save(): void {

      this.data.plannedTime = this.planed*60;

      if(this.data.id != 0){
        this.taskService.updateTask(this.data).subscribe(
        (data)=>{
          this.taskChangeEvent.emit(data);
          let t = this.taskService.tasksSubject.value.find(i=>i.id == data.id);
          if(t){
            t = data;
          }
          this.dialogRef.close();
        },
        (error)=>{
          console.log(error);
          alert("Не удалось создать задачу...");
        }
      );
    }
    else{
      this.taskService.addTask(this.data).subscribe(
        (data)=>{
          this.taskService.tasksSubject.next([...this.taskService.tasksSubject.getValue(), data]);
          this.taskChangeEvent.emit(data);
          this.dialogRef.close();
        },
        (error)=>{
          console.log(error);
          alert("Не удалось создать задачу...");
        }
      );
    }


  }

  cancel(): void {
    this.dialogRef.close();
  }

}
